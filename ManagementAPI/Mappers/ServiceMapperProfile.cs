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
        CreateMap<Service, UpdateServiceDto>().ReverseMap();
    }
}
