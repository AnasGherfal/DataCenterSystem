namespace Core.Constants;

public enum EventType: short
{
    AdminCreated = 1,
    AdminUpdated = 2,
    AdminLocked = 3,
    AdminUnlocked = 4,
    AdminDeleted = 5,
    AdminPasswordReset = 6,
    AdminPasswordChanged = 7,
    
    
    ServiceCreated = 1001,
    ServiceUpdated = 1002,
    ServiceLocked = 1003,
    ServiceUnlocked = 1004,
    ServiceDeleted = 1005,
    
    SubscriptionCreated = 2001,
    SubscriptionRenewed = 2002,
    SubscriptionLocked = 2003,
    SubscriptionUnlocked = 2004,
    SubscriptionDeleted = 2005,
    SubscriptionFileUpdated = 2006,
    SubscriptionRequested = 2007,
    SubscriptionApproved = 2008,
    SubscriptionRejected = 2009,
    
    
    RepresentativeCreated = 3001,
    RepresentativeUpdated = 3002,
    RepresentativeLocked = 3003,
    RepresentativeUnlocked = 3004,
    RepresentativeDeleted = 3005,
    RepresentativeFileUpdated = 3006,
    RepresentativeRequested = 3007,
    RepresentativeApproved = 3008,
    RepresentativeRejected = 3009,
    
    CustomerCreated = 4001,
    CustomerUpdated = 4002,
    CustomerLocked = 4003,
    CustomerUnlocked = 4004,
    CustomerDeleted = 4005,
    CustomerFileUpdated = 4006,

    TimeShiftCreated = 5001,
    TimeShiftUpdated = 5002,
    TimeShiftDeleted = 5003,
    
    VisitCreated = 6001,
    VisitStarted = 6002,
    VisitEnded = 6003,
    VisitDeleted = 6004,
    VisitRequested = 6005,
    VisitCancelled = 6006,
    VisitSigned = 6007,
    
    
    InvoiceCreated = 7001,
    InvoicePaid = 7002,
}