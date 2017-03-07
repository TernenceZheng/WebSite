using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class Customers
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

        //
        [Column("CustomerDesc", TypeName = "ntext")]
        [Display(Name = "XXXXX")]
        public String CustomerDesc { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        [Column("CompanyName", TypeName = "nvarchar")]
        [StringLength(40, ErrorMessage = "長度不可超過40")]
        [Display(Name = "XXXXX")]
        public String CompanyName { get; set; }

        //
        [Column("ContactName", TypeName = "nvarchar")]
        [StringLength(30, ErrorMessage = "長度不可超過30")]
        [Display(Name = "XXXXX")]
        public String ContactName { get; set; }

        //
        [Column("ContactTitle", TypeName = "nvarchar")]
        [StringLength(30, ErrorMessage = "長度不可超過30")]
        [Display(Name = "XXXXX")]
        public String ContactTitle { get; set; }

        //
        [Column("Address", TypeName = "nvarchar")]
        [StringLength(60, ErrorMessage = "長度不可超過60")]
        [Display(Name = "XXXXX")]
        public String Address { get; set; }

        //
        [Column("City", TypeName = "nvarchar")]
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        [Display(Name = "XXXXX")]
        public String City { get; set; }

        //
        [Column("Region", TypeName = "nvarchar")]
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        [Display(Name = "XXXXX")]
        public String Region { get; set; }

        //
        [Column("PostalCode", TypeName = "nvarchar")]
        [StringLength(10, ErrorMessage = "長度不可超過10")]
        [Display(Name = "XXXXX")]
        public String PostalCode { get; set; }

        //
        [Column("Country", TypeName = "nvarchar")]
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        [Display(Name = "XXXXX")]
        public String Country { get; set; }

        //
        [Column("Phone", TypeName = "nvarchar")]
        [StringLength(24, ErrorMessage = "長度不可超過24")]
        [Display(Name = "XXXXX")]
        public String Phone { get; set; }

        //
        [Column("Fax", TypeName = "nvarchar")]
        [StringLength(24, ErrorMessage = "長度不可超過24")]
        [Display(Name = "XXXXX")]
        public String Fax { get; set; }
    }
}