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

namespace NewsWebsite.Controllers
{
    public class MainPageController : Controller
    {
        // GET: MainPage
        public ActionResult Index()
        {
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            TestClassWithWebsiteData TestDataObject = new TestClassWithWebsiteData();
            AccessorToNewsWebsiteDBForMainPage TestObjectForGettingData = new AccessorToNewsWebsiteDBForMainPage();
            TestDataObject.TestListOfNews = TestObjectForGettingData.GetHotNews();
            return View(TestDataObject);
        }
    }
}