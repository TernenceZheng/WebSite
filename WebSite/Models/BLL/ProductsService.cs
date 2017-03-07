using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models.ViewModels;
using WebSite.Models.DAO;

namespace WebSite.Models.BLL
{
    public class ProductsService
    {

        public List<Products> ListProducts( int pageIndex, int pageSize, ref int totalCount)
        {
            ProductsDao _productsDao = new ProductsDao();

            List<Products> query = _productsDao.GetProductsList(pageIndex, pageSize);

            //再將IEnumerable轉為List，以避免在使用.Count或.First時，又會query DB
            var List = query.ToList();

            if (List.Count() != 0)
            {
                totalCount = List.First().TotalCount;
            }

            return List;
        }

        public Products GetProduct(int ProductID)
        {
            ProductsDao _productsDao = new ProductsDao();

            Products _product = _productsDao.GetProducts(ProductID);

            return _product;
        }

    }
}