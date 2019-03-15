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
    public partial class NodeList : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("node_add", "添加结点"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
        }
        public string GetTreeString(int parentID, int deep)
        {
            string str = "";
            List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + parentID + "", "Sort desc");
            if (nodes.Count > 0)
            {
                foreach (Lebi_Node node in nodes)
                {
                    str += "<tr class=\"list\" onDblClick=\"Node_Edit(0," + node.id + ")\">";
                    str += "<td><strong>";
                    str += node.Code;
                    str += "</strong></td>";

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
                    str += NodePage.NodeType(node.TypeFlag);
                    str += "</td>";
                    str += "<td>";
                    str += (node.IsLanguages==1?"Yes":"No");
                    str += "</td>";
                    str += "<td>";
                    str += B_Lebi_Page.Counts("node_id=" + node.id + "").ToString();
                    str += "</td>";
                    
                    str += "<td>";
                    //str += "<a href=\"#\" onclick=\"UpdateNode(" + node.ParentNodeID + ",0);\">" + Tag("创建同级结点") + "</a>";
                    //str += "<span class=\"editSep\">|</span>";

                    str += "<a href=\"#\" onclick=\"Node_Edit(0," + node.id + ");\">" + Tag("修改") + "</a>";
                    str += " | ";
                    str += "<a href=\"#\" onclick=\"Node_Del(" + node.id + ");\">" + Tag("删除") + "</a>";
                    if (node.haveson == 1)
                    {
                        str += " | ";
                        str += "<a href=\"#\" onclick=\"Node_Edit(" + node.id + ",0);\">" + Tag("创建子结点") + "</a>";
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