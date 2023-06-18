using Common.Helpers;
using FluentValidation;
using ManagementAPI.Dtos.Companion;

namespace ManagementAPI.Dtos.Visit;
public class UpdateVisitRequestDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public short VisitTypeId { get; set; }
    public int? InvoiceId { get; set; }
    public IList<int> Representatives { get; set; } = new List<int>();
    public IList<CreateCompanionRequestDto> Companions { get; set; } = new List<CreateCompanionRequestDto>();
}
public class UpdateVisitValidator : AbstractValidator<UpdateVisitRequestDto>
{
    public UpdateVisitValidator()
    {

     
    }
}

