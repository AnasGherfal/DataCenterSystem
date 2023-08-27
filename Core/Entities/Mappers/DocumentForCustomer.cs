using Core.Constants;

namespace Core.Entities.Mappers;

public class DocumentForCustomer
{
    public Guid Id { get; set; } = Guid.Empty;
    public Guid CustomerId { get; set; } = Guid.Empty;
    public DocumentType FileType { get; set; } = DocumentType.Passport;
    public string FileLink { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public bool IsActive { get; set; }
}