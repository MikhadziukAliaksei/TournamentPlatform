using System.Collections.Generic;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.WebUI.Models
{
    public class TeamDetailsViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CaptainId { get; set; }
        public Player Captain { get; set; }
        public IEnumerable<Player> Players { get; set; }
        public IEnumerable<Tournament> Tournaments { get; set; }
        public string LogoPath { get; set; }
        public int Id { get; set; }
    }
}