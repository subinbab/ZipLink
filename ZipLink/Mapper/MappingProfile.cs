using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Models.Client;
using Models.DTO;

namespace ZipLink.Mapper
{

    // This class inherits from AutoMapper Profile class
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // This is how we map URLClient and URLDTO as well as 
            CreateMap<URLClient, URLDTO>() // Not mapped, need custom logic
            .ForMember(dest => dest.createdby, opt => opt.MapFrom(src=>src.clientIp)) // Not mapped, need custom logic
            .ForMember(dest => dest.createdDate, opt => opt.Ignore()) // Not mapped, need custom logic
            .ForMember(dest => dest.modifieddby, opt => opt.MapFrom(src => src.clientIp)) // Not mapped, need custom logic
            .ForMember(dest => dest.modifiedDate, opt => opt.Ignore()); // Not mapped, need custom logic

            CreateMap<URLDTO,URLClient > ().
                ForMember(dest => dest.shortenurl, opt => opt.MapFrom(src => src.shortenUrl));

            CreateMap<URLTrackingClient, URLTracking>().
                ForMember(dest => dest.shortenUrl, opt => opt.MapFrom(src => src.shortenUrl));
            CreateMap<List<URLTracking>, List<URLTracking>>();
            CreateMap<List<URLTracking>, List<URLTrackingClient>>();
            CreateMap<URLTracking, URLTrackingClient>()
            .ForMember(dest => dest.shortenUrl, opt => opt.MapFrom(src => src.shortenUrl))
            .ForMember(dest => dest.clickCount, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.impressionCount, opt => opt.MapFrom(src => src.impressionCount)
            ).ForMember(dest=>dest.createdBy,opt=>opt.MapFrom(src=>src.createdby))
            ;
        }
    }
}