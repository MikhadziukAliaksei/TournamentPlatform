using System.ComponentModel.DataAnnotations;

namespace TournamentPlatform.DL.Domain.IdentityDomains
{
    public class Login
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
