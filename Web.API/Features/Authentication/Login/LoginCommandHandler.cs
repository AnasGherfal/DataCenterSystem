using System.IdentityModel.Tokens.Jwt;
using Infrastructure.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Options;
using Web.API.Services.TokenService;

namespace Web.API.Features.Authentication.Login;

public sealed record LoginCommandHandler : IRequestHandler<LoginCommand, ContentResponse<LoginCommandResponse>>
{
    private readonly AuthenticationOption _option;
    private readonly UserManager<Admin> _userManager;
    private readonly SignInManager<Admin> _signInManager;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(UserManager<Admin> userManager, SignInManager<Admin> signInManager,
        IOptions<AuthenticationOption> options, ITokenService tokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
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
        var result = await _signInManager.PasswordSignInAsync(user, request.Password!, false, true);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                throw new BadRequestException($"USER_ACCOUNT_IS_LOCKED_FOR_{_option.LockoutTimeInMinute}_MINUTES");
            if (result.IsNotAllowed)
                throw new BadRequestException("USER_ACCOUNT_IS_NOT_ALLOWED");
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