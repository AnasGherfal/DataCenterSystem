namespace Web.API.Features.Consumer.Login;

public class LoginCommandResponse
{
    public string? AccessToken { get; set; }
    public DateTime? AccessTokenExpiresAt { get; set; }
}