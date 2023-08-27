namespace Core.Options;

public class MailOption
{
    public const string Section = "Mail";
    public string From { get; set; } = string.Empty;
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; } = 0;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}