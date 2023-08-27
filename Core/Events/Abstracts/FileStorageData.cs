using Core.Constants;

namespace Core.Events.Abstracts;

public class FileStorageData
{
    public Guid FileIdentifier { get; set; } = Guid.Empty;
    public DocumentType FileType { get; set; } = DocumentType.Passport;
    public string FileLink { get; set; } = string.Empty;
}