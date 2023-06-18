using Infrastructure.Models;
using Shared.Constants;

namespace ManagementAPI.Dtos;

public record FileRequestDto(IFormFile File, short DocType);

