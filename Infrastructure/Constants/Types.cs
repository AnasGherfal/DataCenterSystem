namespace Shared.Constants;

public enum IdentityType : short
{
    IdCard = 1,
    Passsport = 2
}
public enum DocType : short
{
    PersonalId = 1,
    CompanyLicense = 2,
    CompanyPermit = 3
}

public enum EntityType : short
{
    AdditionalPower = 1,
    Companion = 2,
    ContractFile = 3,
    Customer = 4,
    CustomerFile = 5,
    Invoice = 6,
    Representative = 7,
    RepresentativeFile = 8,
    Service = 9,
    Subscription = 10,
    SubscriptionFile = 11,
    User = 12,
    Visit = 13,
    VisitTimeShift = 14,
    VisitType = 15
}
public enum Action : short
{
    Create = 1,
    Update = 2,
    Delete = 3

}