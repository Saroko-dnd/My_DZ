using RaceWebsite.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaceWebsite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [DisableGlobalFilterForIEUsers]
        public ActionResult ErrorPageForIEUsers()
        {
            return View();
        }
    }
}