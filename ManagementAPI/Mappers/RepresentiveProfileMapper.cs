using AutoMapper;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representive;
using Shared.Constants;

namespace ManagementAPI.Mappers;

public class RepresentiveProfileMapper : Profile
{
    public RepresentiveProfileMapper()
    {
        CreateMap<CreateVisitRequestDto, Representive>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Status, opt => opt.MapFrom(x => (short)Status.Active))
           .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
           .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));


        CreateMap<Representive, VisitResponseDto>();
        CreateMap<UpdateVisitRequestDto, Representive>().ReverseMap();
    }
}
