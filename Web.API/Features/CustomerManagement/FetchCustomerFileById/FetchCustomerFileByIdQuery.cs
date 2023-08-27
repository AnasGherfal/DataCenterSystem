using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Features.CustomerManagement.FetchCustomerFileById;
public sealed record FetchCustomerFileByIdQuery: IRequest<FileStreamResult>
{
    public string? CustomerId { get; set; }
    public string? FileId { get; set; }
}
