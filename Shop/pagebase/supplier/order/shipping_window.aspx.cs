using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.order
{
    public partial class shipping_window : SupplierAjaxBase
    {
        protected Lebi_Order model;
        protected List<Lebi_Order_Product> pros;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_order_shipping", "订单发货"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);
            model = B_Lebi_Order.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
                model = new Lebi_Order();

            pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
        }
    }
}