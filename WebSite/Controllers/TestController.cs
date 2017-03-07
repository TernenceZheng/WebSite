using System;
using System.Web.Mvc;
using WebSite.Models.ViewModels;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace WebSite.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult AllPayWithdrawBalanceComplete()
        {

            return View();
        }

        public ActionResult CheckOverseasIDNO()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOverseasIDNO(Company company)
        {
            bool result = CheckUniformID(company.IDNO.ToUpper());

            ViewBag.IDNO = result.ToString();
            return View();
        }

        private bool CheckUniformID(string uniformId)
        {
            var regex = new Regex(@"^[a-zA-Z]{1}[a-dA-D1-2]{1}[0-9]{8}$");

            if (!regex.IsMatch(uniformId))
            {
                return false;   //Regular Expression 驗證失敗，回傳 ID 錯誤
            }

            int[] seed = new int[3];       //除了檢查碼外每個數字的存放空間
            string[] charMapping = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "W", "Z", "I", "O" };
            //A=10 B=11 C=12 D=13 E=14 F=15 G=16 H=17 J=18 K=19 L=20 M=21 N=22
            //P=23 Q=24 R=25 S=26 T=27 U=28 V=29 X=30 Y=31 W=32  Z=33 I=34 O=35
            string firstEN = uniformId.Substring(0, 1);

            for (int index = 0; index < charMapping.Length; index++)
            {
                if (charMapping[index] == firstEN)
                {
                    index += 10;
                    seed[0] = index / 10;
                    seed[1] = (index % 10);
                    break;
                }
            }
            string secondEN = uniformId.Substring(1, 1);
            for (int i = 0; i < charMapping.Length; i++)
            {
                if (charMapping[i] == secondEN)
                {
                    seed[2] = i;
                    break;
                }
            }

            int sum = seed[0] * 1 + seed[1] * 9 + seed[2]  * 8;

            for (int k = 3; k <= 9; k++)
            {
                sum +=int.Parse(uniformId.Substring(k - 1, 1)) * (10 - k);
            }

            int chkMac1 = 0;
            if (sum % 10 == 0)
            {
                chkMac1 = 0;
            }
            else
            {
                chkMac1 = (10 - (sum % 10));
            }

            int chkMac = Convert.ToInt32(uniformId.Substring(9, 1));

            return (chkMac == chkMac1);
        }

        [HttpPost]
        public ActionResult GoChkCompanyPhone(Company company)
        {
            Dictionary<string, string> postCollection = new Dictionary<string, string>();

            postCollection.Add("CellPhone", company.CellPhone.ToString());//選擇預設付款方式 
            postCollection.Add("AuthCompanyPhone", company.AuthCompanyPhone.ToString());//選擇預設付款方式 

            TempData["PostCollection"] = postCollection;
            TempData["PostURL"] = "http://devmember.allpay.com.tw:12004/MemberAuth/AuthCellPhoneCompanyRegister";
            //TempData["PostURL"] = "http://payment-beta.allpay.com.tw/Cashier/AioCheckOut";
            return RedirectToAction("AutoSubmitForm", "Common");
        
        }
    }
}