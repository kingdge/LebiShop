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
    public partial class Login : AdminBase
    {
        protected Site site;
        protected string url = "";
        protected string Version = "";
        protected bool IsSet = true;
        protected string token;
        protected bool LoginError = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            site = new Site();
            url = RequestTool.RequestString("url").Replace("<", "").Replace(">", "");
            token = RequestTool.RequestString("token");
            if (url.ToLower().IndexOf("http") > -1 || url.ToLower().IndexOf("login.aspx") > -1 || url.ToLower().IndexOf("/ajax/") > -1 || url == "")
            {
                url = site.AdminPath + "/default.aspx?desk=1";
            }
            if (EX_Admin.MD5(SYS.InstallCode + url) != token)
                url = site.AdminPath + "/default.aspx?desk=1";
            BaseConfig bcf = ShopCache.GetBaseConfig();
            if (bcf.LicenseUserName == "" && bcf.LicensePWD == "")
                IsSet = false;
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                Version = Shop.LebiAPI.Service.Instanse.ServicepackName(SYS.LicensePackage);
                Version = "<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/license/\" target=\"_blank\"><span>" + Tag(Version) + "</span></a>";
            }
            else
            {
                Version = "";
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