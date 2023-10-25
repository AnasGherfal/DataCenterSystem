using Core.Wrappers;
using MediatR;

namespace Web.API.Features.Consumer.Login;

public sealed record LoginCommand(string? Email, string? Password) 
    : IRequest<ContentResponse<LoginCommandResponse>>;