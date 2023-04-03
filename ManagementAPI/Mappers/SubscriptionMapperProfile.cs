using AutoMapper;
using Infrastructure.Models;
using ManagementAPI.Dtos.Service;
using ManagementAPI.Dtos.Subscriptions;

namespace ManagementAPI.Mappers
{
    public class SubscriptionMapperProfile:Profile
    {
        public SubscriptionMapperProfile()
        {
            CreateMap<Subscription, CreateSubscriptionDto>().ReverseMap()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => 1))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 3));

            CreateMap<SubscriptionResponseDto, Subscription>().ReverseMap()
                .ForMember(dest => dest.ServiceName, opt => opt.MapFrom(x => x.Service.Name))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Customer.Name))
;


        }
    }
}
