namespace ManagementAPI.Dtos.Invoice;

public record FetchInvoicesResponseDto(int CurrentPage, int TotalPages, IList<InvoiceResponseDto> Content);