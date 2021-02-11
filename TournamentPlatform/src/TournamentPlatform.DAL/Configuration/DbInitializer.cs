using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TournamentPlatform.DAL.Repository.Implementation;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;
using TournamentPlatform.DL.Domain.IdentityDomains;

namespace TournamentPlatform.DAL.Configuration
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IRepository<Player> repository)
        {
            if (!await roleManager.RoleExistsAsync("admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await userManager.FindByNameAsync(AdminInfo.Login) == null)
            {
                var player = new Player() { Email = AdminInfo.Email, Name = AdminInfo.Login };

                await repository.AddAsync(player);

                var admin = new ApplicationUser() { Email = AdminInfo.Email, UserName = AdminInfo.Login, EmailConfirmed = true, PlayerId = player.Id};
                var result = await userManager.CreateAsync(admin, AdminInfo.Password);
               
               
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }
        }
    }
}
