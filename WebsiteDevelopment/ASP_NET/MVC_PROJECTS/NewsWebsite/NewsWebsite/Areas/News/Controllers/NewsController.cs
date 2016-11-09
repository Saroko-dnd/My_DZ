
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
        // GET: News/News
        public ActionResult Index(bool? EditingEnabled)
        {
            INewsWebsiteDataManager CurrentNewsWebsiteDataManager = NinjectWebCommon.NinjectKernel.Get<INewsWebsiteDataManager>();
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
            INewsWebsiteDataManager CurrentNewsWebsiteDataManager = NinjectWebCommon.NinjectKernel.Get<INewsWebsiteDataManager>();
            IEnumerable<NewsInfrastructure.News> CollectionOfNewsWithSelectedType = CurrentNewsWebsiteDataManager.GetNewsByType((Enums.NewsTypes)SelectedNewsType);
            return PartialView(ApplicationConstants.PathFromRouteToNewsCollectionPartialView, CollectionOfNewsWithSelectedType);
        }

        [HttpPost]
        [FilterForPrintPage]
        [FilterForNewsPartialSubmit]
        public ActionResult ShowSelectedNews(long SelectedNewsID)
        {
            INewsWebsiteDataManager CurrentNewsWebsiteDataManager = NinjectWebCommon.NinjectKernel.Get<INewsWebsiteDataManager>();
            return View(CurrentNewsWebsiteDataManager.GetNewsByID(SelectedNewsID));
        }

        public ActionResult ShowNewsPrintVersion(long SelectedNewsID)
        {
            INewsWebsiteDataManager CurrentNewsWebsiteDataManager = NinjectWebCommon.NinjectKernel.Get<INewsWebsiteDataManager>();
            return View(CurrentNewsWebsiteDataManager.GetNewsByID(SelectedNewsID));
        }
    }
}