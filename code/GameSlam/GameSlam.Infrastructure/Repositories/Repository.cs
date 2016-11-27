using GameSlam.Core.Enums;
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

        ~Repository()
        {
        //    if (disposing)
        //    {
        //        if (_userManager != null)
        //        {
        //            _userManager.Dispose();
        //            _userManager = null;
        //        }

        //        if (_signInManager != null)
        //        {
        //            _signInManager.Dispose();
        //            _signInManager = null;
        //        }
        //    }

        //    base.Dispose(disposing);
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

        public GameDetail AddGame(GameDetail newGame)
        {     
            var newRow = db.GameDetails.Create();

            newRow.CategoryId = newGame.CategoryId;
            newRow.CreateTime = newGame.CreateTime;
            newRow.Description = newGame.Description;
            newRow.StatusId = newGame.StatusId;
            newRow.Title = newGame.Title;
            // this needs to be added in the same context as the usermanager
            newRow.UserId = newGame.UserId;
                                                             
            if (newGame.StatusId != null)
            {
                db.ApprovalStatuses.Attach(newGame.StatusId);
                db.Entry(newGame.StatusId).State = System.Data.Entity.EntityState.Unchanged;
            }
            if (newGame.CategoryId != null)
            {
                db.Categories.Attach(newGame.CategoryId);
                db.Entry(newGame.CategoryId).State = System.Data.Entity.EntityState.Unchanged;
            }
            if (newGame.UserId != null)
            {
                db.Users.Attach(newGame.UserId);
                db.Entry(newGame.UserId).State = System.Data.Entity.EntityState.Unchanged;
            }
            db.GameDetails.Add(newRow);
            db.SaveChanges();
            newGame.Id = newRow.Id;
                                 
            return newGame;
        }

        public ApprovalStatus FindApprovalStatus(ApprovalStatusEnum ae)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.ApprovalStatuses.Find((int)ae);
            }                   
        }

        public Category FindCategory(CategoryEnum cat)
        {
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            {
                return dbContext.Categories.Find((int)cat);
            }
        }

        public ApplicationUser GetUser(String userId)
        {  
            //dont use the UserManager to get the user information because that will cause 
            // dbContext conflicts :(
            using (ApplicationDbContext dbContext = new ApplicationDbContext())
            { 
                return db.Users.Where(m => m.Id == userId).First();
            }
        }
            
    }
}
