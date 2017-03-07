using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class Orders
    {
        //
        [Required(ErrorMessage = "必須輸入")]
        public int OrderID { get; set; }

        //
        public String CustomerID { get; set; }

        //
        public int EmployeeID { get; set; }

        //
        public DateTime OrderDate { get; set; }

        //
        public DateTime RequiredDate { get; set; }

        //
        public DateTime ShippedDate { get; set; }

        //
        public int ShipVia { get; set; }

        //
        public decimal Freight { get; set; }

        //
        [StringLength(40, ErrorMessage = "長度不可超過40")]
        public String ShipName { get; set; }

        //
        [StringLength(60, ErrorMessage = "長度不可超過60")]
        public String ShipAddress { get; set; }

        //
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        public String ShipCity { get; set; }

        //
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        public String ShipRegion { get; set; }

        //
        [StringLength(10, ErrorMessage = "長度不可超過10")]
        public String ShipPostalCode { get; set; }

        //
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        public String ShipCountry { get; set; }

    }
}