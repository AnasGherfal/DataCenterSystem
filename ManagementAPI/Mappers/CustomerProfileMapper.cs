using AutoMapper;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using System.Reflection.Metadata;

namespace ManagementAPI.Mappers;

public class CustomerProfileMapper : Profile
{
    public CustomerProfileMapper() 
    {
        CreateMap<CreateCustomerRequestDto, Customer>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => 1))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));

        CreateMap<Customer, CustomerResponseDto>();
        CreateMap<EditCustomerRequestDto, Customer>();
    }
}
