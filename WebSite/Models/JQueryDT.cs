using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models
{
    public class JQueryDT
    {
        public JQueryDT()
        {
            //
            // TODO: 在這裡新增建構函式邏輯
            //
        }
        public JQueryDT(HttpRequestBase Request)
        {
            sSearch_Cat = Request["ssearch_0"];
            sSearch = Request["ssearch"];
            sEcho = int.Parse(Request["sEcho"].ToString());
            sSearch = Request["sSearch"];
            iDisplayLength = Request["iDisplayLength"];
            iDisplayStart =  int.Parse(Request["iDisplayStart"].ToString())/10 +1;
            iColumns = Request["iColumns"];
            iTotalRecords = Request["iTotalRecords"];
            iTotalDisplayRecords = Request["iTotalDisplayRecords"];
        }
        /// <summary>
        /// DataTable請求伺服器端次數
        /// </summary>
        public int sEcho { get; set; }


        /// <summary>
        /// 過濾文本
        /// </summary>
        public string sSearch { get; set; }

        public string sSearch_Cat { get; set; }


        /// <summary>
        /// 每頁顯示的數量
        /// </summary>
        public string iDisplayLength { get; set; }


        /// <summary>
        /// 分頁時每頁跨度數量
        /// </summary>
        public int iDisplayStart { get; set; }


        /// <summary>
        /// 列數
        /// </summary>
        public string iColumns { get; set; }


        /// <summary>
        /// 排序列的數量
        /// </summary>
        public string iSortingCols { get; set; }

        public string iTotalRecords { get; set; }

        public string iTotalDisplayRecords { get; set; }




        /// <summary>
        /// 逗號分割所有的列
        /// </summary>
        public string sColumns { get; set; }



    }
}