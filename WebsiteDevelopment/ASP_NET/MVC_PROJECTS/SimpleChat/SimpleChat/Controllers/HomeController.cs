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
            NewUserChatPage.LastMessages = CurrentAccessorToApplicationForChat.GetMessages();         
            return View(NewUserChatPage);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "AddNewMessage")]
        public ActionResult AddNewMessage(UserChatPage CurrentUserPage)
        {
            AccessorToApplicationForChat CurrentAccessorToApplicationForChat = new AccessorToApplicationForChat();
            CurrentAccessorToApplicationForChat.AddNewMessageToChat(CurrentUserPage.UserMessage, CurrentUserPage.UserName);
            CurrentUserPage.LastMessages = CurrentAccessorToApplicationForChat.GetMessages();
            AccessorToUserSession CurrentAccessorToUserSession = new AccessorToUserSession(Session);
            CurrentAccessorToUserSession.AddNewMessage(CurrentUserPage.UserMessage, CurrentUserPage.UserName);
            ModelState.Clear();
            return View("Index", CurrentUserPage);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "RefreshMessagesOnUserPage")]
        public ActionResult RefreshMessagesOnUserPage(UserChatPage CurrentUserPage)
        {
            if (CurrentUserPage.ReceiveOnlyMessagesCreatedByCurrentUser)
            {
                AccessorToUserSession CurrentAccessorToUserSession = new AccessorToUserSession(Session);
                CurrentUserPage.LastMessages = CurrentAccessorToUserSession.GetMessages();
            }
            else
            {
                AccessorToApplicationForChat CurrentAccessorToApplicationForChat = new AccessorToApplicationForChat();
                CurrentUserPage.LastMessages = CurrentAccessorToApplicationForChat.GetMessages();
            }
            ModelState.Clear();
            return View("Index", CurrentUserPage);
        }
    }
}