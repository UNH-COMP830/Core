using GameSlam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSlam.Infrastructure.Repositories
{
    public interface IRepository
    {
        IQueryable<GameDetail> GetAllGames();
        IQueryable<PublicResponse> GetAllGameResponses();
        IQueryable<ApplicationUser> GetAllUsers();
        GameDetail GetGame(int gameId);
    }
}
