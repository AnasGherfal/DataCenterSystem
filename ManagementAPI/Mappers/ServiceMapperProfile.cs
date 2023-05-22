using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;

namespace ManagementAPI.Mappers;

public class ServiceMapperProfile : Profile
{
    public ServiceMapperProfile()
    {
        CreateMap<CreateServiceDto, Service>()
         .ForMember(dest => dest.Id, opt => opt.Ignore())
         .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
         .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
         .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));
        CreateMap<Service, ServiceResponseDto>().ReverseMap();
        CreateMap<Service, UpdateServiceDto>().ReverseMap();
    }
}
