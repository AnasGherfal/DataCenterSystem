using Core.Dtos;
using MediatR;

namespace Web.API.Features.Authentication.Login;

public sealed record LoginCommand(string? Email, string? Password) 
    : IRequest<ContentResponse<LoginCommandResponse>>;