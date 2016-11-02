using NewsWebsite.Areas.News.Models;
using NewsWebsite.ClassesForNewsWebsite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewsWebsite.Areas.News.Controllers
{
    public class NewsController : Controller
    {
        // GET: News/News
        public ActionResult Index()
        {
            AccessorToNewsWebsiteDBForMainPage CurrentAccessorToNewsWebsiteDBForMainPage = new AccessorToNewsWebsiteDBForMainPage();
            PageWithListOfNews CurrentPageWithListOfNews = new PageWithListOfNews(CurrentAccessorToNewsWebsiteDBForMainPage.GetAllNews());
            return View(CurrentPageWithListOfNews);
        }

        public ActionResult ShowSelectedNews()
        {
            AccessorToNewsWebsiteDBForMainPage CurrentAccessorToNewsWebsiteDBForMainPage = new AccessorToNewsWebsiteDBForMainPage();
            PageWithListOfNews CurrentPageWithListOfNews = new PageWithListOfNews(CurrentAccessorToNewsWebsiteDBForMainPage.GetAllNews());
            return View(CurrentPageWithListOfNews);
        }
    }
}