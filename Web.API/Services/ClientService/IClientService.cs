namespace Web.API.Services.ClientService;

public interface IClientService
{
    public string IdentityId { get; }
    public string DisplayName { get; }
    public string Email { get; }
    public bool EmailVerified { get; }
    public long Permission { get; }
    Guid GetIdentifier();
}