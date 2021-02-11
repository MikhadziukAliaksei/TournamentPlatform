using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.WebUI.Models
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PlayerTeam> PlayersTeams { get; set; }
        public string AboutMe { get; set; }
        public string Email { get; set; }
        public string ProfileImgUrl { get; set; }
        public IEnumerable<TeamInvite> TeamInvites { get; set; }

    }
}
