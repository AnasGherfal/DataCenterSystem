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
            .ForMember(dest => dest.Files, opt => opt.MapFrom(x => x.Files.Select(p => new FileResponseDto() { Id = p.Id, FileName = p.Filename, DocType = p.DocType.ToString() }).ToList()))
            .ForMember(dest => dest.Subsicrptions, opt => opt.MapFrom(x => x.Subscriptions.Select(p => new SubscriptionRsponseDto()
            {
                Id = p.Id,
                EndDate = p.EndDate,
                StartDate = p.StartDate,
                ServiceName = p.Service.Name,
                Status = (short)p.Status,
                Visits = p.Visits.Select(x => new VisitResponseDto() {
                    Id = x.Id,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Status = x.Status,
                    TimeShift = x.TimeShift.Name,
                    TotalMin = x.TotalMin,
                    InvoiceId = x.InvoiceId,
                    Notes = x.Notes,
                    Price = x.Price,
                    VisitType = x.VisitType.Name??"null",
                    Companions = x.Companions.Select(c => new CompanionResponseDto()
                    {
                        Id = c.Id,
                        FirstName = c.FirstName,
                        LastName = c.LastName,
                        FullName = c.FullName,
                        IdentityNo = c.IdentityNo,
                        IdentityType = c.IdentityType,
                        JobTitle = c.JobTitle??"null"
                    }).ToList(),
                    Representatives = x.RepresentativesVisits.Select(c => new RepresentativeResponseDto()
                    {
                        Id = c.Representative.Id,
                        FirstName = c.Representative.FirstName,
                        LastName = c.Representative.LastName,
                        Email = c.Representative.Email,
                        Status = c.Representative.Status,
                        IdentityNo = c.Representative.IdentityNo,
                        IdentityType = c.Representative.IdentityType,
                        PhoneNo = c.Representative.PhoneNo
                    }).ToList()
                }).ToList()
            }).ToList()))
            .ForMember(dest => dest.Representative, opt => opt.MapFrom(x => x.Representatives.Select(p => new RepresentativeResponseDto()
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                IdentityNo = p.IdentityNo,
                IdentityType = p.IdentityType,
                PhoneNo = p.PhoneNo,
                Status = p.Status
            }).ToList()));
           
        CreateMap<UpdateCustomerRequestDto, Customer>();
        CreateMap<FormFile, CustomerFile>()
            .ForMember(dest => dest.Id, src => src.Ignore())
             .ForMember(dest => dest.Filename, src => src.Ignore())
            .ForMember(dest => dest.FilePath, src => src.Ignore())
             .ForMember(dest => dest.IsActive, src => src.MapFrom(x => (short)GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, src => src.MapFrom(x => DateTime.UtcNow));
    }
}
