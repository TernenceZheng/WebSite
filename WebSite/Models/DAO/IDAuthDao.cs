using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSite.Utility;
using WebSite.Models.ViewModels;

namespace WebSite.Models.DAO
{
    public class IDAuthDao : BaseDao
    {
        internal int UpdateIDAuth(int MID, string DbName)
        {
            if (DbName == "stage")
            {
                //Db.SetConnectionString = DbUtility.ConnectionString.DBConnStr;
            }
            else if (DbName == "beta")
            {
                Db.SetConnectionString = DbUtility.ConnectionString.BetaAllPayConnStr;
            }
            else if (DbName == "dev")
            {
                Db.SetConnectionString = DbUtility.ConnectionString.AllPayConnStr;
            }
            

            int result=0, result1=0, result2=0;

            result = Db.Execute("UPDATE dbo.Member_Auth_IDNO SET AuthStatus = 3 WHERE MID =@MID", new { MID });

            if (result > 0)
            {
                string sqlStr = @"UPDATE
		                                    dbo.Member_Auth_IDNO
	                                        SET
		                                CaptchaURL = 'https://www.ris.gov.tw/OpenCaptcha/captcha?context=-180442312&session=8C7A8A6177203A922CA14CE0124203A3',
		                                CaptchaCode='8dhk7',
		                                AuthStatus = 3
	                                        WHERE		
		                                MID = @MID";

                result1 = Db.Execute(sqlStr, new { MID });
                if (result1 > 0)
                {
                    string sqlStr1 = @"EXEC ausp_Member_UpdateIDNOAuthResult_SU  @MID,1,'驗證成功' ";

                    result2 = Db.Execute(sqlStr1, new { MID });
                }
            }


            return result2;
        }

        internal int UpdateIDAuthAllPay(int MID, string DbName)
        {
            if (DbName == "stage")
            {
                //Db.SetConnectionString = DbUtility.ConnectionString.DBConnStr;
            }
            else if (DbName == "beta")
            {
                Db.SetConnectionString = DbUtility.ConnectionString.BetaAllPayConnStr;
            }
            else if (DbName == "dev")
            {
                Db.SetConnectionString = DbUtility.ConnectionString.AllPayConnStr;
            }
            MsgStatus result ;
            int rtnCode = 0;
            //--取得開通儲值帳戶申請資料		
            MemberTopUpInfo mt = Db.SingleOrDefault<MemberTopUpInfo>("exec ausp_Member_GetTopUpApplyDetail_S @MID, 999900808", new { MID });
            if (mt !=null & mt.MID !=0)
            {
                //新增認證通過的儲值帳戶資料	
                MsgStatus s = Db.SingleOrDefault<MsgStatus>("exec ausp_Member_AddTopUpAccount_I @MID, 999900808, 8801765432100, 3, 1, 1", new { MID });
                if (s !=null & s.RtnCode !=0)
                {
                    //開戶成功更新已驗證通過的身份證資料
                    result = Db.SingleOrDefault<MsgStatus>("exec ausp_Member_UpdateTopUpAuthIDNO_U @MID, 999900808", new { MID });
                    
                    rtnCode = result.RtnCode;
                }                	
            }

            return rtnCode;
        }


    }
}