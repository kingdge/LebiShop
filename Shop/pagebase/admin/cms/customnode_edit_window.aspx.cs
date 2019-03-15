using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Text;
namespace Shop.Admin.cms
{
    public partial class customnode_edit_window : AdminAjaxBase
    {
        protected Lebi_Node model;
        protected Lebi_Node PNode;
        protected Lebi_Node Topnode;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            int pid = RequestTool.RequestInt("pid", 0);
            model = B_Lebi_Node.GetModel(id);
            if (model == null)
            {
                if (!EX_Admin.Power("usernode_add", "添加自定义结点"))
                {
                    WindowNoPower();
                }
                model = new Lebi_Node();
                model.Type_id_PublishType = 120;
                model.haveson = 1;
            }
            else{
                if (!EX_Admin.Power("usernode_edit", "编辑自定义结点"))
                {
                    WindowNoPower();
                }
                pid = model.parentid;
            }
            PNode = B_Lebi_Node.GetModel(pid);
            if (PNode == null)
                PNode = new Lebi_Node();
            string code = RequestTool.RequestString("code");
            Topnode = NodePage.GetNodeByCode(code);
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
                    if (Topnode.IsLanguages == 1)
                        node.Name = Lang(node.Name);
                    builderTree.Append(string.Format("<option value=\"{0}\" {1}>{2}{3}</option>  \r\n", node.id.ToString(), node.id == nodeID ? "selected=\"selected\"" : "", GetPrefixString(depth), node.Name));
                    builderTree.Append(GetOptionTreeString(depth + 1, node.id, nodeID));
                }

            }
            return builderTree.ToString();
        }
        private string GetPrefixString(int depth)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < depth; i++)
            {
                builder.Append("--");

            }
            return builder.ToString();
        }
    }
}