using AutoMapper;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using Shared.Constants;
using System.Reflection.Metadata;

namespace ManagementAPI.Mappers;

public class CustomerProfileMapper : Profile
{
    public CustomerProfileMapper()
    {
        CreateMap<CreateCustomerRequestDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Files, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => Status.Active))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));



        CreateMap<Customer, CustomerResponseDto>()
<<<<<<< Updated upstream
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(x => x.Files.Select(p => p.Filename).ToList()));
=======
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(x => x.Files.Select(p => p.Filename).ToList()))
            .ForMember(dest => dest.Subsicrptions, opt => opt.MapFrom(x => x.Subscriptions.Select(p => p.Id).ToList()))
            .ForMember(dest => dest.Representative, opt => opt.MapFrom(x => x.Representatives.Select(p => p.Id).ToList()));
>>>>>>> Stashed changes
        CreateMap<UpdateCustomerRequestDto, Customer>();
    }
}
