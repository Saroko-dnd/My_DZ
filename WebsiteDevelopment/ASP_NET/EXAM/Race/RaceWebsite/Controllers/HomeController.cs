using RaceInfrastructure;
using RaceWebsite.Filters;
using RaceWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaceWebsite.Controllers
{
    public class HomeController : Controller
    {
        private IRaceManager CurrentRaceManager;
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RacerInfo()
        {
            RacerInfo CurrentRacerInfo = new RacerInfo(false, CurrentRaceManager.RaceRepository.AllRacers.FirstOrDefault());
            return View(CurrentRacerInfo);
        }

        [DisableGlobalFilterForIEUsers]
        public ActionResult ErrorPageForIEUsers()
        {
            return View();
        }

        public HomeController(IRaceManager NewRaceManager)
        {
            CurrentRaceManager = NewRaceManager;
        }
    }
}