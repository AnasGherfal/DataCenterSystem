using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representative;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.Visit;
using Shared.Constants;
using Shared.Dtos;
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
            .ForMember(dest => dest.Files, opt => opt.MapFrom(x => x.Files.Where(p => p.IsActive != GeneralStatus.Deleted).Select(p => new FileResponseDto() { Id = p.Id, FileName = p.Filename, DocType = p.DocType.ToString() }).ToList()))
            .ForMember(dest => dest.Subsicrptions, opt => opt.MapFrom(x => x.Subscriptions.Where(s => s.Status != GeneralStatus.Deleted).Select(p => new CustomerSubscriptionRsponseDto()
            {
                Id = p.Id,
                CustomerName = x.Name,
                ServiceName = p.Service.Name,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                DaysRemainig = (p.EndDate - DateTime.UtcNow).Days,
                Status = p.Status
            })))
            .ForMember(dest => dest.Representative, opt => opt.MapFrom(x => x.Representatives.Where(p => p.Status != GeneralStatus.Deleted).Select(p => new RepresentativeResponseDto()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                IdentityNo = p.IdentityNo,
                IdentityType = p.IdentityType,
                PhoneNo = p.PhoneNo,
                Status = p.Status,
                Files = p.Files.Where(c => c.IsActive != GeneralStatus.Deleted).Select(x=> new FileResponseDto()
                {
                    Id=x.Id,
                    FileName=x.Filename,
                    DocType=x.DocType.ToString()
                }).ToList()
            }).ToList())); ;
           
        CreateMap<UpdateCustomerRequestDto, Customer>()
            .ForMember(dest => dest.Files, opt => opt.Ignore());
        CreateMap<FormFile, CustomerFile>()
            .ForMember(dest => dest.Id, src => src.Ignore())
             .ForMember(dest => dest.Filename, src => src.Ignore())
            .ForMember(dest => dest.FilePath, src => src.Ignore())
             .ForMember(dest => dest.IsActive, src => src.MapFrom(x => (short)GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, src => src.MapFrom(x => DateTime.UtcNow));
    }
}
