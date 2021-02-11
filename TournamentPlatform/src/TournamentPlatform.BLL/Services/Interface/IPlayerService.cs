using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentPlatform.DL.Domain.BusinessDomains;

namespace TournamentPlatform.BLL.Services.Interface
{
    public interface IPlayerService : IBaseService<Player>
    {
        Task SetProfileImage(int id, Uri uri);
    }
}