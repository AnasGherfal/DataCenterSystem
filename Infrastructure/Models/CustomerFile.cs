using Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

public class CustomerFile : BaseModel
{
    public Guid Id { get; set; }
    public string Filename { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public short DocType  { get; set; }
    public GeneralStatus IsActive { get; set; }
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;

}
