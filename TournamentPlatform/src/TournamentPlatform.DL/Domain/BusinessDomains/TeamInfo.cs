using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class TeamInfo : IEntity
    {
        public int? Score { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public int Id { get; set; }
    }
}