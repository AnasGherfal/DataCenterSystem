using Core.Exceptions;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeFileById;

public sealed record FetchMyRepresentativeFileByIdQueryHandler : IRequestHandler<FetchMyRepresentativeFileByIdQuery, FileStreamResult>
{
    private readonly AppDbContext _dbContext;

    public FetchMyRepresentativeFileByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileStreamResult> Handle(FetchMyRepresentativeFileByIdQuery request, CancellationToken cancellationToken)
    {
        var document = await _dbContext.DocumentForRepresentatives
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.FileId!)
                && p.RepresentativeId == Guid.Parse(request.RepresentativeId!), cancellationToken: cancellationToken);
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