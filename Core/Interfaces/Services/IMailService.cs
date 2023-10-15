namespace Core.Interfaces.Services;

public interface IMailService
{
    Task<bool> SendWelcomeEmail(string fullName, string email, string password);
    Task<bool> SendSubscriptionExpiringAlertAsync(string fullName, string email);
}