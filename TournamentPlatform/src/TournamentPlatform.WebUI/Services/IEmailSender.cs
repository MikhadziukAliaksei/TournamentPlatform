using System.Threading.Tasks;

namespace TournamentPlatform.WebUI.Services
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}