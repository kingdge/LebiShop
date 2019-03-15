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
    public partial class Supplier_ProductType_edit_window : SupplierAjaxBase
    {
        protected string ids;
        protected void Page_Load(object sender, EventArgs e)
        {
            ids = RequestTool.RequestString("ids");
            if (!Power("supplier_product_edit", "编辑商品"))
            {
                WindowNoPower();
            }
            if (ids == "")
            {
                Response.Write("参数错误");
                Response.End();
            }
        }
    }
}