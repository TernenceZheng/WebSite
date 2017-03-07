using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models.ViewModels;
using WebSite.Models;
using WebSite.Models.BLL;

namespace WebSite.Controllers
{
    public class ShowReturnController : BaseController
    {
        //
        // GET: /ShowReturn/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ShowOrder(GetFinishTrade tradeData)
        {
            return View(tradeData);
        }

        public ActionResult ShowOrderList()
        {
            return View();
        }
        public ActionResult ShowOrderListJSON()
        {
            int pageSize = 10;
            int totalCount = 0;
            JQueryDT dt = new JQueryDT(Request);
            GetFinishTradeService _GetFinishTrade = new GetFinishTradeService();

            List<GetFinishTrade> _List = _GetFinishTrade.ListProducts(dt.iDisplayStart, pageSize, ref totalCount);

            BaseModel _dt = FormatDTByObject<GetFinishTrade>(_List, dt, pageSize, totalCount);

            return Json(_dt, JsonRequestBehavior.AllowGet);
        }
    }
}
