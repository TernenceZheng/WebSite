using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.SignalR;

namespace WebSite.Controllers
{
    public class SignalRController : Controller
    {
        //
        // GET: /SignalR/

        public ActionResult ChatRoom()
        {
            return View();
        }
    }
}
