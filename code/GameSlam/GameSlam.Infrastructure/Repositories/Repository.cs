using GameSlam.Core.Enums;
using GameSlam.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;              

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
        }

        public IQueryable<GameDetail> GetAllGames()
        {
            return db.GameDetails;
        } 
        public IQueryable<ApplicationUser> GetAllUsers()
        {
            return db.Users;
        }
        public GameDetail GetGame(int gameId)
        {
            return db.GameDetails.Include("User").Include("Category").Include("Status").FirstOrDefault(x => x.Id == gameId);
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

        public AdminApprovalResp DenyGame(int gameId)
        {
            AdminApprovalResp resp = new AdminApprovalResp();

            var game = db.GameDetails.Where(g => g.Id == gameId).First();

            if(game == null)
            {
                resp.Status = false;
                resp.Message = "Could not locate game";
            }
            else if ( game.StatusId == (int)ApprovalStatusEnum.PendingApproval)
            {

                game.StatusId = (int)ApprovalStatusEnum.Declined;
                db.SaveChanges();

                resp.Status = true;
                resp.Message = "Game Status updated.";
            }
            else
            {         
                resp.Status = false;
                resp.Message = "Invalid transition State.";
            }

            return resp;
        }

        public AdminApprovalResp ApproveGame(int gameId)
        {
            AdminApprovalResp resp = new AdminApprovalResp();

            var game = db.GameDetails.Where(g => g.Id == gameId).First();

            if (game == null)
            {
                resp.Status = false;
                resp.Message = "Could not locate game";
            }
            else if (game.StatusId == (int)ApprovalStatusEnum.PendingApproval)
            {

                game.StatusId = (int)ApprovalStatusEnum.Approved;
                db.SaveChanges();

                resp.Status = true;
                resp.Message = "Game Status updated.";
            }
            else
            {
                resp.Status = false;
                resp.Message = "Invalid transition State.";
            }

            return resp;
        }
    }
}
