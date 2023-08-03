using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Shared.Dtos;

public record FileRequestDto(IFormFile File, short DocType);

