using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Web.Script.Serialization;

namespace Shop.Admin.order
{
    public partial class shipping_log_window : AdminAjaxBase
    {
        protected Lebi_Order model;
        protected List<TransportProduct> tps;
        protected Lebi_Transport_Order torder;
        protected KuaiDi100 log;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("order_shipping", "订单发货"))
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
            log = EX_Area.GetKuaiDi100(torder);
        }
    }
}