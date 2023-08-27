using Core.Dtos;
using MediatR;

namespace Web.API.Features.ServiceManagement.CreateService;

public sealed record CreateServiceCommand: IRequest<MessageResponse>
{
    public string? Name { get; set; }
    public string? AmountOfPower { get; set; }
    public string? AcpPort { get; set; }
    public string? Dns { get; set; }
    public int? MonthlyVisits { get; set; }
    public decimal? Price { get; set; }
}