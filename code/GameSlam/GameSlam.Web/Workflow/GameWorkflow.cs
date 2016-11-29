
using GameSlam.Core.Enums;
using GameSlam.Core.Models;
using GameSlam.Infrastructure.Repositories;
using GameSlam.Services;
using GameSlam.Web.Models;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
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
                CategoryId = (int)model.CategoryId,//repository.FindCategory(model.CategoryId),
                CreateTime = DateTime.Now,
                StatusId = (int)ApprovalStatusEnum.PendingApproval,//repository.FindApprovalStatus(ApprovalStatusEnum.PendingApproval),
                Description = model.Description,
                Title = model.Title,
                BlogStorageGuidId = UploadGameFilesHelper(model),
                UserId = System.Web.HttpContext.Current.User.Identity.GetUserId()//repository.GetUser(System.Web.HttpContext.Current.User.Identity.GetUserId())
            };
            
            return repository.AddGame(newGame).Id;
        }

        public GameDetailComplete GetGame(int id)
        {

            GameDetail gameDetail = repository.GetGame(id);
            BlobStorageRepository blogStorage = new BlobStorageRepository();

            if (gameDetail != null)
            {
                return new GameDetailComplete()
                {
                    GameInfo = gameDetail,
                    GameFilesInfo = blogStorage.GetAllBlogItems(gameDetail.BlogStorageGuidId)
                };
            }
            return null;
        }

        private string UploadGameFilesHelper(UploadGameViewModel model)
        {
            UploadFileDetails uploadDetail = new UploadFileDetails()
            {
                LinuxDetails = ConvertHttpFileToDomainSpecific(model.DownloadLinux),
                OsxDetails = ConvertHttpFileToDomainSpecific(model.Downloadosx),
                WindowsDetails = ConvertHttpFileToDomainSpecific(model.DownloadWindows)
            };

            uploadDetail.Screenshots.Add(ConvertHttpFileToDomainSpecific(model.ScreenShot1));
            uploadDetail.Screenshots.Add(ConvertHttpFileToDomainSpecific(model.ScreenShot2));
            uploadDetail.Screenshots.Add(ConvertHttpFileToDomainSpecific(model.ScreenShot3));
            uploadDetail.Screenshots.Add(ConvertHttpFileToDomainSpecific(model.ScreenShot4));
            uploadDetail.Screenshots.Add(ConvertHttpFileToDomainSpecific(model.ScreenShot5));
            uploadDetail.Screenshots.Add(ConvertHttpFileToDomainSpecific(model.ScreenShot6)); 

            BlobStorageRepository blogStorage = new BlobStorageRepository();
            return blogStorage.AddNewGameFiles(uploadDetail);
        }

        private UploadFileDetail ConvertHttpFileToDomainSpecific(HttpPostedFileBase uploadFileInfo)
        {                                     
            if (uploadFileInfo != null)
            {
                return new UploadFileDetail()
                {
                    FileData = StreamToByteArray(uploadFileInfo.InputStream),
                    FileExtention = Path.GetExtension(uploadFileInfo.FileName)
                };
            }
            return null;
        }

        private byte[] StreamToByteArray(Stream stream)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                stream.CopyTo(mem);
                return mem.ToArray();
            }
        } 
    }
}
