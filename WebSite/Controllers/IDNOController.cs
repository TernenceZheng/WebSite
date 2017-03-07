using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models.ViewModels;

namespace WebSite.Controllers
{
    public class IDNOController : Controller
    {
        //
        // GET: /IDNO/


        public ActionResult Index()
        {
            ViewBag.String = "";

            return View();
        }

        [HttpPost]
        public ActionResult Index(Company company)
        {
            Random test = new Random();

            int 預設可整除總數 = 40;

            int A = test.Next(1, 9);
            int B = test.Next(1, 9) * 2;
            int C = test.Next(1, 9);
            int D = test.Next(1, 9) * 2;
            int E = test.Next(1, 9);
            int F = test.Next(1, 9) * 2;
            int G = test.Next(1, 7) * 4;

            if (B >=10)
            {
                int 十位數 = int.Parse( B.ToString().Substring(0, 1));
                int 個位數 = int.Parse( B.ToString().Substring(1,1));

                B = 十位數 + 個位數;
            }

            if (D >= 10)
            {
                int 十位數 = int.Parse(D.ToString().Substring(0, 1));
                int 個位數 = int.Parse(D.ToString().Substring(1, 1));

                D = 十位數 + 個位數;
            }

            if (F >= 10)
            {
                int 十位數 = int.Parse(F.ToString().Substring(0, 1));
                int 個位數 = int.Parse(F.ToString().Substring(1, 1));

                F = 十位數 + 個位數;
            }

            if (G >= 10)
            {
                int 十位數 = int.Parse(G.ToString().Substring(0, 1));
                int 個位數 = int.Parse(G.ToString().Substring(1, 1));

                G = 十位數 + 個位數;
            }



            int 餘額 = 預設可整除總數 - A - B - C - D - E - F - G;

            while (餘額 < 0)
            {
                if (A > 1)
                {
                    A = A - 1;
                    餘額 = 餘額 + 1;
                }
                if (C > 1)
                {
                    C = C - 1;
                    餘額 = 餘額 + 1;
                }
                if (E > 1)
                {
                    E = E - 1;
                    餘額 = 餘額 + 1;
                }
            }

            while (餘額 >=10)
            {
                if (A > 1)
                {
                    A = A + 1;
                    餘額 = 餘額 - 1;
                }
                if (C > 1)
                {
                    C = C + 1;
                    餘額 = 餘額 - 1;
                }
                if (E > 1)
                {
                    E = E + 1;
                    餘額 = 餘額 - 1;
                }
            }


            string FinalString = A.ToString() + B.ToString() +C.ToString() + D.ToString() + E.ToString() + F.ToString() + G.ToString() + 餘額.ToString();

            company.UnifiedBusinessNo = int.Parse( FinalString);

            ViewBag.String = FinalString;
            return View(company);
        }

    }
}
