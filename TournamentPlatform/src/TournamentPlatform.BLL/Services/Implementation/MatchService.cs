using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.BLL.Models;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Implementation
{
    public class MatchService : BaseService<Match>, IMatchService
    {
        public MatchService(IRepository<Match> repository) : base(repository)
        {
        }

        public override Task<Match> GetByIdAsync(int id, Expression<Func<Match, bool>> predicate = null)
        {
            return _repository.GetByIdAsync(id, predicate,
                                            query => query.Include(match => match.TeamInfo)
                                                          .ThenInclude(teamInfo => teamInfo.Team)
                                                          .Include(match => match.Tournament)
                                                          .Include(match => match.NextMatch)
                                                          .Include(match => match.PrevMatches));
        }

        public override Task<IEnumerable<Match>> GetAllAsync(Expression<Func<Match, bool>> predicate = null)
        {
            return _repository.GetAllAsync(predicate, query => query.Include(match => match.TeamInfo)
                                                                    .ThenInclude(teamInfo => teamInfo.Team)
                                                                    .Include(match => match.Tournament)
                                                                    .Include(match => match.NextMatch)
                                                                    .Include(match => match.PrevMatches));
        }
    }
}