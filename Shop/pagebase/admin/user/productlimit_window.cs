using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Collections.Specialized;
using Shop;

namespace Shop.Admin.User
{
    public partial class productlimit_window : AdminAjaxBase
    {

        protected string PageString = "";
        protected string key = "";
        protected int productid = 0;
        protected int userlevelid = 0;
        protected Lebi_Product product;
        protected void Page_Load(object sender, EventArgs e)
        {
            userlevelid = RequestTool.RequestInt("userlevelid");
         
            key = RequestTool.RequestString("key");
            productid = RequestTool.RequestInt("productid");

            product = B_Lebi_Product.GetModel(productid);

            if (product == null)
                product = new Lebi_Product();
          

        }

        /// <summary>
        /// 会员数据
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string CreateTree()
        {

            string str = "";



            List<Lebi_UserLevel> types = B_Lebi_UserLevel.GetList("", "Grade asc");
            
            string WebPath = RequestTool.GetConfigKey("WebPath").Trim().TrimEnd('/');

            WebPath = WebPath + "/theme/system/systempage/admin/images/";
            string showids = LB.Tools.CookieTool.GetCookieString("showUserLevelids").Replace("%2C", ",");
         
            string image = "";
            foreach (Lebi_UserLevel t in types)
            {
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<li><a href=\"javascript:void(0)\" onclick=\"searchproduct(" + t.id + ")\">" + Lang(t.Name) + "</a></li>";
                }
                else
                {
                    image = WebPath + "plus.gif";
                    str += "<tr class=\"list\" name=\"tr" + t.id + "\"  id=\"tr" + t.id + "\">";
                    str += "<td><img src=\"" + image + "\" name=\"imgu" + t.id + "\" id=\"imgu" + t.id + "\" style=\"cursor: pointer; text-align: center; vertical-align:absmiddle\"  />";
                    str += "<a href=\"javascript:void(0)\" onclick=\"searchproduct(" + t.id + ")\">" + Lang(t.Name) + "&nbsp;</a></td></tr>";
                }
            }
            return str;
        }


    }
}