using NewsWebsite.Areas.Admin.Models;
using NewsWebsite.ClassesForNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWebsite.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TestAction(string id, string name)
        {
            return RedirectToAction("Index", new { controller = "Admin", area = "Admin" });
        }

        public ActionResult MenuForCreationgNews()
        {
            return View(new NewsForEditInEditorTemplateModel());
        }

        [HttpPost]
        public ActionResult AddNewNewsToDatabase(NewsForEditInEditorTemplateModel CreatedNews)
        {
            AccessorToNewsWebsiteDBForMainPage TestObjectForGettingData = new AccessorToNewsWebsiteDBForMainPage();
            TestObjectForGettingData.AddNewNews(CreatedNews.GetPureNews());
            return RedirectToAction("Index", new { controller = "Admin", area = "Admin" });
        }
    }
}