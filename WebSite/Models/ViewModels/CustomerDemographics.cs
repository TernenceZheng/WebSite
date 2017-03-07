using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class CustomerDemographics
    {
        //
        [Required(ErrorMessage = "必須輸入")]
        [Column("CustomerTypeID", TypeName = "nchar")]
        [Display(Name = "XXXXX")]
        public String CustomerTypeID { get; set; }

        //
        [Column("CustomerDesc", TypeName = "ntext")]
        [Display(Name = "XXXXX")]
        public String CustomerDesc { get; set; }
    }
}