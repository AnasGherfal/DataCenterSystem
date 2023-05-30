using Common.Helpers;
using FluentValidation;
using ManagementAPI.Dtos.Companion;

namespace ManagementAPI.Dtos.Visit;
public class UpdateVisitRequestDto
{
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public TimeSpan? TotalMin { get; set; }
    public decimal Price { get; set; }
    public string? Notes { get; set; }
    public short VisitTypeId { get; set; }
    public int TimeShiftId { get; set; }
    public int? InvoiceId { get; set; }
<<<<<<< Updated upstream
    public IList<int> Representives { get; set; }
=======
    public IList<int> Representatives { get; set; }
>>>>>>> Stashed changes
    public IList<CreateCompanionRequestDto> Companions { get; set; }
}
public class UpdateVisitValidator : AbstractValidator<UpdateVisitRequestDto>
{
    public UpdateVisitValidator()
    {

     
    }
}

