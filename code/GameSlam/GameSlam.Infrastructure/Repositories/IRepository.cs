using GameSlam.Core.Enums;
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
        GameDetail AddGame(GameDetail newGame);
        ApprovalStatus FindApprovalStatus(ApprovalStatusEnum ae);
        Category FindCategory(CategoryEnum cat);
        ApplicationUser GetUser(String userId);
        List<GameDetail> FindGames(ApprovalStatusEnum approvalStatusEnum);
    }
}
