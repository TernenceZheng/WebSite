using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models.ViewModels;
using WebSite.Models.BLL;
using Webdiyer.WebControls.Mvc;
using WebSite.Models;
using AllPay.ShareLib;

namespace WebSite.Controllers
{
    public class ProductsController : BaseController
    {
        //
        // GET: /Products/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(int ProductID)
        {
            ProductsService _Products = new ProductsService();

            Products _product = _Products.GetProduct(ProductID);

            return View(_product);
        }

        [HttpPost]
        public ActionResult Detail(Products tradeDetailProducts)
        {
            Random r = new Random();

            Dictionary<string, string> postCollection = new Dictionary<string, string>();

            postCollection.Add("ChoosePayment", "ALL");//選擇預設付款方式 
            postCollection.Add("ChooseSubPayment", "");//選擇預設付款方式 
            postCollection.Add("ClientBackURL", "http://www.google.com.tw");//選擇預設付款方式 
            postCollection.Add("ExpireDate", "");//選擇預設付款方式 
            postCollection.Add("ItemName", tradeDetailProducts.ProductName); //商品名稱
            postCollection.Add("ItemURL","");
            postCollection.Add("MerchantID", "1001122");//廠商編號 2000132
            String time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            postCollection.Add("MerchantTradeDate", time);//廠商交易時間
            postCollection.Add("MerchantTradeNo", "TestMVC" + r.Next(9999));//廠商交易編號         
            postCollection.Add("OrderResultURL", "http://jarvis.allpay.com.tw:9527/ShowReturn/ShowOrder");//Client端回傳付款結果網址

            postCollection.Add("PaymentType", "aio");//交易類型
            postCollection.Add("Remark", "");
            postCollection.Add("ReturnURL", "http://jarvis.allpay.com.tw:9527/Common/GetFinishApi");//付款完成通知回傳網址

            postCollection.Add("TotalAmount", tradeDetailProducts.UnitPrice.ToString("N0"));//交易金額
            postCollection.Add("TradeDesc", tradeDetailProducts.ProductName + "的交易描述");//交易描述
            
            
            
            string Mac = GenCheckMacValue(postCollection);
            postCollection.Add("CheckMacValue", Mac);

            TempData["PostCollection"] = postCollection;
            TempData["PostURL"] = "http://devpayment.allpay.com.tw:12005/Cashier/AioCheckOut";
            //TempData["PostURL"] = "http://payment-beta.allpay.com.tw/Cashier/AioCheckOut";
            return RedirectToAction("AutoSubmitForm", "Common");
        }

        private string GenCheckMacValue(Dictionary<string, string> postCollection)
        {
            string _newQuery = "", _newQuery2="";
            if (postCollection.Count > 0)
            {
                foreach (KeyValuePair<string, string> item in postCollection)
                {
                    _newQuery += HttpUtility.UrlDecode(item.Key + "=" + item.Value + "&");
                }
                 _newQuery2 =  _newQuery.Substring(0, _newQuery.Length - 1);
            }

            //beta測試
            //_newQuery = string.Format("HashKey={0}&{1}&HashIV={2}", "WC4O2h6oOKqY3Vuk", _newQuery2, "75UnvoWg1BKArQ14");
            //dev測試
            _newQuery = string.Format("HashKey={0}&{1}&HashIV={2}", "hk64SbkO9teN7GTC", _newQuery2, "N1HqECaW8GrTPsDs");
            _newQuery = HttpUtility.UrlEncode(_newQuery).ToLower();

            string MacValue = new Crypt().MD5(_newQuery);

            return MacValue;

        }

        public ActionResult getDTJson()
        {
            int pageSize = 10;
            int totalCount = 0;
            JQueryDT dt = new JQueryDT(Request);
            ProductsService _Products = new ProductsService();

            List<Products> _productsList = _Products.ListProducts(dt.iDisplayStart, pageSize, ref totalCount);

            BaseModel _dt = FormatDTByObject<Products>(_productsList, dt, pageSize, totalCount);

            return Json(_dt, JsonRequestBehavior.AllowGet);
        }

    }
}
