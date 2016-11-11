using NewsDataAccess;
using NewsWebsite.App_Start;
using NewsWebsite.ClassesForNewsWebsite;
using NewsWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using NewsInfrastructure;
using Ninject;

namespace NewsWebsite.Controllers
{
    public class MainPageController : Controller
    {
        INewsWebsiteDataManager CurrentNewsWebsiteDataManager;

        public MainPageController(INewsWebsiteDataManager SelecteNewsWebsitedManager)
        {
            CurrentNewsWebsiteDataManager = SelecteNewsWebsitedManager;
        }
        // GET: MainPage
        public ActionResult Index()
        {
            TestClassWithWebsiteData TestDataObject = new TestClassWithWebsiteData();
            TestDataObject.TestListOfNews = CurrentNewsWebsiteDataManager.GetHotNews();
            return View(TestDataObject);
        }
    }
}