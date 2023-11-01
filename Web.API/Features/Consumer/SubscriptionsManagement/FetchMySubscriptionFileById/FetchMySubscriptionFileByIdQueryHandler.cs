using Core.Exceptions;
using Core.Interfaces.Services;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Web.API.Features.Consumer.SubscriptionsManagement.FetchMySubscriptionFileById;

public sealed record FetchMySubscriptionFileByIdQueryHandler : IRequestHandler<FetchMySubscriptionFileByIdQuery, FileStreamResult>
{
    private readonly AppDbContext _dbContext;
    private readonly IClientService _client;

    public FetchMySubscriptionFileByIdQueryHandler(AppDbContext dbContext, IClientService client)
    {
        _dbContext = dbContext;
        _client = client;
    }

    public async Task<FileStreamResult> Handle(FetchMySubscriptionFileByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await _dbContext.DocumentForSubscriptions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.FileId!)
                && p.SubscriptionId == Guid.Parse(request.SubscriptionId!), cancellationToken: cancellationToken);
        if (document == null) throw new NotFoundException("FILE_NOT_FOUND");
        if (!document.IsActive) throw new NotFoundException("FILE_WAS_REMOVED");
        if (!File.Exists(document.FileLink)) throw new NotFoundException("FILE_NOT_FOUND");
        var currentCustomer = await _dbContext.Subscriptions.AnyAsync(p => p.Id == document.SubscriptionId
                                                                           && p.CustomerId == _client.GetIdentifier(), cancellationToken: cancellationToken);
        if (!currentCustomer) throw new ForbiddenException("NOT_YOUR_SUBSCRIPTION_FILE");
        var fileContents = await File.ReadAllBytesAsync(document.FileLink, cancellationToken);
        var stream = new MemoryStream(fileContents);
        return new FileStreamResult(stream, new MediaTypeHeaderValue("application/pdf"))
        {
            FileDownloadName = Path.GetFileName(document.FileLink)
        };
    }
}