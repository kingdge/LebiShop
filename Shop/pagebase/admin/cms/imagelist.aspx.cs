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
    public partial class ImageList : AdminPageBase
    {
        protected Lebi_Node node;
        protected Lebi_Node pnode;
        protected List<Lebi_Page> pages;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("image_del", "删除图库"))
            {
                PageNoPower();
            }
            PageSize = RequestTool.getpageSize(25);
            int Node_id = RequestTool.RequestInt("Node_id", 0);
            node = B_Lebi_Node.GetModel(Node_id);
            if (node == null)
            {
                Response.Write("参数错误");
                Response.End();
            }
            pnode = B_Lebi_Node.GetModel(node.parentid);
            if (pnode == null)
                pnode = new Lebi_Node();
            string where = "Node_id=" + node.id + "";
            int recordCount = B_Lebi_Page.Counts(where);
            pages = B_Lebi_Page.GetList(where, "Sort desc,id desc", PageSize, page);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
        }
    }
}