using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Interface
{
    public interface ITeamService : IBaseService<Team>
    {
        Task<IEnumerable<Player>> GetTeamPlayersAsync(int id);
        Task<IEnumerable<Tournament>> GetTournamentsAsync(int id);
        Task AddPlayerToTeam(int playerId, int teamId);
    }
}