namespace ManagementAPI.Dtos.Invoice;

public record UpdateInvoiceRequestDto(Guid Id, DateTime StartDate, DateTime EndDate, string? Description, string? InvoiceNo);
