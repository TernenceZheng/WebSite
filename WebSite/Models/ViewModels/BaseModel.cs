using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Models.ViewModels
{
    public class BaseModel 
    {
        public int sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public virtual List<Object> aaData { get; set; }
    }
}