using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eInvoice.Model
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //Mapper.CreateMap<Team, TeamDTO>()
            //      .ForMember(dest => dest.Players, opt => opt.Ignore())
            //      .ForMember(dest => dest.FoundingDate, opt => opt.MapFrom(src => src.OrganizationDate));
        }
    }
}
