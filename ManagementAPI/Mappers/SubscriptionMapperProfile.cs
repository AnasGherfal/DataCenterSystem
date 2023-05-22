﻿using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;

namespace ManagementAPI.Mappers;

public class SubscriptionMapperProfile:Profile
{
    public SubscriptionMapperProfile()
    {
        CreateMap<Subscription, FileDto>().ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));

        CreateMap<SubscriptionRsponseDto, Subscription>().ReverseMap()
            .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(x => x.Service.Name))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Customer.Name));

        CreateMap<SubscriptionFileResponsDto, SubscriptionFile>().ReverseMap();
        CreateMap<UpdateSubscriptionRequestDto, Subscription>().ReverseMap();

    }
}
