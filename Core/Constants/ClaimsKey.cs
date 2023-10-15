namespace Core.Constants;

public enum ClaimsKey
{
    IdentityId,
    DisplayName,
    Permissions,
    Email,
    EmailVerified,
    UserType,
}

public static class ClaimsKeyExtension
{
    public static string Key(this ClaimsKey claim) => claim switch
        {
            ClaimsKey.IdentityId => "identity_id",
            ClaimsKey.DisplayName => "display_name",
            ClaimsKey.Permissions => "permissions",
            ClaimsKey.Email => "email",
            ClaimsKey.EmailVerified => "email_verified",
            ClaimsKey.UserType => "user_type",
            _ => throw new NotImplementedException(),
        };
}