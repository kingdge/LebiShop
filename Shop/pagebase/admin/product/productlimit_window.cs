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

namespace Shop.Admin.product
{
    public partial class productlimit_window : AdminAjaxBase
    {

        protected string PageString = "";
        protected List<Lebi_Product> products;
        protected int orderid = 0;
        protected int typeid = 0;
        protected string key = "";
        protected int userid = 0;
        protected int userlevelid = 0;
        protected Lebi_User user;
        protected Lebi_UserLevel userlevel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_user_limit", "商品会员权限"))
            {
                WindowNoPower();
            }
            userlevelid = RequestTool.RequestInt("userlevelid");
            userid = RequestTool.RequestInt("userid");
            orderid = RequestTool.RequestInt("orderid");
            typeid = RequestTool.RequestInt("typeid");
            key = RequestTool.RequestString("key");

            user = B_Lebi_User.GetModel(userid);
            userlevel = B_Lebi_UserLevel.GetModel(userlevelid);
            if (user == null)
                user = new Lebi_User();
            if (userlevel == null)
                userlevel = new Lebi_UserLevel();
            string where = "Product_id=0 and (IsDel!=1 or IsDel is null)";
            if (key != "")
                where += " and (Name like '%" + key + "%' or Number like '%" + key + "%' or Code like '%" + key + "%')";
            if (typeid > 0)
            {
                string tids = EX_Product.TypeIds(typeid);
                where += " and Pro_Type_id in (" + tids + ")";
            }
        }
        public string GetUnitName(int id)
        {
            Lebi_Units model = B_Lebi_Units.GetModel(id);
            if (model == null)
                return "";
            return Lang(model.Name);
        }
        /// <summary>
        /// 商品分类树
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public string CreateTree(int pid, int deep)
        {
            return CreateTree(pid, deep, CurrentLanguage.Code);
        }
        /// <summary>
        /// 商品分类树
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public string CreateTree(int pid, int deep, string lang)
        {
            string str = "";
            if (deep == 0)
            {
                str += "<tr class=\"list\">";
                str += "<td><a href=\"javascript:searchproduct(0)\">  + " + Language.Tag("全部分类", lang) + "&nbsp;</a></td></tr>";
            }
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            //将根节点进行遍历
            string style = "";
            string WebPath = RequestTool.GetConfigKey("WebPath").Trim().TrimEnd('/');

            WebPath = WebPath + "/theme/system/systempage/admin/images/";
            string showids = LB.Tools.CookieTool.GetCookieString("showTypeids").Replace("%2C", ",");
            if (deep > 0)
                style = "style=\"display:none;\"";
            string image = "";
            foreach (Lebi_Pro_Type t in types)
            {
                image = WebPath + "plus.gif";

                if (showids.Contains("," + t.id + ",") || showids.Contains("," + t.Parentid + ","))
                {
                    style = "";
                    image = WebPath + "minus.gif";
                }
                str += "<tr class=\"list\" name=\"tr" + t.Parentid + "\" " + style + " id=\"tr" + t.id + "\">";
                //str += "<td style=\"text-align:center\"><input type='checkbox' id=\"check" + t.id + "\" value='" + t.id + "' name='id' del=\"del\" onclick=\"searchproduct();\" /></td>";
                str += "<td>" + deepstr(deep) + "<img src=\"" + image + "\" name=\"img" + t.Parentid + "\" id=\"img" + t.id + "\" style=\"cursor: pointer; text-align: center; vertical-align:absmiddle\" onclick=\"ShowTree('" + findpath(t.id) + "'," + t.id + ")\" title=\"" + Shop.Bussiness.Language.Tag("展开", lang) + "\" />";
                str += "<a href=\"javascript:searchproduct(" + t.id + ")\">" + Language.Content(t.Name, lang) + "&nbsp;</a></td></tr>";
                str += CreateTree(t.id, deep + 1, lang);
            }
            return str;
        }
        private string deepstr(int deep)
        {
            string str = "";
            for (int i = 0; i < deep; i++)
            {
                str += "&nbsp;&nbsp;";
            }
            return str;
        }
        /// <summary>
        /// 某个结点下的所有结点
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        private string findpath(int pid)
        {
            string str = "";
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            if (types.Count == 0)
                return "";
            foreach (Lebi_Pro_Type t in types)
            {
                if (str == "")
                    str = t.id.ToString();
                else
                    str += "," + t.id;
                string f = findpath(t.id);
                //if (f != "")
                str += "," + f;
            }
            return str;
        }
    }
}