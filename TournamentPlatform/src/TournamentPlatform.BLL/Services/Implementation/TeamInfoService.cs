using System;
using System.Collections.Generic;
using System.Linq;
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
    public class TeamInfoService : BaseService<TeamInfo>, ITeamInfoService
    {
        private readonly IRepository<Match> _matchRepository;

        public TeamInfoService(IRepository<TeamInfo> repository, IRepository<Match> matchRepository) : base(repository)
        {
            _matchRepository = matchRepository;
        }

        public async Task UpdateTeamInfoFromBracket(string bracketJson)
        {
            var bracketData = JsonSerializer.Deserialize<BracketData>(bracketJson);
            var winQueue = new Queue<int?>();
            var column = 0;

            foreach (var results in bracketData.results.First())
            {
                foreach (var result in results)
                {
                    var id = result[2];
                    var match = await _matchRepository.GetByIdAsync(id.Value, null, query =>
                                                                                        query.Include(match => match.TeamInfo));
                    var firstTeamInfo = match.TeamInfo.ToList()[0];
                    firstTeamInfo.Score = result[0];
                    var secondTeamInfo = match.TeamInfo.ToList()[1];
                    secondTeamInfo.Score = result[1];

                    if (column > 0)
                    {
                        firstTeamInfo.TeamId = winQueue.Dequeue();
                        secondTeamInfo.TeamId = winQueue.Dequeue();
                    }


                    await _repository.UpdateAsync(firstTeamInfo);
                    await _repository.UpdateAsync(secondTeamInfo);

                    if (firstTeamInfo.Score == secondTeamInfo.Score ||
                        firstTeamInfo.Score == null ||
                        secondTeamInfo.Score == null)
                    {
                        winQueue.Enqueue(null);
                    }
                    else
                    {
                        if (firstTeamInfo.Score > secondTeamInfo.Score)
                        {
                            winQueue.Enqueue(firstTeamInfo.TeamId);
                        }

                        if (firstTeamInfo.Score < secondTeamInfo.Score)
                        {
                            winQueue.Enqueue(secondTeamInfo.TeamId);
                        }
                    }
                }

                column++;
            }
        }

        public override Task<TeamInfo> GetByIdAsync(int id, Expression<Func<TeamInfo, bool>> predicate = null)
        {
            return _repository.GetByIdAsync(id, predicate, query => query.Include(team => team.Match).Include(team => team.Team));
        }

        public override Task<IEnumerable<TeamInfo>> GetAllAsync(Expression<Func<TeamInfo, bool>> predicate = null)
        {
            return _repository.GetAllAsync(predicate, query => query.Include(team => team.Match).Include(team => team.Team));
        }
    }
}