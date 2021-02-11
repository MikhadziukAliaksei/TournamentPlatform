using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;
using TournamentPlatform.DL.Domain.IdentityDomains;
using TournamentPlatform.WebUI.Models;

namespace TournamentPlatform.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPlayerService _playerService;

        public UserController(UserManager<ApplicationUser> userManager, IPlayerService playerService)
        {
            _userManager = userManager;
            _playerService = playerService;
        }

        public async Task<IActionResult> Index() => View(await _userManager.Users.ToListAsync());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Login, Email = model.Email, EmailConfirmed = true };
                var player = new Player{Name = model.Login,Email = model.Email};
                var result = await _userManager.CreateAsync(user, model.Password);
                await _playerService.AddAsync(player);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user==null)
            {
                return NotFound();
            }

            var model = new EditViewModel {Id = user.Id, Email = user.Email, Login = user.UserName};
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.Login;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}
