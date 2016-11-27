
using GameSlam.Core.Enums;
using GameSlam.Core.Models;
using GameSlam.Infrastructure.Repositories;
using GameSlam.Services;
using GameSlam.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Web;

namespace GameSlam.Web.Workflow
{
    public class GameWorkflow
    {
        private readonly ApplicationUserManager userManager;
        private readonly ApplicationSignInManager signInManager;
        private readonly IRepository repository;

        public GameWorkflow(IRepository repository, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            this.repository = repository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public int AddGame(UploadGameViewModel model)
        {

            GameDetail newGame = new GameDetail()
            {
                CategoryId = repository.FindCategory(model.CategoryId),
                CreateTime = DateTime.Now,
                StatusId = repository.FindApprovalStatus(ApprovalStatusEnum.PendingApproval),
                Description = model.Description,
                Title = model.Title
            };

            newGame.UserId = repository.GetUser(System.Web.HttpContext.Current.User.Identity.GetUserId());

            return repository.AddGame(newGame).Id;
        }

        private void uploadGameFiles(UploadGameViewModel model)
        {
            FileStorageDetail fDetails = new FileStorageDetail();

            //fDetails.

        }
    }
}
