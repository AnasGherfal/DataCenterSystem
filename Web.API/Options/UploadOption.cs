namespace Web.API.Options;

public class UploadOption
{
    public const string Section = "FileStorage";
    public int MaximumFileSizeInMb { get; set; } = 5;
}