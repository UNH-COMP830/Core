using GameSlam.Web.Workflow;  
using System.Web.Mvc;

namespace GameSlam.Web.Controllers
{
    public class HomeController : Controller
    {
        GameWorkflow gameWorkflow;
        public HomeController(GameWorkflow gameWorkflow)
        {
            this.gameWorkflow = gameWorkflow;
        }

        public ActionResult Index()
        {
            var result = gameWorkflow.GetAllGamesSmallInfo(Core.Enums.ApprovalStatusEnum.Approved);

            if (result == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}