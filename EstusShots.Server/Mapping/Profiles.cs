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
            
            CreateMap<Player, Shared.Dto.Player>();
            CreateMap<Shared.Dto.Player, Player>();
            
            CreateMap<Drink, Shared.Dto.Drink>();
            CreateMap<Shared.Dto.Drink, Drink>();
            
            CreateMap<Enemy, Shared.Dto.Enemy>();
            CreateMap<Shared.Dto.Enemy, Enemy>();
        }
    }
}