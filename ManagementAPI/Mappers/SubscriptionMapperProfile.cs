using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;

namespace ManagementAPI.Mappers;

public class SubscriptionMapperProfile:Profile
{
    public SubscriptionMapperProfile()
    {
        CreateMap<CreateSubscriptionRequestDto, Subscription>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));
        
        CreateMap<FileRequestDto, SubscriptionFile>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1))
            .ForMember(dest => dest.FileName,opt => opt.Ignore())
            .ForMember(dest => dest.FileType, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<FormFile, SubscriptionFile>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.FileName, opt => opt.Ignore())
            .ForMember(dest => dest.FileType, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active));
       

        CreateMap<Subscription,SubscriptionRsponseDto > ()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(x => x.Service.Name))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Customer.Name));

        CreateMap<SubscriptionFileResponsDto, SubscriptionFile>().ReverseMap();
        CreateMap<UpdateSubscriptionRequestDto, Subscription>();

    }
}
