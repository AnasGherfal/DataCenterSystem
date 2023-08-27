using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Features.RepresentativeManagement.FetchRepresentativeFileById;
public sealed record FetchRepresentativeFileByIdQuery: IRequest<FileStreamResult>
{
    public string? RepresentativeId { get; set; }
    public string? FileId { get; set; }
}
