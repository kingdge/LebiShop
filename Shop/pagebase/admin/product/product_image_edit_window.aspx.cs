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
    public partial class product_Image_Edit_window : AdminAjaxBase
    {
        protected Lebi_Product model;
        protected string ids;
        protected int id;
        protected int randnum;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestTool.RequestInt("id", 0);
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
            ids = RequestTool.RequestString("ids");
            model = B_Lebi_Product.GetModel(id);
            if (ids != "")
            {
                List<Lebi_Product> models = B_Lebi_Product.GetList("id in (lbsql{" + ids + "})", "");
                //if (models.Count == 1)
                model = models.FirstOrDefault();
            }

            if (model == null)
            {
                model = new Lebi_Product();
                //Response.Write("参数错误");
                //Response.End();
            }



        }
    }
}