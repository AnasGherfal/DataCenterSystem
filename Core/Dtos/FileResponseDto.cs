namespace Core.Dtos;

public class FileResponseDto 
{
    public Guid Id { set; get; }
    public string FileName { set; get; }=string.Empty;
    public string DocType { set; get; } = string.Empty;
}
