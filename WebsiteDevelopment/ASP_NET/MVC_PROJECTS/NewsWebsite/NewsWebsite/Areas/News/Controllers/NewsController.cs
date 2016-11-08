﻿using NewsWebsite.Areas.Admin.Models;
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
        public ActionResult Index(bool? EditingEnabled)
        {
            AccessorToNewsWebsiteDBForMainPage CurrentAccessorToNewsWebsiteDBForMainPage = new AccessorToNewsWebsiteDBForMainPage();
            PageWithListOfNews CurrentPageWithListOfNews = new PageWithListOfNews(CurrentAccessorToNewsWebsiteDBForMainPage.GetAllNews());
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
            AccessorToNewsWebsiteDBForMainPage CurrentAccessorToNewsWebsiteDBForMainPage = new AccessorToNewsWebsiteDBForMainPage();
            IEnumerable<NewsInfrastructure.News> CollectionOfNewsWithSelectedType = CurrentAccessorToNewsWebsiteDBForMainPage.GetNewsByType((NewsForEditInEditorTemplateModel.NewsTypes)SelectedNewsType);
            return PartialView(ApplicationConstants.PathFromRouteToNewsCollectionPartialView, CollectionOfNewsWithSelectedType);
        }

        [HttpPost]
        [FilterForPrintPage]
        [FilterForNewsPartialSubmit]
        public ActionResult ShowSelectedNews(long SelectedNewsID)
        {
            AccessorToNewsWebsiteDBForMainPage CurrentAccessorToNewsWebsiteDBForMainPage = new AccessorToNewsWebsiteDBForMainPage();
            return View(CurrentAccessorToNewsWebsiteDBForMainPage.GetNewsByID(SelectedNewsID));
        }

        public ActionResult ShowNewsPrintVersion(long SelectedNewsID)
        {
            AccessorToNewsWebsiteDBForMainPage CurrentAccessorToNewsWebsiteDBForMainPage = new AccessorToNewsWebsiteDBForMainPage();
            return View(CurrentAccessorToNewsWebsiteDBForMainPage.GetNewsByID(SelectedNewsID));
        }
    }
}