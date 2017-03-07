using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Utility;

namespace WebSite.Models.DAO
{
    public class BaseDao
    {
        public DbUtility Db;
        public BaseDao()
        {
            Db = new DbUtility();

            //Db = DbUtility.Connect;
        }
    }
}