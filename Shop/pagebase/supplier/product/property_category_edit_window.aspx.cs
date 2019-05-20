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
    public partial class property_category_edit_window : SupplierAjaxBase
    {
        protected string ids;
        protected int tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            ids = RequestTool.RequestString("ids");
            tid = RequestTool.RequestInt("tid", 133);
            if (!Power("supplier_property", "属性规格"))
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