using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models.ViewModels
{
    public class MsgStatus
    {
        /// <summary>
        /// 訊息代碼
        /// </summary>
        public int RtnCode { get; set; }

        /// <summary>
        /// 訊息
        /// </summary>
        public string RtnMsg { get; set; }

    }
}