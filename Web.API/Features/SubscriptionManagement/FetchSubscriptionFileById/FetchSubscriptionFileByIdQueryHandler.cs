using Core.Exceptions;
using Infrastructure;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Web.API.Features.SubscriptionManagement.FetchSubscriptionFileById;

public sealed record FetchSubscriptionFileByIdQueryHandler : IRequestHandler<FetchSubscriptionFileByIdQuery, FileStreamResult>
{
    private readonly AppDbContext _dbContext;

    public FetchSubscriptionFileByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileStreamResult> Handle(FetchSubscriptionFileByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await _dbContext.DocumentForSubscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.FileId!)
                && p.SubscriptionId == Guid.Parse(request.SubscriptionId!), cancellationToken: cancellationToken);
        if (document == null) throw new NotFoundException("FILE_NOT_FOUND");
        if (!document.IsActive) throw new NotFoundException("FILE_WAS_REMOVED");
        if (!File.Exists(document.FileLink)) throw new NotFoundException("FILE_NOT_FOUND");
        var fileContents = await File.ReadAllBytesAsync(document.FileLink, cancellationToken);
        var stream = new MemoryStream(fileContents);
        return new FileStreamResult(stream, new MediaTypeHeaderValue("application/pdf"))
        {
            FileDownloadName = Path.GetFileName(document.FileLink)
        };
    }
}