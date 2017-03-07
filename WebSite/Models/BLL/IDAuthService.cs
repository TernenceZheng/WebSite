using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models.DAO;
using WebSite.Models.ViewModels;

namespace WebSite.Models.BLL
{
    public class IDAuthService
    {

        public int PassIDAuth(int MID, string DbName)
        {
            IDAuthDao _Dao = new IDAuthDao();

            int query = _Dao.UpdateIDAuth(MID, DbName);

            return query;
        }

        public int PassIDAuthAllPay(int MID, string DbName)
        {
            IDAuthDao _Dao = new IDAuthDao();

            int query = _Dao.UpdateIDAuthAllPay(MID, DbName);

            return query;
        }
    }
}