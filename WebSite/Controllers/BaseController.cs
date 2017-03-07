using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models.ViewModels;
using System.Collections;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class BaseController : Controller
    {
        public static BaseModel FormatDTByObject<T>(IEnumerable<T> enumeration,JQueryDT dt, int pageSize, int totalRecords)
        {
            BaseModel root = new BaseModel();
            root.sEcho = dt.sEcho;
            root.iTotalRecords = totalRecords;
            root.iTotalDisplayRecords = totalRecords;

            if (enumeration != null)
            {
                List<object> adList = new List<object>();
                foreach (T item in enumeration)
                {
                    adList.Add(item);
                }
                root.aaData = adList;
            }
            return root;

        }
    }
}
