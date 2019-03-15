using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Linq;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace Shop.Bussiness
{
    /// <summary>
    /// 前台页面的直接后台文件
    /// </summary>

    public class ShopPageAgent : ShopPage
    {

        protected Lebi_Currency DefaultCurrency;
        protected Site_Agent site; 
        protected override void OnLoad(EventArgs e)
        {
            //页面载入检查
            PageLoadCheck();

            site = new Site_Agent();
            string themecode = "";
            int siteid = 0;
            string languagecode = "";
            var nv = CookieTool.GetCookie("ThemeStatus");
            if (!string.IsNullOrEmpty(nv.Get("theme")))
            {
                themecode = nv.Get("theme");
            }
            if (!string.IsNullOrEmpty(nv.Get("language")))
            {
                languagecode = nv.Get("language");
            }
            if (!string.IsNullOrEmpty(nv.Get("theme")))
            {
                int.TryParse(nv.Get("site"), out siteid);
            }

            if (siteid == 0)
                siteid = ShopCache.GetMainSite().id;
            LoadTheme(themecode, siteid, languagecode, "", true);

            DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
            if (DefaultCurrency == null)
                DefaultCurrency = B_Lebi_Currency.GetList("", "Sort desc").FirstOrDefault();
            CurrentCurrency = DefaultCurrency;
            base.OnLoad(e);

        }

    }

}