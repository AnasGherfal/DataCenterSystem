using Core.Entities;
using Core.Exceptions;
using Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.AdminsManagement.FetchAdminById;

public sealed record FetchAdminByIdQueryHandler : IRequestHandler<FetchAdminByIdQuery, ContentResponse<FetchAdminByIdQueryResponse>>
{
    private readonly UserManager<Admin> _userManager;

    public FetchAdminByIdQueryHandler(UserManager<Admin> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ContentResponse<FetchAdminByIdQueryResponse>> Handle(FetchAdminByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _userManager.Users
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("USER_NOT_FOUND");
        return new ContentResponse<FetchAdminByIdQueryResponse>("", new FetchAdminByIdQueryResponse()
        {
            Id = data.Id,
            DisplayName = data.DisplayName,
            Email = data.Email ?? "",
            Permissions = data.Permissions,
            IsActive = data.Enabled,
            CreatedOn = data.CreatedOn,
        });
    }
}