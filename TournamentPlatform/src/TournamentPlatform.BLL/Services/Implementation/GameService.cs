using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Implementation
{
    public class GameService : BaseService<Game>, IGameService
    {
        public GameService(IRepository<Game> repository) : base(repository)
        { }

        public override Task<IEnumerable<Game>> GetAllAsync(Expression<Func<Game, bool>> predicate = null)
        {
            return _repository.GetAllAsync(predicate,query => query.Include(game=>game.Tournaments));
        }

        public override Task<Game> GetByIdAsync(int id, Expression<Func<Game, bool>> predicate = null)
        {
            return _repository.GetByIdAsync(id, predicate, query => query.Include(game=>game.Tournaments));
        }
    }
}