using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier
{
    public partial class Login : ShopPage
    {
        protected Site_Supplier site;
        protected string url = "";
        protected string logintype = "";
        protected int sid;
        protected bool LoginError = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            site = new Site_Supplier();
            url = RequestTool.RequestString("url").Replace("<", "").Replace(">", "");
            if (url.ToLower().IndexOf("http") > -1 || url.ToLower().IndexOf("login.aspx") > -1 || url == "")
            {
                url = site.AdminPath + "/default.aspx?desk=1";
            }
            string msg = "";
            logintype = RequestTool.RequestString("logintype");
            sid = RequestTool.RequestInt("sid");
            if (CurrentUser.id > 0)
            {
                if (EX_Supplier.Login(CurrentUser, logintype, sid, out msg))
                {
                    Response.Redirect(url);
                }
            }
            try
            {
                if ((string)HttpContext.Current.Session["loginerror"] == "true")
                    LoginError = true;
            }
            catch
            {
                LoginError = false;
            }
        }
    }
}