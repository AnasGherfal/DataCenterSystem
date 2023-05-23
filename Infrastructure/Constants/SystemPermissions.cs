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
    VisitManegment = 512,
    DeleteVsit = 1024,
    RepresentiveManegment = 2048,
    DeleteRepresentive = 4096,
    VisitShiftTime = 8192,
    DeleteVisitShiftTime = 16384,








}