using Core.Wrappers;
using MediatR;

namespace Web.API.Features.TimeShiftManagement.DeleteTimeShift;

public sealed record DeleteTimeShiftCommand : IRequest<MessageResponse>
{
    public string? Id { get; set; }
}