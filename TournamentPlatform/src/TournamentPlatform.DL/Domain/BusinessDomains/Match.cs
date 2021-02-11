using System;
using System.Collections.Generic;
using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class Match : IEntity
    {
        public DateTime Time { get; set; }
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; }
        public int? NextMatchId { get; set; }
        public Match NextMatch { get; set; }
        public int Level { get; set; }
        public IEnumerable<Match> PrevMatches { get; set; }
        public IEnumerable<TeamInfo> TeamInfo { get; set; }
        public int Id { get; set; }
    }
}