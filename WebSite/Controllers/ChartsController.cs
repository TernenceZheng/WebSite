using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Controllers
{
    public class ChartsController : Controller
    {
        //###圓餅圖
        public ActionResult PieChart()
        {
            return View();
        }

        //###長條圖
        public ActionResult BarChart()
        {
            return View();
        }


        //###折線圖
        public ActionResult LineChart()
        {
            return View();
        }
    }
}
