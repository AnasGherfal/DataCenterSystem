using Core.Dtos;
using MediatR;

namespace Web.API.Features.Authentication.ChangePassword;

public sealed record ChangePasswordCommand: IRequest<MessageResponse>
{
    public string? OldPassword { get; set; }
    public string? NewPassword { get; set; }
    public string? ConfirmPassword { get; set; }
}