using MediatR;
using Shared.Dtos;

namespace Web.API.Features.ServiceManagement.CreateService;

public sealed record CreateServiceCommand(string? Name, string? AmountOfPower, string? AcpPort, 
        string? Dns, int? MonthlyVisits, decimal? Price, IFormFile? File) 
    : IRequest<MessageResponse>;