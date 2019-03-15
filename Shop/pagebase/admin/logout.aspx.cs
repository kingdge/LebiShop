using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin
{
    public partial class Logout : System.Web.UI.Page
    {
        protected Site site;
        protected void Page_Load(object sender, EventArgs e)
        {
            site = new Site();
            Log.Add("退出系统", "Login", EX_Admin.CurrentAdmin().id.ToString(), EX_Admin.CurrentAdmin());
            Response.Cookies.Add(new HttpCookie("Master", ""));
            Session["admin"] = null;
            Session["admin_group"] = null;
            Session["admin_power"] = null;
            Session["admin_power_url"] = null;
            Response.Redirect(site.AdminPath + "/login.aspx?url=" + HttpUtility.UrlEncode(RequestTool.GetUrlReferrerNonDomain()) + "&token=" + EX_Admin.MD5(ShopCache.GetBaseConfig().InstallCode + RequestTool.GetUrlReferrerNonDomain()) + "");
        }
    }
}