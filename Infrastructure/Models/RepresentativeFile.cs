using Infrastructure.Constants;

namespace Infrastructure.Models;

public class RepresentativeFile : BaseModel
{
    public Guid Id { get; set; }
    public string Filename { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public DocumentType DocType { get; set; }
    public GeneralStatus IsActive { get; set; }
    public Guid RepresentativeId { get; set; }
    public Representative Representative { get; set; } = default!;
}
