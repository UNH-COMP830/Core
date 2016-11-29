using GameSlam.Core.Enums;
using GameSlam.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            return db.GameDetails.Include("User").Include("Category").Include("Status").FirstOrDefault(x => x.Id == gameId);
            //return db.GameDetails.FirstOrDefault(x => x.Id == gameId);
        }

        public GameDetail AddGame(GameDetail newGame)
        {
            var newRow = db.GameDetails.Create();

            try
            {
                newRow.CategoryId = newGame.CategoryId;
                newRow.CreateTime = newGame.CreateTime;
                newRow.Description = newGame.Description;
                newRow.StatusId = newGame.StatusId;
                newRow.Title = newGame.Title;
                newRow.BlogStorageGuidId = newGame.BlogStorageGuidId;

                // this needs to be added in the same context as the usermanager
                newRow.UserId = newGame.UserId;

                //if (newGame.StatusId != null)
                //{
                //    db.ApprovalStatuses.Attach(newGame.StatusId);
                //    db.Entry(newGame.StatusId).State = System.Data.Entity.EntityState.Unchanged;
                //}
                //if (newGame.CategoryId != null)
                //{
                //    db.Categories.Attach(newGame.CategoryId);
                //    db.Entry(newGame.CategoryId).State = System.Data.Entity.EntityState.Unchanged;
                //}
                //if (newGame.UserId != null)
                //{
                //    db.Users.Attach(newGame.UserId);
                //    db.Entry(newGame.UserId).State = System.Data.Entity.EntityState.Unchanged;
                //}
                db.GameDetails.Add(newRow);
                db.SaveChanges();
                newGame.Id = newRow.Id;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }



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

        public List<GameDetail> FindGames(ApprovalStatusEnum approvalStatusEnum)
        {
            return db.GameDetails.Where(m => m.StatusId == (int)approvalStatusEnum).ToList();
        }
    }
}
