using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Implementation
{
    public class PlayerService : BaseService<Player>, IPlayerService
    {
        public PlayerService(IRepository<Player> repository) : base(repository)
        {
        }

        public override Task<Player> GetByIdAsync(int id, Expression<Func<Player, bool>> predicate = null)
        {
            return _repository.GetByIdAsync(id, predicate, query => query.Include(player => player.PlayersTeams)
                                                                         .Include(player=>player.TeamInvites)
                                                                         .ThenInclude(teamInvite=>teamInvite.Team));
        }

        public async Task SetProfileImage(int id, Uri uri)
        {
            var user = await GetByIdAsync(id);
            user.ProfileImgUrl = uri.AbsoluteUri;
            await _repository.UpdateAsync(user);
        }

        public override Task<IEnumerable<Player>> GetAllAsync(Expression<Func<Player, bool>> predicate = null)
        {
            return _repository.GetAllAsync(predicate, query => query.Include(player => player.PlayersTeams));
        }
    }
}