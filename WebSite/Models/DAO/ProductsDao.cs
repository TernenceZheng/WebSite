using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Utility;
using System.Data;
using System.Data.SqlClient;
using WebSite.Models.ViewModels;

namespace WebSite.Models.DAO
{
    public class ProductsDao:BaseDao
    {
        public List<Products> GetProductsList(int pageIndex, int pageSize)
        {
            Db.SetConnectionString = DbUtility.ConnectionString.LocalDBConnStr;

            List<Products> notifyMsgList = Db.Query<Products>("Products_ListProducts_S @pageIndex, @pageSize",
                                                                                new {  pageIndex, pageSize }).ToList();

            return notifyMsgList;
        }

        public Products GetProducts(int ProductID)
        {
            Db.SetConnectionString = DbUtility.ConnectionString.LocalDBConnStr;

            Products product = Db.SingleOrDefault<Products>("Products_GetProducts_S @ProductID ", new { ProductID });

            return product;
        }

    }
}