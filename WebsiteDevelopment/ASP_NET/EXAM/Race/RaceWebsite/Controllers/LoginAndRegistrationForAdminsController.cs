using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RaceWebsite.Controllers
{
    public class LoginAndRegistrationForAdminsController : Controller
    {
        // GET: LoginAndRegistration
        public ActionResult PageForLoginAndRegistration()
        {
            return View();
        }

        public JsonResult AttemptToRegisterNewAdmin(string NewAdminName, string NewAdminPassword)
        {
            return null;
        }
    }
}