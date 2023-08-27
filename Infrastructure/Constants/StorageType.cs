namespace Infrastructure.Constants;

public enum StorageType
{
    SubscriptionFile,
    RepresentativeFile,
    CustomerFile,
}

public static class StringExtension
{
    public static string FolderName(this StorageType type)
    {
        return type switch
        {
            StorageType.SubscriptionFile => "SubscriptionFiles",
            StorageType.RepresentativeFile => "RepresentativeFiles",
            StorageType.CustomerFile => "CustomerFiles",
            _ => "",
        };
    }
}