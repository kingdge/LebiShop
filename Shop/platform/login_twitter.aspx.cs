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
    public partial class login_twitter : ShopPage
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
            string res = Twitter.GetInstance(DT_id).Login(backurl, IsLogin);
            if (res == "OK")
            {
                Response.Redirect(backurl);
                //if (backurl == "")
                //    Response.Redirect("../");
                //else
                //{
                //    Response.Redirect(QQ.Instance.DEBackuri(backurl));
                //}
            }
            else
                Response.Write(res);
        }
    }
}