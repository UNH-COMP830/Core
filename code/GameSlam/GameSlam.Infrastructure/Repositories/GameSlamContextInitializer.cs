using GameSlam.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace GameSlam.Infrastructure.Repositories
{

    public class GameSlamContextInitializer: DropCreateDatabaseAlways<ApplicationDbContext>
    {
        //protected override void Seed(ApplicationDbContext context)
        //{
        //    var games = new List<GameDetail>
        //    {
        //       new GameDetail() { Title = "Title 1", Description = "" },
        //       new GameDetail() { Title = "Title 2", Description = "" },
        //    };

        //    games.ForEach(b => context.GameDetails.Add(b));
        //    context.SaveChanges();


        //    var externalResp = new List<PublicResponse>
        //    {
        //       new PublicResponse() { Content = "Comment 1", GameInfo = games[0],},
        //       new PublicResponse() { Content = "Comment 2", GameInfo = games[0],},
        //    };

        //    externalResp.ForEach(b => context.PublicResponse.Add(b));
        //    context.SaveChanges();

        //         /*
        //    var externalRespLink = new List<GameResponse>
        //    {
        //       new GameResponse() {  PublicResponses = externalResp[0]},
        //       new GameResponse() { GameInfo = games[0], PublicResponses = externalResp[1]},
        //    };

        //    //context.GameDetails.Add      

        //    externalRespLink.ForEach(b => context.GameResp.Add(b));
        //    context.SaveChanges();
        //    */

        //    base.Seed(context);
        //}
    }
}
