using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Interface
{
    public interface ITournamentService:IBaseService<Tournament>
    {
        public Task<IEnumerable<Team>> GetTeams(int id);
        public Task<IEnumerable<Match>> GenerateMatchesBracket(int tournamentId);
        public Task<string> GenerateBracketJson(IEnumerable<Match> matches);
    }
}