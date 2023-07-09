namespace Web.API.Options;

public class AuthenticationOption
{
    public const string Section = "Authentication";
    public int AllowedPeriodForReLoginIn { get; set; }
    public int NumberOfTimesAllowedToLogIn { get; set; }
    public int ConfirmationCodeExpirationTimeInSecond { get; set; }
    public required string HashedKey { get; set; }
    public int LockoutTimeInMinute { get; set; }
    public int LockoutCount { get; set; } = 5;
    public bool AutoAcceptable { get; set; }
    
    public string ValidAudience { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int TokenValidityInSecond { get; set; }
}