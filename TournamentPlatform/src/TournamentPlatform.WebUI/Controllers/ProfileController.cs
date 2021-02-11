using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;
using TournamentPlatform.WebUI.Controllers.Extensions;
using TournamentPlatform.WebUI.Models;

namespace TournamentPlatform.WebUI.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly IPlayerService _playerService;
        private readonly ITeamInviteService _teamInviteService;
        private readonly ITeamService _teamService;

        public ProfileController(
            IPlayerService playerService,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment,
            ITeamService teamService,
            ITeamInviteService teamInviteService)
        {
            _playerService = playerService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _teamService = teamService;
            _teamInviteService = teamInviteService;
        }

        public async Task<IActionResult> Index(int id)
        {
            var player = await _playerService.GetByIdAsync(id);

            var model = _mapper.Map<ProfileViewModel>(player);


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfilePhoto(ProfileViewModel model, IFormFile logoPath)
        {
            if (logoPath != null)
            {
                var oldFilePath = "/img/profile/" + model.ProfileImgUrl;
                if (System.IO.File.Exists(_hostEnvironment.WebRootPath + oldFilePath))
                {
                    System.IO.File.Delete(_hostEnvironment.WebRootPath + oldFilePath);
                }

                model.ProfileImgUrl = await logoPath.SaveImage(_hostEnvironment.WebRootPath, "/img/profile/");
            }


            await _playerService.UpdateAsync(_mapper.Map<Player>(model));

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> AcceptTeamInvite(int inviteId)
        {
            var teamInvite = await _teamInviteService.GetByIdAsync(inviteId);
            await _teamService.AddPlayerToTeam(teamInvite.PlayerId, teamInvite.TeamId);
            await _teamInviteService.DeleteAsync(inviteId);
            return RedirectToAction("Index", new {id = teamInvite.PlayerId});
        }

        [HttpGet]
        public async Task<IActionResult> DeclineTeamInvite(int inviteId)
        {
            var teamInvite = await _teamInviteService.GetByIdAsync(inviteId);
            await _teamInviteService.DeleteAsync(inviteId);
            return RedirectToAction("Index", new {id = teamInvite.PlayerId});
        }
    }
}