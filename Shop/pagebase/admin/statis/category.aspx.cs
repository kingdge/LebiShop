using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using LB.DataAccess;
using System.Collections.Specialized;

namespace Shop.statis
{

    public partial class category : AdminPageBase
    {
        protected int time;
        protected int type;
        protected string List;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("statis_category", "类别统计"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            time = RequestTool.RequestInt("time", 0);
            type = RequestTool.RequestInt("type", 0);
            List = CreateTree(0, 0);
        }
        public string CreateTree(int pid, int deep)
        {

            string str = "";
            //的道所有的更节点
            // List<Lebi_Pro_Type> types = B_Lebi_Pro_Type.GetList("Parentid=" + pid + "", "Sort desc");
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            //将根节点进行遍历
            string style = "";
            if (deep > 0)
                style = "style=\"display:none;\"";
            string showids = LB.Tools.CookieTool.GetCookieString("showTypeids").Replace("%2C", ",");
            string image = "";
            foreach (Lebi_Pro_Type t in types)
            {
                image = AdminImage("plus.gif");
                bool showson = false;
                if (showids.Contains("," + t.id + ",") || showids.Contains("," + t.Parentid + ","))
                {

                    style = "";
                }
                if (showids.Contains("," + t.id + ","))
                {
                    image = AdminImage("minus.gif");
                    showson = true;

                }
                int count = B_Lebi_Pro_Type.Counts("Parentid=" + t.id + "");
                //str += "<tr onclick='javascript:selectrow(\"check" + t.id + "\");' name=\"tr" + t.Parentid + "\" " + style + " id=\"tr" + t.id + "\">";
                str += "<tr class=\"list\" name=\"tr" + t.Parentid + "\" " + style + " id=\"tr" + t.id + "\">";
                str += "<td>" + deepstr(deep);
                if (count > 0)
                    str += "<img src=\"" + image + "\" name=\"img" + t.Parentid + "\" id=\"img" + t.id + "\" style=\"cursor: pointer; text-align: center; vertical-align:absmiddle\" onclick=\"ShowChild('" + findpath(t.id) + "'," + t.id + "," + (deep + 1) + ")\" title=\"" + Tag("展开") + "\" />&nbsp;&nbsp;";
                else
                    str += "<img src=\"" + AdminImage("minus.gif") + "\" style=\"cursor: pointer; text-align: center; vertical-align:absmiddle\" />&nbsp;&nbsp;";
                if (t.ImageSmall != "")
                {
                    str += "<img src=\"" + t.ImageSmall + "\" height=\"16\" />&nbsp;";
                }
                str += Language.Content(t.Name, CurrentLanguage.Code) + "&nbsp;<a href=\"" + Shop.Bussiness.ThemeUrl.GetURL("P_ProductCategory", t.id.ToString(), "", CurrentLanguage.Code) + "\" target=\"_blank\"><img src=\"" + PageImage("icon/newWindow.png") + "\" style=\"vertical-align:absmiddle\" /></a></td>";
                str += "<td>" + Shop.Bussiness.EX_Product.TypeProductCount(t.id) + "</td>";
                str += "<td>" + Shop.Bussiness.EX_Product.LikeProductCount(t.id) + "</td>";
                str += "<td>" + Shop.Bussiness.EX_Product.SalesProductCount(t.id) + "</td>";
                str += "<td>" + Shop.Bussiness.EX_Product.ViewsProductCount(t.id) + "</td>";
                str += "</tr>";
                if (showson)
                    str += CreateTree(t.id, deep + 1);

                //CreateSunNode(int.Parse(ds.Tables[0].Rows[i]["id"].ToString()), ",0", ss);
            }
            return str;
        }
        private string deepstr(int deep)
        {
            string str = "";
            for (int i = 0; i < deep; i++)
            {
                str += "&nbsp;&nbsp;&nbsp;";
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