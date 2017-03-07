using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models.BLL;

namespace WebSite.Controllers
{
    public class BonusController : Controller
    {
        //
        // GET: /Bonus/


        /// <summary>
        /// 先列出我的交易資料來源
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int MID, string Type)
        {
            IDAuthService idAuth = new IDAuthService();
            int result = idAuth.PassIDAuth(MID, Type);
            return Json(result);
        }
    }
}
