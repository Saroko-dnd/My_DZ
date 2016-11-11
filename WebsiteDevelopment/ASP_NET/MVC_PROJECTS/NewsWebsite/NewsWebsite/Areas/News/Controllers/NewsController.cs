
using NewsWebsite.Areas.News.Models;
using NewsWebsite.ClassesForNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsInfrastructure;
using Ninject;
using NewsWebsite.App_Start;

namespace NewsWebsite.Areas.News.Controllers
{
    public class NewsController : Controller
    {
        INewsWebsiteDataManager CurrentNewsWebsiteDataManager;

        public NewsController(INewsWebsiteDataManager SelectedNewsWebsiteDataManager)
        {
            CurrentNewsWebsiteDataManager = SelectedNewsWebsiteDataManager;
        }
        // GET: News/News
        public ActionResult Index(bool? EditingEnabled)
        {
            PageWithListOfNews CurrentPageWithListOfNews = new PageWithListOfNews(CurrentNewsWebsiteDataManager.GetAllNews());
            if (EditingEnabled == true)
            {
                CurrentPageWithListOfNews.AdminIsHere = true;
            }
            else
            {
                CurrentPageWithListOfNews.AdminIsHere = false;
            }
            return View(CurrentPageWithListOfNews);
        }

        public ActionResult ReturnNewsWithSelectedType(int SelectedNewsType)
        {
            IEnumerable<NewsInfrastructure.News> CollectionOfNewsWithSelectedType = CurrentNewsWebsiteDataManager.GetNewsByType((Enums.NewsTypes)SelectedNewsType);
            return PartialView(ApplicationConstants.PathFromRouteToNewsCollectionPartialView, CollectionOfNewsWithSelectedType);
        }

        public ActionResult ReturnNewsWithSelectedHeader(string SelectedNewsHeader)
        {
            IEnumerable<NewsInfrastructure.News> CollectionOfNewsWithSelectedHeader = CurrentNewsWebsiteDataManager.GetNewsByHeader(SelectedNewsHeader);
            return PartialView(ApplicationConstants.PathFromRouteToNewsCollectionPartialView, CollectionOfNewsWithSelectedHeader);
        }

        [HttpPost]
        [FilterForPrintPage]
        [FilterForNewsPartialSubmit]
        public ActionResult ShowSelectedNews(long SelectedNewsID)
        {
            return View(CurrentNewsWebsiteDataManager.GetNewsByID(SelectedNewsID));
        }

        public ActionResult ShowNewsPrintVersion(long SelectedNewsID)
        {
            return View(CurrentNewsWebsiteDataManager.GetNewsByID(SelectedNewsID));
        }
    }
}