namespace Web.API.Services.MailService;

public interface IMailService
{
    Task<bool> SendSubscriptionExpiringAlertAsync(string fullName, string email);
}