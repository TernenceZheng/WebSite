using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class Products
    {
        	//
	 [Required(ErrorMessage = "必須輸入")]
	public  int ProductID { get; set; }

	//
	 [Required(ErrorMessage = "必須輸入")]
	[StringLength(40,ErrorMessage="長度不可超過40")]
	public  String ProductName { get; set; }

	//
	public  int SupplierID { get; set; }

	//
	public  int CategoryID { get; set; }

	//
	[StringLength(20,ErrorMessage="長度不可超過20")]
	public  String QuantityPerUnit { get; set; }

	//
	public  decimal UnitPrice { get; set; }

	//
	public  short UnitsInStock { get; set; }

	//
	public  short UnitsOnOrder { get; set; }

	//
	public  short ReorderLevel { get; set; }

	//
	 [Required(ErrorMessage = "必須輸入")]
	public bool Discontinued { get; set; }

     //查詢結果的總筆數
     public int TotalCount { get; set; }
    }
}