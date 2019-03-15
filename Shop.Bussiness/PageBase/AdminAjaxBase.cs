using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using System.Collections.Specialized;
namespace Shop.Bussiness
{
    public class AdminAjaxBase : AdminBase
    {

        protected List<Lebi_Menu> AllMenus;
        protected override void OnLoad(EventArgs e)
        {
            if (!EX_Admin.LoginStatus() && RequestTool.GetRequestUrlNonDomain().ToLower().IndexOf(site.AdminPath + "/login.aspx") < 0)
            {
                //Response.Redirect(site.AdminPath + "/login.aspx?url=" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "");
                string url = site.AdminPath + "/login.aspx?url=" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "&token=" + EX_Admin.MD5(SYS.InstallCode + RequestTool.GetRequestUrlNonDomain()) + "";
                url = "<script type=\"text/javascript\">window.location='" + url + "';</script>";
                Response.Write(url);
                Response.End();
                return;
            }
            CurrentAdmin = EX_Admin.CurrentAdmin();
            //CurrentAdminGroup = (Lebi_Admin_Group)Session["admin_group"];
            CurrentAdminGroup = B_Lebi_Admin_Group.GetModel(CurrentAdmin.Admin_Group_id);
            
            CheckPagePower();
            base.OnLoad(e);
        }

        public string MenuUrl(string url, int t)
        {
            if (url.IndexOf("http") == 0)
                return url;
            url = site.AdminPath + "/" + url;
            if (t == 1)
            {
                if (url.Contains("?"))
                    url = url + "&desk=1";
                else
                    url = url + "?desk=1";
            }
            url = ThemeUrl.CheckURL(url);
            return url;
        }

        /// <summary>
        /// 根据COOKIE检查菜单是否显示
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool MenuShow(int id)
        {
            string menuids = RequestTool.GetCookieString("menus", "");
            if (menuids.Contains("|" + id + "|"))
                return false;
            return true;
        }
    }


}
