namespace Infrastructure.Constants;

[Flags]
public enum SystemPermissions : long
{
    None = 0,
    CustomerManagement = 1,
    VisitsManagement = 2,
    ServiceManagement = 4,
    InvoiceManagement = 8,
    SubscriptionManagement = 16,
    CompanionManagement = 32,
    RepresentativeManagement = 64,
    TimeShiftManagement = 128,
    AnalyticsManagement = 256,
    SuperAdmin = None | CustomerManagement 
                 | VisitsManagement | ServiceManagement 
                 | InvoiceManagement | SubscriptionManagement 
                 | CompanionManagement | RepresentativeManagement 
                 | TimeShiftManagement | AnalyticsManagement,
}

public static class SystemPermissionExtension
{
    public static string Name(this SystemPermissions claim) => claim switch
    {
        SystemPermissions.None => "None",
        SystemPermissions.CustomerManagement => "Customer Management",
        SystemPermissions.VisitsManagement => "Visits Management",
        SystemPermissions.ServiceManagement => "Service Management",
        SystemPermissions.InvoiceManagement => "Invoice Management",
        SystemPermissions.SubscriptionManagement => "Subscription Management",
        SystemPermissions.CompanionManagement => "Companion Management",
        SystemPermissions.RepresentativeManagement => "Representative Management",
        SystemPermissions.TimeShiftManagement => "Time Shift Management",
        SystemPermissions.AnalyticsManagement => "Analytics Management",
        SystemPermissions.SuperAdmin => "Super Admin",
        _ => throw new NotImplementedException(),
    };
}