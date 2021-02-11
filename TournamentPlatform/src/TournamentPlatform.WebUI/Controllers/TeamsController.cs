using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;
using TournamentPlatform.DL.Domain.IdentityDomains;
using TournamentPlatform.WebUI.Controllers.Extensions;
using TournamentPlatform.WebUI.Models;

namespace TournamentPlatform.WebUI.Controllers
{
    public class TeamsController : Controller
    {
        private const string ImgPath = "/img/teams/";

        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeamsController(ITeamService teamService, 
                               IMapper mapper,
                               IWebHostEnvironment hostEnvironment, 
                               UserManager<ApplicationUser> userManager)
        {
            _teamService = teamService;
            _mapper = mapper;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
        }

        // GET: Teams
        public async Task<ActionResult> Index()
        {
            var teams = _mapper.Map<IEnumerable<TeamViewModel>>(await _teamService.GetAllAsync());
            return View(teams);
        }

        // GET: Teams/Details/5
        public async Task<ActionResult> Details(int id)
        {
            if(id == 0)
                return RedirectToAction(nameof(Index));

            var teamPlayersViewModel = _mapper.Map<TeamDetailsViewModel>(await _teamService.GetByIdAsync(id));
            teamPlayersViewModel.Players = await _teamService.GetTeamPlayersAsync(id);
            teamPlayersViewModel.Tournaments = await _teamService.GetTournamentsAsync(id);

            return View(teamPlayersViewModel);
        }

        // GET: Teams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TeamDetailsViewModel teamViewModel, IFormFile logoPath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    teamViewModel.CaptainId = (await _userManager.GetUserAsync(User)).PlayerId;
                    teamViewModel.LogoPath = await logoPath.SaveImage(_hostEnvironment.WebRootPath, ImgPath);

                    var team = _mapper.Map<Team>(teamViewModel);
                    await _teamService.AddAsync(team);
                    await _teamService.AddPlayerToTeam(teamViewModel.CaptainId, team.Id);

                    return RedirectToAction(nameof(Index));
                }

                return View(teamViewModel);
            }
            catch
            {
                return View(teamViewModel);
            }
        }

        // GET: Teams/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var team = _mapper.Map<TeamDetailsViewModel>(await _teamService.GetByIdAsync(id));
            return View(team);
        }

        // POST: Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TeamDetailsViewModel teamViewModel, IFormFile logoPath)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (logoPath != null)
                    {
                        string oldFilePath = ImgPath + teamViewModel.LogoPath;
                        if (System.IO.File.Exists(_hostEnvironment.WebRootPath + oldFilePath))
                        {
                            System.IO.File.Delete(_hostEnvironment.WebRootPath + oldFilePath);
                        }
                        
                        teamViewModel.LogoPath = await logoPath.SaveImage(_hostEnvironment.WebRootPath, ImgPath);
                    }

                    await _teamService.UpdateAsync(_mapper.Map<Team>(teamViewModel));

                    return RedirectToAction(nameof(Index));
                }

                return View(teamViewModel);
            }
            catch
            {
                return View(teamViewModel);
            }
        }

        // POST: Teams/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _teamService.DeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}