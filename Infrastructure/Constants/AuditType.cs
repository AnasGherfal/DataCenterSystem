namespace Infrastructure.Constants;

public enum AuditType
{
    None = 0,
    AdminCreated = 1,
    AdminUpdated = 2,
    AdminLocked = 3,
    AdminUnlocked = 4,
    AdminDeleted = 5,
    AdminPasswordReset = 6,
    CustomerCreated=7,
    CustomerUpdated=8,
    CustomerLocked=9,
    CustomerUnlocked=10,
    CustomerDeleted=11,

}