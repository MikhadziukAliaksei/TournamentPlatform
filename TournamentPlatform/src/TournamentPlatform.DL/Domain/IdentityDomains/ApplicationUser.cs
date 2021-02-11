using Microsoft.AspNetCore.Identity;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.DL.Domain.IdentityDomains
{
    public class ApplicationUser : IdentityUser
    {
        public int PlayerId { get; set; }
    }
}