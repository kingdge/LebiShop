using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Collections.Specialized;
using System.Text;
using System.Collections.Generic;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace INIpayWap
{
    public partial class INIStdPayReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 여기에 사용자 코드를 배치하여 페이지를 초기화합니다.
            if (!Page.IsPostBack)
                StartINIStdReturn();
        }

        private void StartINIStdReturn()
        {
            string PGIP = RequestTool.GetClientIP();

            if (PGIP == "118.129.210.25" || PGIP == "211.219.96.165" || PGIP == "183.109.71.153")
            {


                DateTime resp_time = DateTime.Now;

                string P_TID = RequestTool.RequestSafeString("P_TID").Trim();

                string P_MID = RequestTool.RequestSafeString("P_MID").Trim();

                string P_AUTH_DT = RequestTool.RequestSafeString("P_AUTH_DT").Trim();

                string P_STATUS = RequestTool.RequestSafeString("P_STATUS").Trim();

                string P_TYPE = RequestTool.RequestSafeString("P_TYPE").Trim();

                string P_OID = RequestTool.RequestSafeString("P_OID").Trim();

                string P_FN_CD1 = RequestTool.RequestSafeString("P_FN_CD1").Trim();

                string P_FN_CD2 = RequestTool.RequestSafeString("P_FN_CD2").Trim();

                string P_FN_NM = RequestTool.RequestSafeString("P_FN_NM").Trim();

                string P_AMT = RequestTool.RequestSafeString("P_AMT").Trim();

                string P_UNAME = RequestTool.RequestSafeString("P_UNAME").Trim();

                string P_RMESG1 = RequestTool.RequestSafeString("P_RMESG1").Trim();

                string P_RMESG2 = RequestTool.RequestSafeString("P_RMESG2").Trim();

                string P_NOTI = RequestTool.RequestSafeString("P_NOTI").Trim();

                string P_AUTH_NO = RequestTool.RequestSafeString("P_AUTH_NO").Trim();


                if (P_TYPE == "VBANK")
                {
                    if (P_STATUS != "02")
                    {

                        Order.OnlinePaySuccess("INIpayWap", P_OID); 
                        Response.Write("OK");
                        Response.End();
                    }


                }
            }
            Response.Write("FAIL");
            Response.Redirect("/user/Orders.aspx");

        }
    }
}
