using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using WebSite.Utility;
using AllPayResource;

namespace WebSite.Models.ViewModels
{
    public class MemberTopUpInfo
    {
        //會員編號
        public long MID { get; set; }

        //銀行特店編號
        public long MerchantID { get; set; }


        public string CName { get; set; }

        [Display(Name = "郵遞區號")]
        public string ZipCode { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [StringLength(100)]
        [Display(Name = "通訊地址")]
        public string Address { get; set; }

        [Display(Name = "性別")]
        public string Sex { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "生日")]
        public string Birthday { get; set; }

        [Display(Name = "年")]
        public string Year { get; set; }

        [Display(Name = "月")]
        public string Month { get; set; }

        [Display(Name = "日")]
        public string Day { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [IDNO]
        [Display(Name = "身份證字號")]
        public string IDNO { get; set; }

        [Display(Name = "電子郵件")]
        [Email]
        public string Email { get; set; }

        [Display(Name = "手機號碼")]
        public string CellPhone { get; set; }


        //### 身分證驗證相關
        [StringLength(5, MinimumLength = 5, ErrorMessage = "長度限制5個字元")]
        [Display(Name = "驗證碼")]
        public string CaptchaCode { get; set; }

        [Display(Name = "發證日期")]
        public string IDNODate { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = "發證年")]
        public string IDNOYear { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = "發證月")]
        public string IDNOMonth { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = "發證日")]
        public string IDNODay { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = "發證地點")]
        public string IDNOIssueLocation { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = "領取類別")]
        public string IDNOObtainType { get; set; }

        [Required(ErrorMessageResourceName = "_10000001", ErrorMessageResourceType = typeof(ErrorMessageResource))]
        [Display(Name = "戶籍地址")]
        public string HomeAddress { get; set; }

        [Display(Name = "身分證上是否有照片")]
        public string PicFree { get; set; }

        [Display(Name = "是否已驗證")]
        public string AuthStatus { get; set; }

        [Display(Name = "Hidden")]
        public string Hidden { get; set; }

        //驗證方式
        public short AuthType { get; set; }

        //廠商代碼
        public string IcpNo { get; set; }

        //HashKey組別
        public string HashKeyNo { get; set; }

        //銀行名稱
        public string BankName { get; set; }

        //傳送路徑的檔名
        public string UrlName { get; set; }

        //會員統編領補換日
        public string IssueDate { get; set; }

        //領補換代號(1:初發  2: 補發  3: 換發 )
        public string IssueType { get; set; }

        //領補換地點
        public string IssueLoc { get; set; }

        //認證類別(C1 :電子簽章認證   C2:金融連結認證  C3 :手機、email認證)
        public string IDType { get; set; }

        //虛擬帳號
        public string AccNo { get; set; }

        //使用狀態
        public int AccountStatus { get; set; }

        //戶籍郵遞區號
        public string HomeZipCode { get; set; }

        //市內電話
        public string Phone { get; set; }

    }
}