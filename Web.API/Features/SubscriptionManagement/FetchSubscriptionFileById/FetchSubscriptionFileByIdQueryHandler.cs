using Infrastructure;
using Infrastructure.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Shared.Dtos;
using Shared.Exceptions;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionFileById;

public sealed record FetchSubscriptionFileByIdQueryHandler : IRequestHandler<FetchSubscriptionFileByIdQuery, FileStreamResult>
{
    private readonly DataCenterContext _dbContext;

    public FetchSubscriptionFileByIdQueryHandler(DataCenterContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileStreamResult> Handle(FetchSubscriptionFileByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _dbContext.Subscriptions
            .Include(p => p.SubscriptionFile)
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id!), cancellationToken: cancellationToken);
        if (data == null) throw new NotFoundException("SERVICE_NOT_FOUND");
        if (data.Status != GeneralStatus.Active) throw new NotFoundException("SERVICE_NOT_ACTIVE");
        if (data.SubscriptionFile.IsActive != GeneralStatus.Active) throw new NotFoundException("FILE_NOT_ACTIVE");
        if (!File.Exists(data.SubscriptionFile.FilePath)) throw new NotFoundException("File not found: ");
        var fileContents = await File.ReadAllBytesAsync(data.SubscriptionFile.FilePath, cancellationToken);
        var stream = new MemoryStream(fileContents);
        return new FileStreamResult(stream, new MediaTypeHeaderValue("application/pdf"))
        {
            FileDownloadName = Path.GetFileName(data.SubscriptionFile.FilePath)
        };
    }
}