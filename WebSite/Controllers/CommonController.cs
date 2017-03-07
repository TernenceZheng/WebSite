using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using WebSite.Models.ViewModels;

namespace WebSite.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        /// <summary>
        /// 自動Client Post
        /// </summary>
        /// <returns></returns>
        public ActionResult AutoSubmitForm(TempDataDictionary tempData)
        {

            if (TempData["PostCollection"] == null || TempData["PostURL"] == null)
            {
                return RedirectToAction("Index", "MemberEdit");
            }

            string FormID = "PostForm";
            string PostURL = Convert.ToString(TempData["PostURL"]);
            StringBuilder strForm = new StringBuilder();
            StringBuilder strScript = new StringBuilder();


            //建立Form HTML Tag
            strForm.Append("<form id=\"" + FormID + "\" name=\"" + FormID + "\" action=\"" + PostURL + "\" method=\"POST\">");

            foreach (var Item in TempData["PostCollection"] as Dictionary<string, string>)
            {
                strForm.Append("<input type=\"hidden\" name=\"" + Item.Key + "\" value=\"" + Item.Value + "\" />");
            }


            strForm.Append("<a type='button' id='SubmitForm' />");
            strForm.Append("</form>");


            //建立JavaScript
            strScript.Append("<script type=\"text/javascript\">");
            strScript.Append("$(function(){");
            strScript.Append("$('#SubmitForm').click(function(){");
            strScript.Append("var v" + FormID + " = document." + FormID + ";");
            strScript.Append("v" + FormID + ".submit();");
            strScript.Append("});$('#SubmitForm').trigger('click'); });");
            strScript.Append("</script>");


            TempData["PostForm"] = Convert.ToString(strForm) + Convert.ToString(strScript.ToString());

            return View();

        }

        /// <summary>
        /// 交易完成返回資料
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFinishApi()
        {
            var db = new PetaPoco.Database("LocalDBConnStr");

            GetFinishTrade b = new GetFinishTrade();

            b.MerchantID = Request["MerchantID"];
            b.MerchantTradeNo = Request["MerchantTradeNo"];
            b.PaymentDate = Request["PaymentDate"];
            b.PaymentType = Request["PaymentType"];
            b.PaymentTypeChargeFee = Request["PaymentTypeChargeFee"];
            b.RtnCode = int.Parse(Request["RtnCode"].ToString());
            b.RtnMsg = Request["RtnMsg"];
            b.SimulatePaid = int.Parse(Request["SimulatePaid"].ToString());
            b.TradeAmt = decimal.Parse(Request["TradeAmt"].ToString());
            b.TradeDate = Request["TradeDate"];
            b.TradeNo = Request["TradeNo"];
            b.CheckMacValue = Request["CheckMacValue"];
            b.TotalCount = (Request["TotalCount"] == null) ? 0 : int.Parse(Request["TotalCount"].ToString());

            if (b.MerchantID != null)
            {
                db.Insert(b);
                return Content("1|OK");
            }
            else
            {
                return Content("0|ErrorMessage");
            }
        }

        /// <summary>
        /// 模擬Post至MP反查訂單狀態應該回傳的資料
        /// </summary>
        /// <returns></returns>
        public ActionResult GetEsunMockMPcheckData()
        {
            string IcpNo = Request["IcpNo"];
            string TransNo = Request["TransNo"];



            string _data = "";
            Dictionary<string, string> dicData = new Dictionary<string, string>();
            //dicData.Add("IcpNo", IcpNo);
            //dicData.Add("TransNo", TransNo);
            dicData.Add("IcpNo", "5353885105");
            dicData.Add("TransNo", "27707");
            dicData.Add("TransAmt", "9999");
            dicData.Add("atmTradeNo", "20140822141540");
            dicData.Add("atmTradeDate", "20140822141540");
            dicData.Add("HashKey", "86A2C451B375D51053538851F8A6E5B1");
            dicData.Add("atmTradeState", "S");

            dicData.Add("MacCheck", GenerateTransIdentifyNoNew(dicData["IcpNo"], dicData["TransNo"], dicData["TransAmt"], dicData["atmTradeNo"], dicData["atmTradeDate"], dicData["HashKey"], dicData["atmTradeState"]));


            foreach (KeyValuePair<string, string> item in dicData)
            {
                _data += item.Value + "|";
            }
            _data = _data.Substring(0, _data.LastIndexOf('|') - 1);

            return Content(_data);
        }

        private string GenerateTransIdentifyNoNew(string icpNo, string transNo, string transAmt, string atmTradeNo, string atmTradeDate, string hashKey, string atmTradeState)
        {
            string s = string.Format("{0}{1}{2}{3}{4}{5}{6}", icpNo, transNo, transAmt, atmTradeNo, atmTradeDate, hashKey, atmTradeState);


            //以SHA-1來產生checksum
            string checkSum = new AllPay.ShareLib.Crypt().SHA(s);
            return checkSum;
        }
    }
}
