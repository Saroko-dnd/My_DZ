
using NewsWebsite.ClassesForNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsInfrastructure;
using Ninject;
using NewsWebsite.App_Start;
using NewsWebsite.Areas.News.Models;

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
            INewsWebsiteDataManager CurrentNewsWebsiteDataManager = NinjectWebCommon.NinjectKernel.Get<INewsWebsiteDataManager>();
            NewsInfrastructure.News ChangedNews = CurrentNewsWebsiteDataManager.UpdateNewsProperty(PropertyName, NewValue, SelectedNewsID);
            return PartialView(ApplicationConstants.PathFromRouteToNewsPartialView, new NewsForPartialView(ChangedNews));
        }

        [HttpPost]
        public ActionResult AddNewNewsToDatabase(NewsInfrastructure.News CreatedNews)
        {
            INewsWebsiteDataManager CurrentNewsWebsiteDataManager = NinjectWebCommon.NinjectKernel.Get<INewsWebsiteDataManager>();
            CurrentNewsWebsiteDataManager.AddNewNews(CreatedNews);
            return RedirectToAction("Index", new { controller = "Admin", area = "Admin" });
        }
    }
}