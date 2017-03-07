using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class Employees
    {
        //
        [Required(ErrorMessage = "必須輸入")]
        public int EmployeeID { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        [StringLength(20, ErrorMessage = "長度不可超過20")]
        public String LastName { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        [StringLength(10, ErrorMessage = "長度不可超過10")]
        public String FirstName { get; set; }

        //
        [StringLength(30, ErrorMessage = "長度不可超過30")]
        public String Title { get; set; }

        //
        [StringLength(25, ErrorMessage = "長度不可超過25")]
        public String TitleOfCourtesy { get; set; }

        //
        public DateTime BirthDate { get; set; }

        //
        public DateTime HireDate { get; set; }

        //
        [StringLength(60, ErrorMessage = "長度不可超過60")]
        public String Address { get; set; }

        //
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        public String City { get; set; }

        //
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        public String Region { get; set; }

        //
        [StringLength(10, ErrorMessage = "長度不可超過10")]
        public String PostalCode { get; set; }

        //
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        public String Country { get; set; }

        //
        [StringLength(24, ErrorMessage = "長度不可超過24")]
        public String HomePhone { get; set; }

        //
        [StringLength(4, ErrorMessage = "長度不可超過4")]
        public String Extension { get; set; }

        //
        public byte[] Photo { get; set; }

        //
        public String Notes { get; set; }

        //
        public int ReportsTo { get; set; }

        //
        [StringLength(255, ErrorMessage = "長度不可超過255")]
        public String PhotoPath { get; set; }

    }
}