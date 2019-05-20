using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.product
{
    public partial class product_Image_Edit_window : SupplierAjaxBase
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
                if (!Power("supplier_product_add", "添加商品"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!Power("supplier_product_edit", "编辑商品"))
                {
                    WindowNoPower();
                }
            }
            ids = RequestTool.RequestString("ids");
            model = B_Lebi_Product.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
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