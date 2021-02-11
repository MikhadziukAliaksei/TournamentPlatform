using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class PlayerTeam : IEntity
    {
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int Id { get; set; }
    }
}