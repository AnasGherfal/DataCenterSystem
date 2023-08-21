using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using Shared.Dtos;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;
using ManagementAPI.Dtos.Visit;
using ManagementAPI.Dtos.Companion;
using ManagementAPI.Dtos.Representative;
using Shared.Constants;

namespace ManagementAPI.Mappers;

public class SubscriptionMapperProfile:Profile
{
    public SubscriptionMapperProfile()
    {
        CreateMap<CreateSubscriptionRequestDto, Subscription>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SubscriptionFile,opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow));



        CreateMap<Subscription, SubscriptionRsponseDto>()
            .ForMember(dest=> dest.CustomerName,opt=>opt.MapFrom(x=>x.Customer.Name))
            .ForMember(dest => dest.File, opt => opt.MapFrom(x=> x.SubscriptionFile))
            .ForMember(dest=> dest.CustomerName, opt=> opt.MapFrom(x=>x.Customer.Name))
            .ForMember(dest=> dest.MonthlyVisits, opt => opt.MapFrom(x=> x.MonthlyVisits))
            .ForMember(dest=> dest.DaysRemaining,opt=>opt.MapFrom(x=>(x.EndDate-DateTime.UtcNow).Days))
           .ForMember(dest => dest.Visits, opt => opt.MapFrom(x => x.Visits.Where(p=>p.Status!=GeneralStatus.Deleted).Select(c => new VisitResponseDto()
            {
                Id = c.Id,
                StartTime = c.StartTime,
                EndTime = c.EndTime,
                Status = c.Status,
                TimeShift = c.TimeShift.Name,
                InvoiceId = c.InvoiceId,
                Notes = c.Notes,
                Price = c.Price,
                TotalMin = c.TotalMin,
               VisitType=c.VisitType.Id,
                Companions = c.Companions.Select(x => new CompanionResponseDto()
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    FullName = x.FullName,
                    IdentityNo = x.IdentityNo,
                    IdentityType = x.IdentityType,
                    JobTitle = x.JobTitle ?? "null"
                }).ToList(),
                Representatives = c.RepresentativesVisits.Select(x => new RepresentativeResponseDto()
                {
                    Id = x.Representative.Id,
                    FirstName = x.Representative.FirstName,
                    LastName = x.Representative.LastName,
                    Email = x.Representative.Email,
                    Status = x.Representative.Status,
                    IdentityNo = x.Representative.IdentityNo,
                    IdentityType = x.Representative.IdentityType,
                    PhoneNo = x.Representative.PhoneNo
                }).ToList()
            }).ToList()))
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(x => x.Service.Name))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Customer.Name));

        CreateMap<FormFile, SubscriptionFile>()
       .ForMember(dest => dest.Id, src => src.Ignore())
        .ForMember(dest => dest.FileName, src => src.Ignore())
       .ForMember(dest => dest.FilePath, src => src.Ignore())
       .ForMember(dest => dest.IsActive, src=> src.MapFrom(x=>(short)GeneralStatus.Active))
       .ForMember(dest => dest.CreatedOn, src => src.MapFrom(x => DateTime.UtcNow));
        CreateMap<SubscriptionFile, FileResponseDto>()
            .ForMember(dest => dest.FileName, opt => opt.MapFrom(x => x.FileName))
            .ForMember(dest => dest.DocType, opt => opt.MapFrom(x => DocType.SupscriptionFile.ToString()));

        CreateMap<UpdateSubscriptionRequestDto, Subscription>();

    }
}
