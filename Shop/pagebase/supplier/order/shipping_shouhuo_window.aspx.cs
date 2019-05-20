using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;
using System.Web.Script.Serialization;

namespace Shop.Supplier.order
{
    public partial class shipping_shouhuo_window : SupplierAjaxBase
    {
        protected Lebi_Order order;
        protected List<Lebi_Order_Product> pros;
        protected Lebi_Transport_Order torder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_order_edit", "编辑订单"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            torder = B_Lebi_Transport_Order.GetModel(id);
            if (torder == null)
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            order = B_Lebi_Order.GetModel(torder.Order_id);
            if (order.Supplier_id != CurrentSupplier.id)
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            pros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
            
        }
    }
}