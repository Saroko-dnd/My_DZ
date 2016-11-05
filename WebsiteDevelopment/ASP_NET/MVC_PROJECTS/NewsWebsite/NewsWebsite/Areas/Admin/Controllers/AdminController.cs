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

        public ActionResult MenuForCreationgNews()
        {
            return View(new NewsForEditInEditorTemplateModel());
        }

        public ActionResult SaveChangesInNewsAfterEdit(string PropertyName, string NewValue, long SelectedNewsID)
        {
            AccessorToNewsWebsiteDBForMainPage TestObjectForGettingData = new AccessorToNewsWebsiteDBForMainPage();
            TestObjectForGettingData.UpdateNewsProperty(PropertyName, NewValue, SelectedNewsID);
            return RedirectToAction("Index", new { controller = "News", area = "News", EditingEnabled = true });
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