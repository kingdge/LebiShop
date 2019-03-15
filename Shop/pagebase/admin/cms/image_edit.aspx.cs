using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.cms
{
    public partial class Image_Edit : AdminPageBase
    {
        protected Lebi_Page page;
        protected Lebi_Node node;
        protected Lebi_Node pnode;
        protected void Page_Load(object sender, EventArgs e)
        {
            int Node_id = RequestTool.RequestInt("Node_id", 0);
            int Page_id = RequestTool.RequestInt("Page_id", 0);
            page = B_Lebi_Page.GetModel(Page_id);
            node = B_Lebi_Node.GetModel(Node_id);
            if (page != null)
            {
                if (!EX_Admin.Power("page_edit", "编辑结点内容"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                
                Node_id = page.Node_id;
                node = B_Lebi_Node.GetModel(Node_id);
            }
            else
            {
                if (!EX_Admin.Power("page_add", "添加结点内容"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                
                page = new Lebi_Page();
                page.Node_id = node.id;
            }
           
            if (node == null)
            {
                node = new Lebi_Node();
            }
            pnode = B_Lebi_Node.GetModel(node.parentid);
        }
        /// <summary>
        /// 递归生成下拉菜单
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="parentID"></param>
        /// <param name="nodeID"></param>
        /// <returns></returns>
        public string GetOptionTreeString(int depth, int parentID, int nodeID)
        {

            StringBuilder builderTree = new StringBuilder();
            //DataRow[] drs = dtNodes.Select(string.Format("ParentNodeID={0}", parentID));
            List<Lebi_Node> nodes = new List<Lebi_Node>();
            nodes = B_Lebi_Node.GetList("parentid=" + parentID + "", "");
            if (nodes.Count > 0)
            {
                foreach (Lebi_Node node in nodes)
                {
                    if (node.haveson == 0)
                        builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id.ToString(), node.id == nodeID ? "selected=\"selected\"" : "", GetPrefixString(depth), Language.Content(node.Name, "CN")));
                    builderTree.Append(GetOptionTreeString(depth + 1, node.id, nodeID));
                }

            }
            return builderTree.ToString();
        }
        private string GetPrefixString(int depth)
        {
            //StringBuilder builder = new StringBuilder();
            //builder.Append("--");
            //for (int i = 0; i < depth; i++)
            //{
            //    builder.Append("--");

            //}
            return "";
        }
    }
}