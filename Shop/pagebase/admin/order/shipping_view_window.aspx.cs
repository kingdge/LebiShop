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
    public partial class shipping_view_window : AdminAjaxBase
    {
        protected Lebi_Order model;
        protected List<TransportProduct> tps;
        protected Lebi_Transport_Order torder;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("order_shipping", "订单发货"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);
            torder = B_Lebi_Transport_Order.GetModel(id);
            if (torder == null) 
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            string where = "Order_id = " + torder.Order_id + "";
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
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            tps = new List<TransportProduct>();
            JavaScriptSerializer jss = new JavaScriptSerializer();
            try
            {
                tps = jss.Deserialize<List<TransportProduct>>(torder.Product);
            }
            catch (Exception)
            {
                tps = new List<TransportProduct>();
            }
        }
    }
}