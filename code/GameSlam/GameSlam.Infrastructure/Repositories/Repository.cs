using GameSlam.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSlam.Infrastructure.Repositories
{
    public class Repository : IRepository
    {
        ApplicationDbContext db;

        public Repository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IQueryable<GameDetail> GetAllGames()
        {
            return db.GameDetails;
        }
        public IQueryable<PublicResponse> GetAllGameResponses()
        {
            return db.PublicResponse;
        }
        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return db.Users;
        }
        public GameDetail GetGame(int gameId)
        {
            return db.GameDetails.FirstOrDefault(x => x.Id == gameId);
        }
    }
}
