using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class CustomerCustomerDemo
    {
        //
        [Required(ErrorMessage = "必須輸入")]
        [Column("CustomerID", TypeName = "nchar")]
        [Display(Name = "XXXXX")]
        public String CustomerID { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        [Column("CustomerTypeID", TypeName = "nchar")]
        [Display(Name = "XXXXX")]
        public String CustomerTypeID { get; set; }
    }
}