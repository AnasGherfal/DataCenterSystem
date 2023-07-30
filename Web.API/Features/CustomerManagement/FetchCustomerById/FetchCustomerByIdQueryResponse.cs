using Infrastructure.Constants;

namespace Web.API.Features.CustomerManagement.FetchCustomerById;

public class FetchCustomerByIdQueryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = default!;
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; } = default!;
    public GeneralStatus Status { get; set; }
    public IList<Guid> Subsicrptions { get; set; } = new List<Guid>();
    public IList<Guid> Representative { get; set; } = new List<Guid>();
    public IList<string> FileName { get; set; } = new List<string>();
}
