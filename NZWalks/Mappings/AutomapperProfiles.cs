using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Mappings
{
    public class AutomapperProfiles:Profile
    {
        public AutomapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
        }
    }
}
