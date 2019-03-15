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
    public partial class customnode : AdminPageBase
    {
        protected Lebi_Node Topnode;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("usernode_add", "添加自定义结点"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            string code = RequestTool.RequestString("code");
            Topnode = NodePage.GetNodeByCode(code);
        }
        public string GetTreeString(int parentID, int deep)
        {
            string str = "";
            List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + parentID + "", "Sort desc");
            if (nodes.Count > 0)
            {
                foreach (Lebi_Node node in nodes)
                {
                    if (Topnode.IsLanguages == 1)
                        node.Name = Lang(node.Name);
                    str += "<tr class=\"list\" onDblClick=\"Node_Edit(0," + node.id + ")\">";
                    str += "<td style=\"text-align:center\"><input type=\"checkbox\" name=\"ids\" del=\"true\" value=\"" + node.id + "\" /></td>";
                    str += "<td>" + node.id + "</td>";
                    str += "<td>" + node.Sort + "</td>";
                    if (deep == 0)
                    {
                        str += "<td><strong>" + node.Name + "</strong></td>";
                    }
                    else
                    {
                        str += "<td>" + GetDeep("<label class=\"subFlag\">┗</label>" + node.Name, deep) + "</td>";

                    }
                    str += "<td>";

                    str += "<a href=\"#\" onclick=\"Node_Edit(0," + node.id + ");\">" + Tag("编辑") + "</a>";
                    if (Topnode.haveson == 1)
                        str += " | <a href=\"#\" onclick=\"Node_Edit(" + node.id + ",0);\">" + Tag("创建子结点") + "</a>";
                    str += "</td>";
                    str += "</tr>";
                    str += GetTreeString(node.id, deep + 1);
                }
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