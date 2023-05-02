using AutoMapper;
using Infrastructure.Models;
using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Customer;

namespace ManagementAPI.Mappers;

public class CompanionProfileMapper : Profile
{
    public CompanionProfileMapper()
    {
        CreateMap<CreateCompanionRequestDto, Companion>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));
        CreateMap<Companion, CompanionResponseDto>();
        CreateMap<UpdateCompanionRequestDto, Companion>();
    }
}
