using Core.Constants;
using Core.Entities;
using Core.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.AdminsManagement.FetchAdmins;

public sealed record FetchAdminsQueryHandler : IRequestHandler<FetchAdminsQuery, PagedResponse<FetchAdminsQueryResponse>>
{
    private readonly UserManager<Account> _userManager;

    public FetchAdminsQueryHandler(UserManager<Account> userManager)
    {
        _userManager = userManager;
    }

    public async Task<PagedResponse<FetchAdminsQueryResponse>> Handle(FetchAdminsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber ?? 1;
        var pageSize = request.PageSize ?? 5;
        var data = await _userManager.Users
            .Where(p => p.AccountType == AccountType.Admin)
            .OrderBy(p => p.CreatedOn)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .AsNoTracking()
            .Select(p => new FetchAdminsQueryResponse()
            {
                Id = p.Id,
                DisplayName = p.DisplayName,
                Email = p.Email ?? "",
                Permissions = p.Permissions,
                IsActive = p.Enabled,
                CreatedOn = p.CreatedOn,
            })
            .ToListAsync(cancellationToken: cancellationToken);
        var count = await _userManager.Users.Where(p => p.AccountType == AccountType.Admin).CountAsync(cancellationToken: cancellationToken);
        return new PagedResponse<FetchAdminsQueryResponse>("", data, count, pageNumber, pageSize);
    }
}