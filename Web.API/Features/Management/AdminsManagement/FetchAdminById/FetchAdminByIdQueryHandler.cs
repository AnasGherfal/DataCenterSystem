using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Wrappers;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Web.API.Features.Management.AdminsManagement.FetchAdminById;

public sealed record FetchAdminByIdQueryHandler : IRequestHandler<FetchAdminByIdQuery, ContentResponse<FetchAdminByIdQueryResponse>>
{
    private readonly AppDbContext _dbContext;

    public FetchAdminByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ContentResponse<FetchAdminByIdQueryResponse>> Handle(FetchAdminByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Users
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!)
                && p.AccountType == AccountType.Admin, cancellationToken: cancellationToken);
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