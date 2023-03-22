using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Models;

public class CustomerFile : BaseModel
{
    public int Id { get; set; }
    public string Filename  { get; set; }=string.Empty;
    public string FileType { get; set; } = string.Empty;
    public short DocType  { get; set; }
    public int CustomerId { get; set; }

    //[NotMapped]
    //public IFormFile CustmoerFile { get; set; } = default!; 
  

    //----------------Relations

    public Customer Customer { get; set; }

}
