using Microsoft.AspNetCore.Http;

namespace Shared.Dtos;

public record FileRequestDto(IFormFile File, short DocType);

