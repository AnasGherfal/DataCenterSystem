namespace ManagementAPI.Dtos.Invoice;

public record UpdateInvoiceRequestDto(int id, DateTime StartDate, DateTime EndDate, string? Description, string? InvoiceNo);
