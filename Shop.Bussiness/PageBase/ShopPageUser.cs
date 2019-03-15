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
    /// 用户中心部分
    /// </summary>

    public class ShopPageUser : ShopPage
    {

        protected override void LoadTheme(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode, true);
            
        }

    }

}