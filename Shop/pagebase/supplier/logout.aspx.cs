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
    public partial class Logout : System.Web.UI.Page
    {
        protected Site_Supplier site_supplier;
        protected void Page_Load(object sender, EventArgs e)
        {
            site_supplier = new Site_Supplier();

            CookieTool.DeleteCookie("User");
            Response.Redirect(site_supplier.AdminPath + "/login.aspx?url=" + HttpUtility.UrlEncode(RequestTool.GetUrlReferrerNonDomain()) + "");

        }
    }
}