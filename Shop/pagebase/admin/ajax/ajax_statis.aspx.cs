using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace Shop.Admin.Ajax
{
    public partial class Ajax_statis : AdminAjaxBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);

        }
        #region 后台分类列表
        /// <summary>
        /// 生成分类列表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="deep"></param>
        /// <returns></returns>
        public void CreateTree()
        {
            int pid = RequestTool.RequestInt("pid");
            int deep = RequestTool.RequestInt("deep");
            string str = "";
            //的道所有的更节点
            // List<Lebi_Pro_Type> types = B_Lebi_Pro_Type.GetList("Parentid=" + pid + "", "Sort desc");
            List<Lebi_Pro_Type> types = EX_Product.Types(pid);
            //将根节点进行遍历
            string style = "";
            foreach (Lebi_Pro_Type t in types)
            {
                int count = B_Lebi_Pro_Type.Counts("Parentid=" + t.id + "");
                str += "<tr class=\"list\" name=\"tr" + t.Parentid + "\" " + style + " id=\"tr" + t.id + "\">";
                str += "<td>" + deepstr(deep);
                if (count > 0)
                    str += "<img src=\"" + AdminImage("plus.gif") + "\" name=\"img" + t.Parentid + "\" id=\"img" + t.id + "\" style=\"cursor: pointer; text-align: center; vertical-align:absmiddle\" onclick=\"ShowChild('" + findpath(t.id) + "'," + t.id + "," + (deep + 1) + ")\" title=\"" + Tag("展开") + "\" />&nbsp;&nbsp;";
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
            }
            Response.Write(str);
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
        #endregion
    }
}