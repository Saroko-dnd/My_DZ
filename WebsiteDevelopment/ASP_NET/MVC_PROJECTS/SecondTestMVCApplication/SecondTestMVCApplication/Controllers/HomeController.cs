using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SecondTestMVCApplication.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        /* public ActionResult Index()
         {
             return View();
         }*/

        /*public string Index()
        {
            return "<p>This image was provided by SecondTestMVCApplication (this is String from HomeController)</p><img src='~/Images/BrownChecker.png' width='120' height='120' alt='Checker'> ";
        } */
        public ViewResult About()
        {
            return View();
        }
        public ViewResult Contacts()
        {
            return View();
        }
        public ViewResult Index()
        {
            return View();
        }
    }
}