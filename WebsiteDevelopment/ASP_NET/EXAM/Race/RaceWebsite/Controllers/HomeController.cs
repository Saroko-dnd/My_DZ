using RaceInfrastructure;
using RaceWebsite.ClassesForRaceWebsite;
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
            return View(new RaceParticipantsModel(false, CurrentRaceManager));
        }

        public ActionResult RacerInfo(long SelectedRacerID)
        {
            Racer SelectedRacer = CurrentRaceManager.RaceRepositories.RacerRepository.GetAll().Where(FoundRacer => FoundRacer.RacerID == SelectedRacerID).FirstOrDefault();
            RacerInfo CurrentRacerInfo = new RacerInfo(false, SelectedRacer);
            return View(CurrentRacerInfo);
        }

        public JsonResult RaceAlreadyExist()
        {
            if (CurrentRaceManager.NewRaceCanBeCreated)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CurrentRaceFinishDistance()
        {
            return Json(CurrentRaceManager.CurrentFinishDistance, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RaceEnded()
        {
            return PartialView(ApplicationConstants.PathFromRouteToRaceParticipantsPartialView, new RaceParticipantsModel(false, CurrentRaceManager));
        }

        public ActionResult MainPage()
        {
            return View();
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