using Infrastructure.Models;
using Shared.Constants;

namespace ManagementAPI.Dtos.Customer;

public record FileRequestDto(IFormFile File, short DocType);

