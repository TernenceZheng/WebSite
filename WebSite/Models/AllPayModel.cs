using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class AllPayModel
    {
        #region AllPayModel



        /// <summary>
        /// 歐付寶查詢需要的參數
        /// </summary>
        public class AllPayQueryInfo
        {
            public string MerchantID { get; set; }

            public string MerchantTradeNo { get; set; }

            public int TimeStamp { get; set; }

            public string CheckMacValue { get; set; }

        }

        public class CreditInfo
        {
            public string RtnMsg { get; set; }
            public RtnValue RtnValue { get; set; }
        }
        public class CloseData
        {
            public string status { get; set; }
            public string amount { get; set; }
            public string datetime { get; set; }
        }

        public class RtnValue
        {
            public string TradeID { get; set; }
            public string amount { get; set; }
            public string clsamt { get; set; }
            public string authtime { get; set; }
            public string status { get; set; }
            public List<CloseData> close_data { get; set; }
        }

        public class RtnModel
        {
            public int RtnCode { get; set; }

            public string RtnMsg { get; set; }

        }

        #endregion
    }


}