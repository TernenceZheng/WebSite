using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Models.DAO;
using WebSite.Models.ViewModels;

namespace WebSite.Models.BLL
{
    public class GetFinishTradeService
    {
        public List<GetFinishTrade> ListProducts(int pageIndex, int pageSize, ref int totalCount)
        {
            GetFinishTradeDao _productsDao = new GetFinishTradeDao();

            List<GetFinishTrade> query = _productsDao.GetProductsList(pageIndex, pageSize);

            //再將IEnumerable轉為List，以避免在使用.Count或.First時，又會query DB
            var List = query.ToList();

            if (List.Count() != 0)
            {
                totalCount = List.First().TotalCount;
            }

            return List;
        }
    }
}