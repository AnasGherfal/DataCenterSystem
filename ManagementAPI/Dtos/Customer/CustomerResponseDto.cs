namespace ManagementAPI.Dtos.Customer;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string PrimaryPhone { get; set; } = string.Empty;
    public string? SecondaryPhone { get; set; }
    public string Email { get; set; } = string.Empty;
    public short Status { get; set; }
}