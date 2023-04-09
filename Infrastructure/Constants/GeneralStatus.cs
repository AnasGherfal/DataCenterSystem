namespace Infrastructure.Constants;

public enum GeneralStatus:short
{
    Entry = 0,
    Active = 1,
    NonActive = 2,
    Deleted = 3,
    Locked = 4,
    LockedByUser = 5,
    Reviewed = 6,
    Confirmed = 7,
    Rejected = 8,
    Transferred = 9,
    Canceled = 10,
    Paused = 11,
}