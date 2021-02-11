using System.Collections.Generic;
using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class Team : IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string LogoPath { get; set; }
        public int CaptainId { get; set; }
        public IEnumerable<TeamInfo> TeamInfo { get; set; }
        public IEnumerable<PlayerTeam> PlayersTeams { get; set; }
        public IEnumerable<TournamentTeam> TournamentsTeams { get; set; }
        public IEnumerable<TeamInvite> TeamInvites { get; set; }
        public int Id { get; set; }
    }
}