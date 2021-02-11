using AutoMapper;
using TournamentPlatform.DL.Domain.BusinessDomains;
using TournamentPlatform.WebUI.Models;

namespace TournamentPlatform.WebUI.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Team, TeamViewModel>().ReverseMap();
            CreateMap<Team, TeamDetailsViewModel>().ReverseMap();
            CreateMap<Tournament, TournamentViewModel>().ReverseMap();
            CreateMap<Tournament, TournamentDetailsViewModel>().ReverseMap();
            CreateMap<Player, ProfileViewModel>().ReverseMap();
        }
    }
}