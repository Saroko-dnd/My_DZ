
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
            return View(new NewsInfrastructure.News());
        }

        public ActionResult SaveChangesInNewsAfterEdit(string PropertyName, string NewValue, long SelectedNewsID)
        {
            AccessorToNewsWebsiteDBForMainPage TestObjectForGettingData = new AccessorToNewsWebsiteDBForMainPage();
            NewsInfrastructure.News ChangedNews = TestObjectForGettingData.UpdateNewsProperty(PropertyName, NewValue, SelectedNewsID);
            return PartialView(ApplicationConstants.PathFromRouteToNewsPartialView, new NewsForPartialView(ChangedNews));
            //return RedirectToAction("Index", new { controller = "News", area = "News", EditingEnabled = true });
        }

        [HttpPost]
        public ActionResult AddNewNewsToDatabase(NewsInfrastructure.News CreatedNews)
        {
            AccessorToNewsWebsiteDBForMainPage TestObjectForGettingData = new AccessorToNewsWebsiteDBForMainPage();
            TestObjectForGettingData.AddNewNews(CreatedNews);
            return RedirectToAction("Index", new { controller = "Admin", area = "Admin" });
        }
    }
}