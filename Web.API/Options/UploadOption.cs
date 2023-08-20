namespace Web.API.Options;

public class UploadOption
{
    public const string Section = "FileStorage";
    public decimal MaximumFileSizeInMb { get; set; } = 5;
}