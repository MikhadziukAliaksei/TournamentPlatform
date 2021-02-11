using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.BLL.Models;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Implementation
{
    public class TournamentService : BaseService<Tournament>, ITournamentService
    {
        private readonly IRepository<Match> _matchRepository;
        private readonly IRepository<TournamentTeam> _tournamentTeamsRepository;

        public TournamentService(
            IRepository<Tournament> repository,
            IRepository<TournamentTeam> tournamentTeamsRepository,
            IRepository<Match> matchRepository) : base(repository)
        {
            _tournamentTeamsRepository = tournamentTeamsRepository;
            _matchRepository = matchRepository;
        }

        public override Task<Tournament> GetByIdAsync(int id, Expression<Func<Tournament, bool>> predicate = null)
        {
            return _repository.GetByIdAsync(id, predicate, query => query.Include(tournament => tournament.Matches)
                                                                         .Include(tournament => tournament.TournamentsTeams)
                                                                         .Include(tournament => tournament.Game));
        }

        public async Task<IEnumerable<Team>> GetTeams(int id)
        {
            return (await _tournamentTeamsRepository.GetAllAsync(tournamentTeam => tournamentTeam.TournamentId == id,
                                                                 query => query.Include(tournamentTeam => tournamentTeam.Team)))
                .Select(tournamentTeam => tournamentTeam.Team);
        }

        public async Task<IEnumerable<Match>> GenerateMatchesBracket(int tournamentId)
        {
            var teams = (await GetTeams(tournamentId)).ToList();
            var matchList = new List<Match>();

            for (var i = 1; i < teams.Count(); i += 2)
            {
                var match = new Match
                            {
                                TeamInfo = new[]
                                           {
                                               new TeamInfo
                                               {
                                                   TeamId = teams[i - 1].Id
                                               },
                                               new TeamInfo
                                               {
                                                   TeamId = teams[i].Id
                                               }
                                           },
                                Time = DateTime.Now,
                                TournamentId = tournamentId,
                                Level = 0
                            };
                matchList.Add(match);
                await _matchRepository.AddAsync(match);
            }

            await GenerateSubMatches(matchList);

            return matchList;
        }

        public async Task<string> GenerateBracketJson(IEnumerable<Match> matches)
        {
            var teamPairs = matches.Where(match => match.Level == 0)
                                   .Select(match => new[]
                                                    {
                                                        match.TeamInfo.First().Team.Name,
                                                        match.TeamInfo.Skip(1).First().Team.Name
                                                    });

            var scorePairs = new [] {matches.GroupBy(match => match.Level)
                                    .Select(group =>
                                                group.Select(match => new []
                                                                      {
                                                                          match.TeamInfo.First().Score,
                                                                          match.TeamInfo.Skip(1).First().Score,
                                                                          match.Id
                                                                      })
                                           )};

            var jsonBracket = JsonSerializer.Serialize(new BracketData {teams = teamPairs, results = scorePairs});

            return jsonBracket;
        }

        public override Task<IEnumerable<Tournament>> GetAllAsync(Expression<Func<Tournament, bool>> predicate = null)
        {
            return _repository.GetAllAsync(predicate, query => query.Include(tournament => tournament.Matches)
                                                                    .Include(tournament => tournament.TournamentsTeams)
                                                                    .Include(tournament => tournament.Game));
        }

        private async Task GenerateSubMatches(IEnumerable<Match> matches)
        {
            var newLevel = matches.Max(match => match.Level) + 1;
            var sourceMatchesList = matches.ToList();
            if (matches.Count() == 1)
            {
                return;
            }

            var matchList = new List<Match>();

            for (var i = 1; i < matches.Count(); i += 2)
            {
                var match = new Match
                            {
                                PrevMatches = new[] {sourceMatchesList[i - 1], sourceMatchesList[i]},
                                TeamInfo = new[]
                                           {
                                               new TeamInfo(),
                                               new TeamInfo()
                                           },
                                TournamentId = sourceMatchesList.First().TournamentId,
                                Level = newLevel
                            };

                matchList.Add(match);
                await _matchRepository.AddAsync(match);
            }

            await GenerateSubMatches(matchList);
        }
    }
}