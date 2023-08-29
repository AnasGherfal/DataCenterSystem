using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Invoice;
using ManagementAPI.Dtos.Visit;

namespace ManagementAPI.Mappers;

public class InvoiceProfileMapper : Profile
{
    public InvoiceProfileMapper()
    {
        CreateMap<CreateInvoiceRequestDto, Invoice>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(x => x.EndDate))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(x => x.StartDate));

        CreateMap<Invoice, InvoiceResponseDto>()
            .ForMember(dest => dest.Visits, opt => opt.MapFrom(x => x.Visits))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Subscription.Customer.Name));
        
        CreateMap<UpdateInvoiceRequestDto, Invoice>();
    }
}
