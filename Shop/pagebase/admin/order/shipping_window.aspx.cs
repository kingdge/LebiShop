using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class shipping_window : AdminAjaxBase
    {
        protected Lebi_Order model;
        protected List<Lebi_Order_Product> pros;
        protected Lebi_Transport_Order transport_order;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("order_shipping", "订单发货"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);
            string where = "id = " + id + "";
            if (!string.IsNullOrEmpty(EX_Admin.Project().Site_ids))
            {
                where += " and (Site_id in (" + EX_Admin.Project().Site_ids + "))";
            }
            if (!string.IsNullOrEmpty(EX_Admin.Project().Supplier_ids))
            {
                where += " and (Supplier_id in (" + EX_Admin.Project().Supplier_ids + "))";
            }
            model = B_Lebi_Order.GetModel(where);
            if (model == null)
                model = new Lebi_Order();

            pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
            transport_order = B_Lebi_Transport_Order.GetModel("Order_id = "+ id);
            if (transport_order == null)
                transport_order = new Lebi_Transport_Order();
        }
    }
}