using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models.ViewModels;
using WebSite.Models;
using WebSite.Models.BLL;
using System.Configuration;
using System.IO;

namespace WebSite.Controllers
{
    public class MpPostController : BaseController
    {
        //
        // GET: /MpPost/

        public ActionResult Index()
        {
            return View();
        }

        #region 身份認證與開通玉山儲值     
        /// <summary>
        /// 身份認證
        /// </summary>
        /// <param name="MID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ActionResult IDAuth(int MID,string Type)
        {
            IDAuthService idAuth = new IDAuthService();
            int result = idAuth.PassIDAuth(MID, Type);
            return Json(result);          
        }

        /// <summary>
        /// 開通儲值
        /// </summary>
        /// <param name="MID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public ActionResult IDAuthAllPay(int MID,string Type)
        {
            IDAuthService idAuth = new IDAuthService();
            int result = idAuth.PassIDAuth(MID, Type);
            return Json(result);
        }
        #endregion

        #region 模擬銀行MP POST

        //TODO

        #endregion

        #region 退款

        public ActionResult CancalAllPayTrade()
        {
            return View();
        }


        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="MasterNo"></param>
        /// <param name="TradeAmt"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CancalAllPayTrade(string MasterNo, int TradeAmt)
        {

            #region [SP]查詢金流訂單By訂單號碼
            string UserID = "";
            string PayFlag = string.Empty;
            string OrderID = "";
            string AllPayPaymentType = string.Empty;
            string AllPayTradeNo = string.Empty;
            string OrderAmount = string.Empty;
            string TransactionID = string.Empty;
            string CreditCheckCode = string.Empty;//會員檢查碼
            DateTime transDate = new DateTime(1900, 1, 1, 0, 0, 0);
            long TimeStamp = GetUnixTimeStamp(DateTime.Now);

            InsertReceivePaymentInfo("CancalAllPayTrade", MasterNo, HttpContext.Current.Request);
            try
            {
                var dsQuery = sputility.SP_Payment_QueryOrderBySingleField(MasterNo);

                if (dsQuery == null ||
                    dsQuery.Tables.Count == 0 ||
                    dsQuery.Tables[0].Rows.Count == 0)
                {
                    return ConverJSON(new RtnModel { RtnCode = 1010, RtnMsg = "訂單資料不存在" });
                }

                UserID = dsQuery.Tables[0].Rows[0]["UserID"].ToString();
                OrderID = dsQuery.Tables[0].Rows[0]["OrderID"].ToString();
                PayFlag = dsQuery.Tables[0].Rows[0]["PayFlag"].ToString();
                AllPayPaymentType = dsQuery.Tables[0].Rows[0]["AllPayPaymentType"].ToString();
                AllPayTradeNo = dsQuery.Tables[0].Rows[0]["AllPayTradeNo"].ToString();
                OrderAmount = dsQuery.Tables[0].Rows[0]["OrderAmount"].ToString();
                TransactionID = dsQuery.Tables[0].Rows[0]["TransactionID"].ToString();

                CreditCheckCode = dsQuery.Tables[0].Rows[0]["Info4"].ToString();
                string tempDate = dsQuery.Tables[0].Rows[0]["CreateTime"].ToString();
                DateTime.TryParse(tempDate, out transDate);

            }
            catch (Exception ex)
            {
                WriteLog(LogFile, "SP_Payment_QueryOrderBySingleField Error: " + ex.Message);

                return ex.Message;
            }

            #endregion

            #region ServerPost至AllPay查訂單狀態資料
            var allPayResult = string.Empty;
            bool checkPayFlag = false;
            string PostUrl = string.Empty;
            string resultStr = string.Empty;

            string HashKey = ConfigurationManager.AppSettings["HashKey"];
            string HashIV = ConfigurationManager.AppSettings["HashIV"];

            ReceiveCreditCancel receiveCreditCancel = new ReceiveCreditCancel();
            try
            {
                #region 檢查訂單狀態退款清單是否可退
                if (!string.IsNullOrEmpty(AllPayPaymentType) && !string.IsNullOrEmpty(PayFlag))
                {
                    int intPayFlag = 0;

                    int.TryParse(PayFlag, out intPayFlag);

                    if (intPayFlag > 0)
                    {
                        checkPayFlag = true;

                        int intTotalAmount = 0;
                        int.TryParse(OrderAmount, out intTotalAmount);


                        #region ServerPost至AllPay查訂單狀態資料
                        string PostAllPayQueryStr = string.Concat("MerchantID=", UserID,
                                         "&MerchantTradeNo=", OrderID,
                                         "&TimeStamp=", TimeStamp
                                        );

                        PostAllPayQueryStr += string.Concat("&CheckMacValue=", allPayCF.GetMacValue(PostAllPayQueryStr, HashKey, HashIV, "utf-8"));


                        var allPayData = string.Empty;
                        try
                        {
                            var requestUrl = string.Concat(System.Configuration.ConfigurationManager.AppSettings["AllPayTradeInfo"]);

                            allPayData = allPayCF.DoRequest(requestUrl, PostAllPayQueryStr, "application/x-www-form-urlencoded");


                        }
                        catch (Exception ex)
                        {
                            var logMsg = ex.Message + Environment.NewLine + ex.StackTrace;
                            WriteLog(LogFile, logMsg);
                            return ConverJSON(new RtnModel { RtnCode = 1012, RtnMsg = ex.Message.ToString() });
                        }

                        AllPayTradeInfo result = new AllPayTradeInfo();
                        if (!string.IsNullOrEmpty(allPayData))
                        {
                            result = ConvertDataToAllPayTradeInfo(allPayData);
                        }



                        #endregion


                        #region POST至歐付寶退款

                        //退款方式分兩種
                        if (AllPayPaymentType.Contains("Credit"))
                        {
                            if (intTotalAmount != TradeAmt)
                            {
                                return "歐付寶支付-信用卡退刷金額不符";
                            }

                            string Action = string.Empty;
                            //DateTime PM_Eight = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 20, 0, 0, 0);

                            #region 判斷Action動作

                            #region 查詢信用卡單筆明細

                            string CreditPostUrl = ConfigurationManager.AppSettings["CreditPaymentInfo"];

                            string PostCreditQueryStr = string.Concat("MerchantID=", UserID,
                                         "&CreditRefundId=", result.gwsr,
                                         "&CreditAmount=", result.amount,
                                         "&CreditCheckCode=", CreditCheckCode
                                        );

                            PostCreditQueryStr += string.Concat("&CheckMacValue=", allPayCF.GetMacValue(PostCreditQueryStr, HashKey, HashIV, "utf-8"));

                            WriteLog(LogFile, string.Format("查詢信用卡資料參數：{0}", PostCreditQueryStr));

                            allPayResult = allPayCF.DoRequest(CreditPostUrl, PostCreditQueryStr, "application/x-www-form-urlencoded");

                            WriteLog(LogFile, "查詢信用卡單筆明細的資料：" + allPayResult);

                            CreditInfo creditInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<CreditInfo>(allPayResult);

                            #endregion

                            if (creditInfo.RtnValue != null || !creditInfo.RtnMsg.Contains("error"))
                            {
                                if (creditInfo.RtnValue.status == "已關帳" || creditInfo.RtnValue.status == "要關帳")
                                {
                                    Action = "R";
                                }
                                else
                                {
                                    Action = "N";
                                }
                            }
                            else
                            {
                                WriteLog(LogFile, "查詢信用卡單筆明細的資料錯誤：" + allPayResult);
                                return "CancelAllPayError|Error";
                            }


                            #endregion

                            WriteLog(LogFile, string.Format("送信用卡退刷的之前動作「金額：{0}」,『動作：{1}』：", intTotalAmount, Action));

                            string PostQueryStr = string.Empty;
                            PostQueryStr = string.Concat("MerchantID=", UserID,
                                                         "&MerchantTradeNo=", OrderID,
                                                         "&TradeNo=", AllPayTradeNo,
                                                         "&Action=", Action,
                                                         "&TotalAmount=", intTotalAmount.ToString()
                                                        );

                            PostQueryStr += string.Concat("&CheckMacValue=", allPayCF.GetMacValue(PostQueryStr, HashKey, HashIV, "utf-8"));

                            //信用卡關帳/退刷/取消/放棄
                            PostUrl = ConfigurationManager.AppSettings["CreditBackURL"];
                            allPayResult = allPayCF.DoRequest(PostUrl, PostQueryStr, "application/x-www-form-urlencoded");

                            WriteLog(LogFile, "送信用卡退刷的返回結果：" + allPayResult);

                            #region 轉成物件資料

                            if (!string.IsNullOrEmpty(allPayResult))
                            {
                                try
                                {
                                    string[] allPayResultModel = allPayResult.Split('&');
                                    Dictionary<string, string> dicData = new Dictionary<string, string>();
                                    foreach (var item in allPayResultModel)
                                    {
                                        string[] ary = item.Split('=');
                                        if (ary != null && ary.Length > 1 && receiveCreditCancel.GetType().GetProperty(ary[0]) != null)
                                        {
                                            receiveCreditCancel.GetType().GetProperty(ary[0]).SetValue(receiveCreditCancel, ary[1], null);
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    var logMsg = ex.Message + Environment.NewLine + ex.StackTrace;
                                    WriteLog(LogFile, logMsg);

                                    return "CancelAllPayError|Error";
                                }
                            }

                            #endregion

                        }
                        else
                        {//可利用此API將消費金額退回消費者的歐付寶帳戶

                            #region 檢查應退費金額
                            int tureChargeBackAmt = 0;
                           

                            //手續費需由廠商負擔
                            tureChargeBackAmt = result.TradeAmt;

                            #endregion

                            PostUrl = ConfigurationManager.AppSettings["AllPayBackURL"];

                            string PostQueryStr = string.Empty;

                            PostQueryStr = string.Concat("MerchantID=", UserID,
                                                         "&MerchantTradeNo=", OrderID,
                                                         "&TradeNo=", AllPayTradeNo,
                                                         "&ChargeBackTotalAmount=", tureChargeBackAmt
                                                        );

                            PostQueryStr += string.Concat("&CheckMacValue=", allPayCF.GetMacValue(PostQueryStr, HashKey, HashIV, "utf-8"));

                            //回傳物件 1|OK
                            allPayResult = allPayCF.DoRequest(PostUrl, PostQueryStr, "application/x-www-form-urlencoded");

                        }

                        #endregion

                    }

                }

                #endregion
                

            }
            catch (Exception ex)
            {
                var logMsg = ex.Message + Environment.NewLine + ex.StackTrace;
                WriteLog(LogFile, logMsg);
                return "更新金流訂單狀態錯誤";
            }
            #endregion



            return resultStr;
        }

        #endregion



        internal AllPayTradeInfo ConvertDataToAllPayTradeInfo(string allPayResult)
        {
            AllPayTradeInfo result = new AllPayTradeInfo();
            try
            {
                string[] allPayResultModel = allPayResult.Split('&');
                Dictionary<string, string> dicData = new Dictionary<string, string>();
                foreach (var item in allPayResultModel)
                {
                    string[] ary = item.Split('=');
                    if (ary != null && ary.Length > 1 && result.GetType().GetProperty(ary[0]) != null)
                    {
                        if (result.GetType().GetProperty(ary[0]).PropertyType.Name == typeof(Int32).Name)
                        {
                            int tempIntValue = 0;
                            int.TryParse(ary[1], out tempIntValue);
                            result.GetType().GetProperty(ary[0]).SetValue(result, tempIntValue, null);
                        }
                        else
                        {
                            result.GetType().GetProperty(ary[0]).SetValue(result, ary[1], null);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                var logMsg = ex.Message + Environment.NewLine + ex.StackTrace;
                WriteLog(LogFile, logMsg);

                return result;
                throw;
            }
            return result;
        }

        internal int GetUnixTimeStamp(DateTime time)
        {
            return Convert.ToInt32((time.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }

        public void WriteLog(string strLogFileTitle, string strLog)
        {
            string strLogFilePath = strLogFileTitle + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            var strServerFilePath = Server.MapPath(strLogFilePath);
            var strServerFileDirectory = System.IO.Path.GetDirectoryName(strServerFilePath);
            if (!System.IO.Directory.Exists(strServerFileDirectory))
                System.IO.Directory.CreateDirectory(strServerFileDirectory);

            StreamWriter sw = new StreamWriter(strServerFilePath, true, System.Text.Encoding.GetEncoding("BIG5"));
            sw.Write(DateTime.Now.ToString("T", new System.Globalization.CultureInfo("hr-HR")) + '\t');

            sw.WriteLine(strLog);

            sw.Flush();
            sw.Close();
            sw.Dispose();
        }
    }


}
