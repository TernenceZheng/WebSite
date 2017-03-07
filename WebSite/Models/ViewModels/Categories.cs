using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class Categories
    {
        //
        [Required(ErrorMessage = "必須輸入")]
        [Column("CategoryID", TypeName = "int")]
        [Display(Name = "XXXXX")]
        public int CategoryID { get; set; }

        //
        [Required(ErrorMessage = "必須輸入")]
        [Column("CategoryName", TypeName = "nvarchar")]
        [StringLength(15, ErrorMessage = "長度不可超過15")]
        [Display(Name = "XXXXX")]
        public String CategoryName { get; set; }

        //
        [Column("Description", TypeName = "ntext")]
        [Display(Name = "XXXXX")]
        public String Description { get; set; }

        //
        [Column("Picture", TypeName = "image")]
        [Display(Name = "XXXXX")]
        public byte[] Picture { get; set; }
       
    }
}