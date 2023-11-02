using Core.Constants;

namespace Web.API.Features.Consumer.ServiceManagement.FetchServices
{
    public sealed record FetchServicesQueryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string AmountOfPower { get; set; } = string.Empty;
        public string AcpPort { get; set; } = string.Empty;
        public string Dns { get; set; } = string.Empty;
        public int MonthlyVisits { get; set; }
        public decimal Price { get; set; }
    }
}
