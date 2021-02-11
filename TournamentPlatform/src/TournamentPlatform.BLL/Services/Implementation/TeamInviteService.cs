using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DAL.Repository.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Implementation
{
    public class TeamInviteService:BaseService<TeamInvite>,ITeamInviteService
    {
        public TeamInviteService(IRepository<TeamInvite> repository) : base(repository)
        {
        }
    }
}