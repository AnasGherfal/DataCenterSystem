using AutoMapper;
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
           .ForMember(dest => dest.TotalMin, opt => opt.MapFrom(x => x.EndTime.TimeOfDay - x.StartTime.TimeOfDay))
           .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
           .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));

        CreateMap<Visit, VisitResponseDto>();
        CreateMap<UpdateVisitRequestDto, Visit>();
    }
}
