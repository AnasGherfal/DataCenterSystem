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
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now));



        CreateMap<Customer, CustomerResponseDto>()
            //.ForMember(dest => dest.Files, opt => opt.MapFrom(x => x.Files.Select(p => new { p.Id, p.Filename,p.DocType}).ToList()))
            .ForMember(dest => dest.Subsicrptions, opt => opt.MapFrom(x => x.Subscriptions.Select(p => new CustomerSub(){
               Id = p.Id }).ToList()))
            .ForMember(dest => dest.Representative, opt => opt.MapFrom(x => x.Representatives.Select(p => p.Id).ToList()))
            .ForMember(dest => dest.Visits, opt=> opt.MapFrom(p => p.Subscriptions.SelectMany(p=>p.Visits.Select(p=>p.Id)).ToList()));
        CreateMap<UpdateCustomerRequestDto, Customer>();
        CreateMap<FormFile, CustomerFile>()
            .ForMember(dest => dest.Id, src => src.Ignore())
             .ForMember(dest => dest.Filename, src => src.Ignore())
            .ForMember(dest => dest.FilePath, src => src.Ignore())
            .ForMember(dest => dest.CreatedOn, src => src.MapFrom(x => DateTime.UtcNow));
    }
}
