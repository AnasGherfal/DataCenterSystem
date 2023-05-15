using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.User;

namespace ManagementAPI.Mappers;

public class UserMapperProfile:Profile
{
    public UserMapperProfile()
    {
        CreateMap<User, CreateUserRequestDto>().ReverseMap()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => GeneralStatus.Active))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.UtcNow))
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(x => 1));
        CreateMap<User, UserResponseDto>().ReverseMap();
        CreateMap<User,UpdateUserRequestDto> ().ReverseMap();
    }
}
