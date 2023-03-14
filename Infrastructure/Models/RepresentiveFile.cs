namespace Infrastructure.Models;

public class RepresentiveFile : BaseModel
{
    public Guid Id { get; set; }
    public string Filename { get; set; } = string.Empty;
    public Byte[] Data { get; set; } = default!;
    public string FileType { get; set; } = string.Empty;
    public short DocType { get; set; }
    public int RepresintiveId { get; set; }

    //-----------Realations

    public Representive Representive { get; set; } = new Representive();
}
