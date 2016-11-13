using RaceInfrastructure;
using RaceWebsite.Areas.Admin.Models;
using RaceWebsite.ClassesForRaceWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace RaceWebsite.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        IRaceManager CurrentRaceManager;
        // GET: Admin/Admin
        public ActionResult Index()
        {
            RaceBackgroundWorker TestRaceBackgroundWorker = new RaceBackgroundWorker();
            ThreadPool.QueueUserWorkItem(o => TestRaceBackgroundWorker.StartRaceManagement());
            List<Car> hhh = CurrentRaceManager.GetAllCars().ToList();
            ModelState.Clear();
            return View(new RaceAdminModel(hhh, true));
        }

        public AdminController(IRaceManager NewRaceManager)
        {
            CurrentRaceManager = NewRaceManager;
        }       

    }
}