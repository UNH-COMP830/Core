using GameSlam.Services.Services;
using GameSlam.Web.Models;
using GameSlam.Web.Workflow;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            return View();
        }

        // GET: Game/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize(Roles = AccountService.AuthorizedUserStr)]
        //[Authorize(Roles = AccountService.AdminRoleStr + "," + AccountService.AuthorizedUserStr)]
        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = AccountService.AdminRoleStr)]
        //[Authorize(Roles = AccountService.AdminRoleStr + "," + AccountService.AuthorizedUserStr)]
        // GET: Game/Create
        public ActionResult PendingApprovial()
        {
            return View();
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

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
