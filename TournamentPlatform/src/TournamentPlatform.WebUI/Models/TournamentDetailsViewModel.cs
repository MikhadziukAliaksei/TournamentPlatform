using System;
using System.Collections.Generic;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.WebUI.Models
{
    public class TournamentDetailsViewModel
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string LogoPath { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public int Id { get; set; }
    }
}