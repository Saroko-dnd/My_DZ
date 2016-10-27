using SimpleChat.ClassesForChat;
using SimpleChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleChat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            UserChatPage NewUserChatPage = new UserChatPage();
            AccessorToApplicationForChat CurrentAccessorToApplicationForChat = new AccessorToApplicationForChat();
            NewUserChatPage.LastMessages = CurrentAccessorToApplicationForChat.GetLastMessages();
            return View(NewUserChatPage);
        }

        [HttpPost]
        public ActionResult AddNewMessage(UserChatPage CurrentUserPage_)
        {
            AccessorToApplicationForChat CurrentAccessorToApplicationForChat = new AccessorToApplicationForChat();
            CurrentAccessorToApplicationForChat.AddNewMessageToChat(CurrentUserPage_.UserMessage, CurrentUserPage_.UserName);
            CurrentUserPage_.LastMessages = CurrentAccessorToApplicationForChat.GetLastMessages();
            ModelState.Clear();
            return View("Index", CurrentUserPage_);
        }

        [HttpPost]
        public ActionResult RefreshMessagesOnUserPage(UserChatPage CurrentUserPage)
        {
            AccessorToApplicationForChat CurrentAccessorToApplicationForChat = new AccessorToApplicationForChat();
            CurrentUserPage.LastMessages = CurrentAccessorToApplicationForChat.GetLastMessages();
            return View("Index");
        }
    }
}