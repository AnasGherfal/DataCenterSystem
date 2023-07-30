using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Visit;
using Shared.Constants;

namespace ManagementAPI.Mappers;

public class VisitProfileMapper:Profile
{
    public VisitProfileMapper()
    {
        CreateMap<CreateVisitRequestDto, Visit>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.InvoiceId, opt => opt.MapFrom(x => 0))
           .ForMember(dest => dest.TotalMin, opt => opt.MapFrom(x => x.EndTime.TimeOfDay - x.StartTime.TimeOfDay))
           .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
           .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active));


        CreateMap<Visit, VisitResponseDto>()
            .ForMember(p => p.Representatives, opt => opt.MapFrom(x => x.RepresentativesVisits))
            .ForMember(dest => dest.TimeShift, opt => opt.MapFrom(x => x.TimeShift.Name))
            .ForMember(dest => dest.VisitType, opt => opt.MapFrom(x => x.VisitType.Name))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.RepresentativesVisits.Select(p=>p.Representative.Customer.Name).Single()));


        CreateMap<UpdateVisitRequestDto, Visit>();

       
    }
    
   
}
