using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class TournamentTeam : IEntity
    {
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int Id { get; set; }
    }
}