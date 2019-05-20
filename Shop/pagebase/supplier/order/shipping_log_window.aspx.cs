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

namespace Shop.supplier.order
{
    public partial class shipping_log_window : SupplierAjaxBase
    {
        protected Lebi_Order model;
        protected List<TransportProduct> tps;
        protected Lebi_Transport_Order torder;
        protected KuaiDi100 log;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_order_shipping", "订单发货"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            torder = B_Lebi_Transport_Order.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (torder == null)
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }

            log = EX_Area.GetKuaiDi100(torder);

        }
    }
}