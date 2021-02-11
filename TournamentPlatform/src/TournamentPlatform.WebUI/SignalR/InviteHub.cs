using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using TournamentPlatform.BLL.Services.Interface;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.WebUI.SignalR
{
    public class InviteHub:Hub
    {
        private readonly IPlayerService _playerService;
        private readonly IBaseService<TeamInvite> _teamInviteService;

        public InviteHub(IPlayerService playerService, IBaseService<TeamInvite> teamInviteService)
        {
            _playerService = playerService;
            _teamInviteService = teamInviteService;
        }

        public async Task SendInvite(string nickname, string teamId)
        {
            try
            {
                var player = (await _playerService.GetAllAsync(player => player.Name == nickname)).FirstOrDefault();

                if (player != null)
                {
                    var teamInvite = new TeamInvite
                                     {
                                         PlayerId = player.Id,
                                         TeamId = int.Parse(teamId)
                                     };

                    await _teamInviteService.AddAsync(teamInvite);

                    await Clients.User(nickname).SendAsync("SendInvite", nickname, teamId);
                }
            }
            catch (Exception exception)
            { }
        }
    }
}