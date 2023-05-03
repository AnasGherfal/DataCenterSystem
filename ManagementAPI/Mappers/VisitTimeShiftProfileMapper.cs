using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.VisitTimeShift;
using Shared.Constants;

namespace ManagementAPI.Mappers;

public class VisitTimeShiftProfileMapper: Profile
{
    public VisitTimeShiftProfileMapper()
    {
        CreateMap<CreateVisitTimeShiftRequestDto, VisitTimeShift>()
                  .ForMember(dest => dest.Id, opt => opt.Ignore())
                  .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
                  .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x=> DateTime.UtcNow))
                  .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));
        CreateMap<VisitTimeShift, VisitTimeShiftResponseDto>();
        CreateMap<UpdateVisitTimeShiftRequestDto, VisitTimeShift>();
    }
}
