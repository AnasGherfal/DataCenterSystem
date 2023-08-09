﻿using AutoMapper;
using Infrastructure.Constants;
using Infrastructure.Models;
using ManagementAPI.Dtos.Customer;
using ManagementAPI.Dtos.Representative;
using Shared.Constants;
using Shared.Dtos;

namespace ManagementAPI.Mappers;

public class RepresentativeProfileMapper : Profile
{
    public RepresentativeProfileMapper()
    {
        CreateMap<CreateRepresentativeRequestDto, Representative>()
           .ForMember(dest => dest.Id, opt => opt.Ignore())
           .ForMember(dest => dest.Status, opt => opt.MapFrom(x => (short)Status.Active))
           .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(x => DateTime.Now));


        CreateMap<Representative, RepresentativeResponseDto>()
            .ForMember(dest => dest.Files,opt => opt.MapFrom(x=>x.Files.Select(c=> new FileResponseDto() {Id=c.Id, FileName= c.Filename,DocType=c.DocType.ToString() })));
        CreateMap<RepresentativeVisit, RepresentativeResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Representative.Id))
            .ForMember(dest => dest.IdentityType, opt => opt.MapFrom(x => x.Representative.IdentityType))
            .ForMember(dest => dest.IdentityType, opt => opt.MapFrom(x => x.Representative.IdentityType))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(x => x.Representative.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(x => x.Representative.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(x => x.Representative.Email))
            .ForMember(dest => dest.IdentityNo, opt => opt.MapFrom(x => x.Representative.IdentityNo))
            .ForMember(dest => dest.PhoneNo, opt => opt.MapFrom(x => x.Representative.PhoneNo))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(x => x.Representative.Status))
            .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(x => x.Representative.Customer.Name));


        CreateMap<UpdateRepresentativeRequestDto, Representative>().ReverseMap();
        CreateMap<FormFile, RepresentativeFile>()
             .ForMember(dest => dest.Id, src => src.Ignore())
              .ForMember(dest => dest.Filename, src => src.Ignore())
             .ForMember(dest => dest.FilePath, src => src.Ignore())
              .ForMember(dest => dest.IsActive, src => src.MapFrom(x => (short)GeneralStatus.Active))
             .ForMember(dest => dest.CreatedOn, src => src.MapFrom(x => DateTime.UtcNow));

    }
}
