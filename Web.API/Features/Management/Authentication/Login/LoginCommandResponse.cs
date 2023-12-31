﻿using MediatR;

namespace Web.API.Features.Authentication.Login;

public class LoginCommandResponse
{
    public string? AccessToken { get; set; }
    public DateTime? AccessTokenExpiresAt { get; set; }
}