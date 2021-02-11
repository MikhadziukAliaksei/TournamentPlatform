using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;
using TournamentPlatform.WebUI.Controllers.Extensions;
using TournamentPlatform.WebUI.Models;

namespace TournamentPlatform.WebUI.Controllers
{
    public class TournamentsController : Controller
    {
        private const string ImgPath = "/img/tournaments/";

        private readonly ITournamentService _tournamentService;
        private readonly IMatchService _matchService;
        private readonly IGameService _gameService;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IMapper _mapper;
        private readonly ITeamInfoService _teamInfoService;

        public TournamentsController(ITournamentService tournamentService,
                                     IMapper mapper, 
                                     IGameService gameService,
                                     IWebHostEnvironment hostEnvironment,
                                     IMatchService matchService,
                                     ITeamInfoService teamInfoService)
        {
            _tournamentService = tournamentService;
            _mapper = mapper;
            _gameService = gameService;
            _hostEnvironment = hostEnvironment;
            _matchService = matchService;
            _teamInfoService = teamInfoService;
        }

        // GET: TournamentsController
        public async Task<ActionResult> Index()
        {
            var tournaments = _mapper.Map<IEnumerable<TournamentViewModel>>(await _tournamentService.GetAllAsync());
            return View(tournaments);
        }

        // GET: TournamentsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var tournament = _mapper.Map<TournamentDetailsViewModel>(await _tournamentService.GetByIdAsync(id));
            tournament.Teams = await _tournamentService.GetTeams(tournament.Id);
            return View(tournament);
        }

        public async Task<ActionResult> GetBracket(int id)
        {
            var matches = await _matchService.GetAllAsync(match => match.TournamentId == id);
            return Content(await _tournamentService.GenerateBracketJson(matches));
        }

        // GET: TournamentsController/Create
        public async Task<ActionResult> Create()
        {
            var games = await _gameService.GetAllAsync();
            var tournamentViewModel = new TournamentViewModel{GamesSelectList = new SelectList(games, "Id", "Name")};
            return View(tournamentViewModel);
        }

        // POST: TournamentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TournamentViewModel tournamentViewModel, IFormFile logo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tournament = _mapper.Map<Tournament>(tournamentViewModel);
                    tournament.LogoPath = await logo.SaveImage(_hostEnvironment.WebRootPath,ImgPath);
                    await _tournamentService.AddAsync(tournament);
                    return RedirectToAction(nameof(Index));
                }

                return View(tournamentViewModel);
            }
            catch
            {
                return View(tournamentViewModel);
            }
        }

        public async Task<ActionResult> GenerateBracket(int tournamentId)
        {
            await _tournamentService.GenerateMatchesBracket(tournamentId);

            return RedirectToAction("Details", new {id = tournamentId});
        }

        public async Task<ActionResult> SaveBracket(string bracketJson)
        {
            await _teamInfoService.UpdateTeamInfoFromBracket(bracketJson);

            return Ok();
        }

        // GET: TournamentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TournamentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TournamentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TournamentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
