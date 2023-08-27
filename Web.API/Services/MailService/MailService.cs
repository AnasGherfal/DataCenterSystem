using Microsoft.Extensions.Options;
using Web.API.Options;
using System.Reflection;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace Web.API.Services.MailService;

public class MailService: IMailService
{
    private readonly ILogger<MailService> _logger;
    private readonly MailOption _mailOption;
    public MailService(ILogger<MailService> logger, IOptions<MailOption> settings)
    {
        _logger = logger;
        _mailOption = settings.Value;
    }

    public async Task<bool> SendSubscriptionExpiringAlertAsync(string fullName, string email)
    {
        var mail = new MimeMessage();
        mail.From.Add(new MailboxAddress(_mailOption.From, _mailOption.Email));
        mail.To.Add(new MailboxAddress(fullName, email));
        mail.Subject = "OTP";
        var builder = new BodyBuilder();
        using (var sourceReader = File.OpenText($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Templates/SubscriptionAlert.html"))
        {
            builder.HtmlBody = (await sourceReader.ReadToEndAsync()).Replace("{{FullName}}", fullName);
        }
        mail.Body = builder.ToMessageBody();
        return await Send(mail);
    }

    private async Task<bool> Send(MimeMessage mailMessage)
    {
        using var client = new SmtpClient();
        try
        {
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await client.ConnectAsync(_mailOption.SmtpServer, _mailOption.Port, SecureSocketOptions.Auto);
            client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_mailOption.Email, _mailOption.Password);
            await client.SendAsync(mailMessage);
            return true;
        }
        catch
        {
            _logger.LogCritical("Infrastructure {name}, was unable to send.", "Mail");
            return false;
        }
        finally
        {
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}