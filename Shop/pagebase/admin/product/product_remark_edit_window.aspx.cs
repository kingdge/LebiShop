using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class product_remark_edit_window : AdminAjaxBase
    {
        protected Lebi_Product model;
        protected int id;
        protected int pid;
        protected int randnum;
        protected string removelang;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestTool.RequestInt("id", 0);
            pid = RequestTool.RequestInt("pid", 0);
            randnum = RequestTool.RequestInt("randnum", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("product_add", "添加商品"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("product_edit", "编辑商品"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Product.GetModel(id);
            if (model == null)
            {
                Response.Write("参数错误");
                Response.End();
            }
        }
    }
}