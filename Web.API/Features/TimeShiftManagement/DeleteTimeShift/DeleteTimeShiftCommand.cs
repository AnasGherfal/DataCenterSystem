using MediatR;
using Shared.Dtos;

namespace Web.API.Features.TimeShiftManagement.DeleteTimeShift;

public sealed record DeleteTimeShiftCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}