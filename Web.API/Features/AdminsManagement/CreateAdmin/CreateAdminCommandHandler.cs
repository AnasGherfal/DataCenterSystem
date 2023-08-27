using System.Security.Claims;
using Infrastructure;
using Infrastructure.Constants;
using Infrastructure.Entities;
using Infrastructure.Events.Admin;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Dtos;
using Shared.Exceptions;
using Web.API.Services.ClientService;

namespace Web.API.Features.AdminsManagement.CreateAdmin;

public sealed record CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, ContentResponse<CreateAdminCommandResponse>>
{
    private readonly IClientService _client;
    private readonly AppDbContext _dbContext;
    private readonly UserManager<Admin> _userManager;

    public CreateAdminCommandHandler(UserManager<Admin> userManager, AppDbContext dbContext, IClientService client)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<ContentResponse<CreateAdminCommandResponse>> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
    {
        var admin = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (admin != null) throw new BadRequestException("EMAIL_ALREADY_EXIST");
        var password = GeneratePassword();
        var @event = new AdminCreatedEvent(_client.GetIdentifier(), Guid.NewGuid(), new AdminCreatedEventData()
        {
            FullName = request.FullName!,
            UserName = Guid.NewGuid().ToString().Replace("-", ""),
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = request.Email!,
            Permissions = request.Permissions!.Value,
            EmpId = request.EmpId!.Value,
            Password = password,
        });
        var data = new Admin();
        data.Apply(@event);
        //TODO: Not Idempotent - Use Transaction Instead
        var result = await _userManager.CreateAsync(data, password);
        await _userManager.AddClaimsAsync(data, new List<Claim>()
        {
            new(ClaimsKey.IdentityId.Key(), data.Id.ToString()),
            new(ClaimsKey.DisplayName.Key(), data.DisplayName),
            new(ClaimsKey.Email.Key(), data.Email ?? ""),
            new(ClaimsKey.Permissions.Key(), data.Permissions.ToString("D")),
            new(ClaimsKey.EmailVerified.Key(), data.EmailConfirmed.ToString()),
        });
        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()!.Description);
        await _dbContext.Events.AddAsync(@event, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        var responseContent = new CreateAdminCommandResponse()
        {
            UserId = data.Id.ToString(),
            UserPassword = password,
        };
        return new ContentResponse<CreateAdminCommandResponse>("SUCCESS", responseContent);
    }

    private static readonly Random Random = new();
    private string GeneratePassword()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var newPassword = (Enumerable.Repeat(chars, 8).Select(s => s[Random.Next(s.Length)]).ToArray());
        return  new string(newPassword);
    }
}