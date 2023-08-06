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
    
    
    ServiceCreated = 1001,
    ServiceUpdated = 1002,
    ServiceLocked = 1003,
    ServiceUnlocked = 1004,
    ServiceDeleted = 1005,

}