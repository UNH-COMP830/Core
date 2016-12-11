using GameSlam.Core.Models;
using GameSlam.Services.Services;
using GameSlam.Web.Models;
using GameSlam.Web.Workflow;
using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace GameSlam.Web.Controllers
{
    public class GameController : Controller
    {
        GameWorkflow gameWorkflow;
        public GameController(GameWorkflow gameWorkflow)
        {
            this.gameWorkflow = gameWorkflow;
        }

        // GET: Game
        public ActionResult Index()
        {
            ViewBag.Title = "GameSlam";
            ViewBag.HeaderTitle = "";
            return View();
        }

        // GET: Game/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var result = gameWorkflow.GetGame(id.Value);

            if (result == null)
            {
                return HttpNotFound();
            }

            return View(result);
        }

        [Authorize(Roles = AccountService.AuthorizedUserStr)]                                          
        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = AccountService.AdminRoleStr)]                                               
        // GET: Game/Create
        public ActionResult PendingApproval()
        {
            var result = gameWorkflow.GetAllGamesSmallInfo(Core.Enums.ApprovalStatusEnum.PendingApproval);

            if (result == null)
            {
                return HttpNotFound();
            }
            ViewBag.Title = "Pending Approval";
            ViewBag.HeaderTitle = "Pending Approval";

            return View("../Home/Index", result);  
        }


        // POST: Game/Create 
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UploadGameViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                //check if model is valid
                if (ModelState.IsValid)
                {
                    int gameId = gameWorkflow.AddGame(model);
                    return RedirectToAction("Details", new { id = gameId });
                }
            }
            catch (Exception ex)
            {
                
                return View();
            }

            return View();
        }
         
        [AcceptVerbs(HttpVerbs.Post)] 
        [ValidateAntiForgeryToken]
        public ActionResult AdminApproveResponse(int gameId, bool approve)
        {     
            AdminApprovalResp responseMsg = null;

            RouteCollection routes = RouteTable.Routes;

            using (routes.GetWriteLock())
            {
                if (approve)
                    responseMsg = gameWorkflow.ApproveGame(gameId);
                else
                    responseMsg = gameWorkflow.DenyGame(gameId);
            }

            return Json(responseMsg);
        }
    }
}
