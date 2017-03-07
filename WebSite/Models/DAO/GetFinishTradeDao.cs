using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Utility;
using WebSite.Models.ViewModels;

namespace WebSite.Models.DAO
{
    public class GetFinishTradeDao : BaseDao
    {

        public List<GetFinishTrade> GetProductsList(int pageIndex, int pageSize)
        {
            Db.SetConnectionString = DbUtility.ConnectionString.LocalDBConnStr;

            List<GetFinishTrade> notifyMsgList = Db.Query<GetFinishTrade>("GetFinishTrade_ListGetFinishTrade_S @pageIndex, @pageSize",
                                                                                new { pageIndex, pageSize }).ToList();

            return notifyMsgList;
        }
    }
}