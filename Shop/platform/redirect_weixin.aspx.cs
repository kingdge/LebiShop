using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Platform;
using Shop.Bussiness;
using LB.Tools;
namespace Shop.platform
{
    public partial class redirect_weixin : ShopPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string backurl = RequestTool.RequestString("backurl");
            backurl = backurl.Replace("weixinlogin", "weixinlogin_");
            if (backurl == "" || backurl.ToLower().IndexOf(URL("P_Login", "").ToLower()) > -1 || backurl.ToLower().IndexOf(URL("P_Register", "").ToLower()) > -1 || backurl.ToLower().IndexOf(URL("P_FindPassword", "").ToLower()) > -1)
            {
                backurl = URL("P_UserCenter", "");
            }
            string res = weixin.GetInstance(DT_id,CurrentSite).LoginURL(backurl);
            Response.Redirect(res);
            //Response.Write(res);
        }
    }
}