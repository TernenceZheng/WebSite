using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class EmployeeTerritories
    {
        //
        [Required(ErrorMessage = "必須輸入")]
        public int EmployeeID { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        [StringLength(20, ErrorMessage = "長度不可超過20")]
        public String TerritoryID { get; set; }

    }
}