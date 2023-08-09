namespace Infrastructure.Constants;

public enum StorageType
{
    SubscriptionFile,
}

public static class StringExtension
{
    public static string FolderName(this StorageType type)
    {
        return type switch
        {
            StorageType.SubscriptionFile => "SubscriptionFiles",
            _ => "",
        };
    }
}