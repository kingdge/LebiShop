using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Platform;
using LB.Tools;
using Shop.Bussiness;
namespace Shop.platform
{
    public partial class login_weixin : ShopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string backurl = RequestTool.RequestString("backurl");
            int IsLogin = 0;
            if (backurl.ToLower().IndexOf(URL("P_Login", "").ToLower()) > -1)
            {
                IsLogin = 1;
            }
            if (backurl == "" || backurl.ToLower().IndexOf(URL("P_Login", "").ToLower()) > -1 || backurl.ToLower().IndexOf(URL("P_Register", "").ToLower()) > -1 || backurl.ToLower().IndexOf(URL("P_FindPassword", "").ToLower()) > -1)
            {
                backurl = URL("P_UserCenter", "");
            }
            backurl = weixin.GetInstance(DT_id,CurrentSite).DEBackuri(backurl);
            string res = weixin.GetInstance(DT_id, CurrentSite).Login(backurl, IsLogin);
            if (res == "OK")
            {
                Response.Redirect(backurl);
            }
            else
            {
                Response.Write(res);
                //Response.Redirect(backurl);
            }
        }
    }
}