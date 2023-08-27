using Microsoft.AspNetCore.Http;

namespace Core.Dtos;

public class FileRequestDto
{
    public IFormFile File { set; get; } = default!;
    public short DocType { get; set; }

}