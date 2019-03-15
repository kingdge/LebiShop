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
    public partial class shipping_shouhuo_window : AdminAjaxBase
    {
        protected Lebi_Order order;
        protected List<Lebi_Order_Product> pros;
        protected Lebi_Transport_Order torder;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            torder = B_Lebi_Transport_Order.GetModel(id);
            if (torder == null)
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            string where = "id = " + torder.Order_id + "";
            if (!string.IsNullOrEmpty(EX_Admin.Project().Site_ids))
            {
                where += " and (Site_id in (" + EX_Admin.Project().Site_ids + "))";
            }
            if (!string.IsNullOrEmpty(EX_Admin.Project().Supplier_ids))
            {
                where += " and (Supplier_id in (" + EX_Admin.Project().Supplier_ids + "))";
            }
            order = B_Lebi_Order.GetModel(where);
            if (order == null)
            {
                Response.Write(Tag("参数错误"));
                Response.End();
                return;
            }
            pros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
            
        }
    }
}