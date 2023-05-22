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
        CreateMap<CreateRepresentiveRequestDto, Representive>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Status, opt => opt.MapFrom(x => (short)Status.Active))
           .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
           .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));


        CreateMap<Representive, RepresentiveResponseDto>();
        CreateMap<RepresentiveVisit, RepresentiveResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Representive.Id))
            .ForMember(dest => dest.IdentityType, opt => opt.MapFrom(x => x.Representive.IdentityType))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.Representive.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.Representive.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Representive.Email))
            .ForMember(dest => dest.IdentityNo, opt => opt.MapFrom(x => x.Representive.IdentityNo))
            .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(x => x.Representive.PhoneNo))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Representive.Status))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Representive.Customer.Name));


        CreateMap<UpdateRepresentiveRequestDto, Representive>().ReverseMap();
        
    }
}
