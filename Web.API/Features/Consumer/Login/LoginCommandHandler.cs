using System.IdentityModel.Tokens.Jwt;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Services;
using Core.Options;
using Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Web.API.Features.Consumer.Login;

public sealed record LoginCommandHandler : IRequestHandler<LoginCommand, ContentResponse<LoginCommandResponse>>
{
    private readonly AuthenticationOption _option;
    private readonly UserManager<Account> _userManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<Account> userManager,
        IOptions<AuthenticationOption> options, ITokenService tokenService)
    {
        _userManager = userManager;
        _option = options.Value;
        _tokenService = tokenService;
    }

    public async Task<ContentResponse<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user == null)
            throw new NotFoundException("USER_NOT_EXIST");
        if (!user.Enabled)
            throw new BadRequestException("USER_ACCOUNT_IS_DISABLED");
        if (await _userManager.IsLockedOutAsync(user))
            throw new BadRequestException($"USER_ACCOUNT_IS_LOCKED_FOR_{_option.LockoutTimeInMinute}_MINUTES");
        if (!user.EmailConfirmed)
            throw new BadRequestException($"USER_HAS_NOT_CONFIRMED_EMAIL");
        var checkPasswordResult = await _userManager.CheckPasswordAsync(user, request.Password!);
        if (!checkPasswordResult)
        {
            await _userManager.AccessFailedAsync(user);
            throw new NotFoundException("INCORRECT_CREDENTIALS_COMBINATION");
        }
        var token = await _tokenService.GenerateAccessToken(user);
        var responseContent = new LoginCommandResponse()
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            AccessTokenExpiresAt = token.ValidTo,
        };
        return new ContentResponse<LoginCommandResponse>("SUCCESS", responseContent);
    }
}