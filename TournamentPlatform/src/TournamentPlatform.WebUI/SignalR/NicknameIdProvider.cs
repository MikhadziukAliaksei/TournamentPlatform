using Microsoft.AspNetCore.SignalR;

namespace TournamentPlatform.WebUI.SignalR
{
    public class NicknameIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity.Name;
        }
    }
}