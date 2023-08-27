using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Shared.Exceptions;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentativeFileById;

public sealed record FetchRepresentativeFileByIdQueryHandler : IRequestHandler<FetchRepresentativeFileByIdQuery, FileStreamResult>
{
    private readonly AppDbContext _dbContext;

    public FetchRepresentativeFileByIdQueryHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<FileStreamResult> Handle(FetchRepresentativeFileByIdQuery request, CancellationToken cancellationToken)
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