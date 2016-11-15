using RaceInfrastructure;
using RaceWebsite.ClassesForRaceWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using RaceWebsite.Models;

namespace RaceWebsite.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        IRaceManager CurrentRaceManager;
        // GET: Admin/Admin
        public ActionResult Index()
        {            
            ModelState.Clear();
            return View(new RaceParticipantsModel(true, CurrentRaceManager));
        }

        public ActionResult StartNewRace(long NewFinishDistance)
        {
            CurrentRaceManager.StartRaceManagementAsync(NewFinishDistance);
            return PartialView(ApplicationConstants.PathFromRouteToRaceParticipantsPartialView, new RaceParticipantsModel(true, CurrentRaceManager));
        }

        public AdminController(IRaceManager NewRaceManager)
        {
            CurrentRaceManager = NewRaceManager;
        }       

    }
}