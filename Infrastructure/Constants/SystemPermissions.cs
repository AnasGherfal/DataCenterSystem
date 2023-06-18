namespace Common.Constants;

[Flags]
public enum SystemPermissions : long
{
    None = 0,
    SuperAdmin = 1,
    CustomerManegment = 2,
    DeleteCustomer = 4,
    ServiceManegment = 8,
    DeleteService = 16,
    SubscriptionManegment = 32,
    DeleteSubscription = 64,
    CompainionManegment = 128,
    DeleteCompainion = 256,

}