namespace LTTDataCenter.DTOs.Requests.Create
{
    public class CreateInvoice
    {
        public DateTime Date { get; set; }
       // public decimal TotalAmount { get; set; }
        public string? Description { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsPaid { get; set; }

        

    }
}
