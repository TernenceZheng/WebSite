using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WebSite.Models.ViewModels
{
    public class GetFinishTrade
    {
        //廠商編號
        [Required(ErrorMessage = "必須輸入")]
        public string MerchantID { get; set; }

        //廠商交易編號
        [StringLength(20, ErrorMessage = "長度不可超過20")]
        public String MerchantTradeNo { get; set; }

        //交易狀態
        public int RtnCode { get; set; }

        //交易訊息
        [StringLength(200, ErrorMessage = "長度不可超過200")]
        public String RtnMsg { get; set; }

        //AllPay的交易編號
        [StringLength(20, ErrorMessage = "長度不可超過20")]
        public String TradeNo { get; set; }

        //交易金額
        public decimal TradeAmt { get; set; }

        //付款時間
        [StringLength(20, ErrorMessage = "長度不可超過20")]
        public String PaymentDate { get; set; }

        //會員選擇的付款方式
        [StringLength(20, ErrorMessage = "長度不可超過20")]
        public String PaymentType { get; set; }

        //通路費
        public string PaymentTypeChargeFee { get; set; }

        //訂單成立時間
        [StringLength(20, ErrorMessage = "長度不可超過20")]
        public String TradeDate { get; set; }

        //是否為模擬付款
        public int SimulatePaid { get; set; }

        //檢查碼
        [StringLength(50, ErrorMessage = "長度不可超過50")]
        public String CheckMacValue { get; set; }

        //查詢結果的總筆數
        public int TotalCount { get; set; }
    }
}