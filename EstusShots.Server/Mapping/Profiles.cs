using AutoMapper;
using EstusShots.Server.Models;

namespace EstusShots.Server.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Season, Shared.Models.Season>();
            CreateMap<Shared.Models.Season, Season>();
        }
    }
}