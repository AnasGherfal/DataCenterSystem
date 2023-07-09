namespace Infrastructure.Constants;

public enum ClaimsKey
{
    IdentityId,
    DisplayName,
    Permissions,
    EmailVerified,
}

public static class ClaimsKeyExtension
{
    public static string Key(this ClaimsKey claim) => claim switch
        {
            ClaimsKey.IdentityId => "identity_id",
            ClaimsKey.DisplayName => "display_name",
            ClaimsKey.Permissions => "permissions",
            ClaimsKey.EmailVerified => "email_verified",
            _ => throw new NotImplementedException(),
        };
}