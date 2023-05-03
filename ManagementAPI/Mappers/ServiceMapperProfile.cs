using AutoMapper;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;

namespace ManagementAPI.Mappers;

public class ServiceMapperProfile : Profile
{
    public ServiceMapperProfile()
    {
        CreateMap<Service, ServiceResponseDto>().ReverseMap();
        CreateMap<Service, CreateServiceDto>().ReverseMap();
            /*.ForMember(dest => dest.Status, opt => opt.MapFrom(x => 1))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 3));*/ 
        CreateMap<Service, UpdateServiceDto>().ReverseMap();
    }
}
