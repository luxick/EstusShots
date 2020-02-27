using AutoMapper;
using EstusShots.Server.Models;

namespace EstusShots.Server.Mapping
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Season, Shared.Dto.Season>();
            CreateMap<Shared.Dto.Season, Season>();

            CreateMap<Episode, Shared.Dto.Episode>();
            CreateMap<Shared.Dto.Episode, Episode>();
        }
    }
}