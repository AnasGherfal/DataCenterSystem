using AutoMapper;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Mappers;

public class CustomerFileProfileMapper : Profile
{
    public CustomerFileProfileMapper()
    {
        CreateMap<FormFile, CustomerFile>()
            .ForMember(dest => dest.Id, src => src.Ignore())
             .ForMember(dest => dest.Filename, src => src.Ignore())
            .ForMember(dest => dest.FileType, src => src.Ignore())
            .ForMember(dest => dest.CreatedById, src => src.MapFrom(x => 1))
            .ForMember(dest => dest.CreatedOn, src => src.MapFrom(x => DateTime.UtcNow));
        
    }
}
