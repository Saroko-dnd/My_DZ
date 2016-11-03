using NewsWebsite.Areas.Admin.Models;
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
            ModelForNewsCreationMenu CurrentModelForNewsCreationMenu = new ModelForNewsCreationMenu();
            CurrentModelForNewsCreationMenu.CurrentNews = new ClassesForNewsWebsite.NewsForEditInEditorTemplate();
            return View(CurrentModelForNewsCreationMenu);
        }
    }
}