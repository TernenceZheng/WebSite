using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class Order_Details
    {
        //
        [Required(ErrorMessage = "必須輸入")]
        public int OrderID { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        public int ProductID { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        public decimal UnitPrice { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        public short Quantity { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        public float Discount { get; set; }
    }
}