using System.Collections.Generic;
using TournamentPlatform.DL.Interfaces;

namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class Game : IEntity
    {
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public IEnumerable<Tournament> Tournaments { get; set; }
        public int Id { get; set; }
    }
}