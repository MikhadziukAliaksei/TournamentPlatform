using System.Threading.Tasks;

namespace TournamentPlatform.BLL.Services.Interface
{
    public interface ITeamInfoService
    {
        Task UpdateTeamInfoFromBracket(string bracketJson);
    }
}