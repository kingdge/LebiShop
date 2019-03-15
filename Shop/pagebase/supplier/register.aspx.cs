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
    public partial class Register : ShopPage
    {
        protected Site_Supplier site;
        protected string url = "";
        protected string Version = "";
        protected bool IsSet = true;
        protected void Page_Load(object sender, EventArgs e)
        {
            site = new Site_Supplier();
            //url = RequestTool.RequestString("url").Replace("<", "").Replace(">", "");
            //if (url.ToLower().IndexOf("http") > -1 || url.ToLower().IndexOf("login.aspx") > -1 || url == "")
            //{
            //    url = site.AdminPath + "/default.aspx?desk=1";
            //}
        }
    }
}