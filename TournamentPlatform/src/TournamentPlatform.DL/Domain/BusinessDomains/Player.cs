using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TournamentPlatform.DL.Domain.IdentityDomains;
using TournamentPlatform.DL.Interfaces;



namespace TournamentPlatform.DL.Domain.BusinessDomains
{
    public class Player : IEntity
    {
        public string Name { get; set; }
        public IEnumerable<PlayerTeam> PlayersTeams { get; set; }
        public IEnumerable<TeamInvite> TeamInvites { get; set; }
        public int Id { get; set; }

        public string AboutMe { get; set; }

        public string Email { get; set; }

        public string ProfileImgUrl { get; set; }

    }
}