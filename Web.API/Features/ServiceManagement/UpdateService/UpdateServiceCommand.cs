using Common.Constants;
using MediatR;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.UpdateService;

public sealed record UpdateServiceCommand : IRequest<MessageResponse>
{
    public string? Id { get; private set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? AmountOfPower { get; set; } = string.Empty;
    public string? AcpPort { get; set; } = string.Empty;
    public string? Dns { get; set; } = string.Empty;
    public int? MonthlyVisits { get; set; }
    public decimal? Price { get; set; }
    
    public void SetId(string id)
    {
        Id = id;
    }
}