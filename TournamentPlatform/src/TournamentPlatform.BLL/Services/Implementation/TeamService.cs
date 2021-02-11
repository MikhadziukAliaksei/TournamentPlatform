using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Implementation
{
    public class TeamService : BaseService<Team>, ITeamService
    {
        private readonly IRepository<PlayerTeam> _playerTeamsRepository;
        private readonly IRepository<TournamentTeam> _tournamentTeamsRepository;

        public TeamService(
            IRepository<Team> repository,
            IRepository<PlayerTeam> playerTeamsRepository,
            IRepository<TournamentTeam> tournamentTeamsRepository) : base(repository)
        {
            _playerTeamsRepository = playerTeamsRepository;
            _tournamentTeamsRepository = tournamentTeamsRepository;
        }

        public override Task<Team> GetByIdAsync(int id, Expression<Func<Team, bool>> predicate = null)
        {
            return _repository.GetByIdAsync(id, predicate, query => query.Include(team => team.TeamInfo)
                                                                         .Include(team => team.TournamentsTeams)
                                                                         .Include(team => team.PlayersTeams));
        }

        public async Task<IEnumerable<Player>> GetTeamPlayersAsync(int id)
        {
            return (await _playerTeamsRepository.GetAllAsync(teamPlayer => teamPlayer.TeamId == id,
                                                             query => query.Include(team => team.Player)))
                .Select(teamPlayer => teamPlayer.Player);
        }

        public async Task<IEnumerable<Tournament>> GetTournamentsAsync(int id)
        {
            return (await _tournamentTeamsRepository.GetAllAsync(tournamentTeams => tournamentTeams.TeamId == id,
                                                                 query => query
                                                                          .Include(tournamentTeam => tournamentTeam.Tournament)
                                                                          .ThenInclude(tournament => tournament.Matches)
                                                                          .ThenInclude(match => match.TeamInfo)
                                                                          .ThenInclude(teamInfo => teamInfo.Team))
                   ).Select(tournamentTeam => tournamentTeam.Tournament)
                    .Select(tournament => new Tournament
                                          {
                                              Name = tournament.Name,
                                              EndDate = tournament.EndDate,
                                              Game = tournament.Game,
                                              GameId = tournament.GameId,
                                              Id = tournament.Id,
                                              LogoPath = tournament.LogoPath,
                                              Matches = tournament.Matches.Where(match => match.TeamInfo.Any(teamInfo => teamInfo.TeamId == id)),
                                              StartDate = tournament.StartDate,
                                              TournamentsTeams = tournament.TournamentsTeams
                                          });
        }

        public Task AddPlayerToTeam(int playerId, int teamId)
        {
            return _playerTeamsRepository.AddAsync(new PlayerTeam {PlayerId = playerId, TeamId = teamId});
        }

        public override Task<IEnumerable<Team>> GetAllAsync(Expression<Func<Team, bool>> predicate = null)
        {
            return _repository.GetAllAsync(predicate, query => query.Include(team => team.TeamInfo)
                                                                    .Include(team => team.TournamentsTeams)
                                                                    .Include(team => team.PlayersTeams));
        }
    }
}