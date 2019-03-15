using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.cms
{
    public partial class UserNodeList : AdminPageBase
    {
        protected Lebi_Node UserNode;
        protected string lang;
        protected string key;
        protected string PageCode;
        protected string code;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("usernode_add", "添加自定义结点"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            int Node_id = RequestTool.RequestInt("Node_id", 0);
            UserNode = B_Lebi_Node.GetModel(Node_id);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            if (UserNode == null)
            {
                code = RequestTool.RequestString("code");
                if (code == "")
                    code = "UserNode";
                UserNode = NodePage.GetNodeByCode(code);
            }
            if (UserNode.Code == "Help")
                PageCode = "P_Help";
        }
        public string GetTreeString(int parentID, int deep)
        {
            string str = "";
            string where = "parentid=" + parentID + "";
            if (lang != "")
                where += " and Language Like lbsql{'%" + lang + "%'}";
            if (key != "")
                where += " and [Name] like lbsql{'%" + key + "%'}";
            if (site.SiteCount > 1 && CurrentAdmin.Site_ids != "")
            {
                string sonwhere = "";
                List<Lebi_Language> ls = B_Lebi_Language.GetList("Site_id in (" + CurrentAdmin.Site_ids + ")", "");
                foreach (Lebi_Language l in ls)
                {

                    if (sonwhere == "")
                        sonwhere = "','+Language_ids+',' like '%," + l.id + ",%'";
                    else
                        sonwhere += " or ','+Language_ids+',' like '%," + l.id + ",%'";

                }
                if (sonwhere != "")
                {
                    where += " and (" + sonwhere + " or Language_ids='')";
                }
            }
            List<Lebi_Node> nodes = B_Lebi_Node.GetList(where, "Sort desc");
            if (nodes.Count > 0)
            {
                foreach (Lebi_Node node in nodes)
                {

                    str += "<tr class=\"list\" onDblClick=\"Node_Edit(0," + node.id + ")\">";
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        str += "<td><label class=\"custom-control custom-checkbox\"><input type=\"checkbox\" id=\"check" + node.id + "\" name=\"ids\" value=\"" + node.id + "\" class=\"custom-control-input\" del=\"true\"><span class=\"custom-control-label\"></span></label></td>";
                    }else{ 
                        str += "<td style=\"text-align:center\"><input type=\"checkbox\" name=\"ids\" del=\"true\" value=\"" + node.id + "\" /></td>";
                    }
                    str += "<td>" + node.id + "</td>";
                    str += "<td><div class=\"more\" style=\"width: 80px;\" title=\"" + LanguageName(node.Language_ids) +"\">" + LanguageName(node.Language_ids) + "</div></td>";
                    if (deep == 0)
                    {
                        str += "<td><a href=\"" + NodePage.AdminIndexPage(node) + "\" >" + node.Name + "</a></td>";
                    }
                    else
                    {
                        str += "<td>" + GetDeep("<label class=\"subFlag\">┗</label>" + node.Name, deep) + "</td>";

                    }
                    str += "<td>";
                    str += B_Lebi_Page.Counts("node_id=" + node.id + "").ToString();
                    str += "</td>";
                    str += "<td>" + node.ShowMode + "</td>";
                    str += "<td>" + node.Sort + "</td>";
                    str += "<td style=\"text-align:left\">";

                    str += "<a href=\"#\" onclick=\"Node_Edit(0," + node.id + ");\">" + Tag("编辑") + "</a>";
      

                    if (node.haveson == 1)
                    {
                        str += "<span class=\"editSep\"> | </span>";
                        str += "<a href=\"#\" onclick=\"Node_Edit(" + node.id + ",0);\">" + Tag("创建子分类") + "</a>";
                    }
                    else
                    {
                        str += " | ";
                        str += "<a href=\"" + NodePage.AdminIndexPage(node) + "\" >" + Tag("进入") + "</a>";
                    }

                    str += "</td>";
                    str += "</tr>";
                    str += GetTreeString(node.id, deep + 1);
                }
                //str += "</ul>  \r\n";

            }

            return str;
        }
        private string GetDeep(string strIn, int deep)
        {
            string str1 = "";
            string str2 = "";
            for (int i = 0; i < deep; i++)
            {
                str1 += "<span style=\"margin-left: 20px;\">";
                str2 += "</span>";
            }
            return str1 + strIn + str2;
        }
    }
}