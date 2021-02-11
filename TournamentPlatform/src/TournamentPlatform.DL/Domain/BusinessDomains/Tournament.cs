using System;
using System.Collections.Generic;
using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class Tournament : IEntity
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public string LogoPath { get; set; }
        public IEnumerable<Match> Matches { get; set; }
        public IEnumerable<TournamentTeam> TournamentsTeams { get; set; }
        public int Id { get; set; }
    }
}