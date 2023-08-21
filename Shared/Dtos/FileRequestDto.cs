using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Dtos;

public class FileRequestDto
{
    public IFormFile File { set; get; } = default!;
    public short DocType { get; set; }

}