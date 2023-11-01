using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Features.Consumer.RepresentativesManagement.FetchMyRepresentativeFileById;
public sealed record FetchMyRepresentativeFileByIdQuery: IRequest<FileStreamResult>
{
    public string? RepresentativeId { get; set; }
    public string? FileId { get; set; }
}
