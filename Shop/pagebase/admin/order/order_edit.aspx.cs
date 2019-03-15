using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class Order_Edit : AdminPageBase
    {
        protected Lebi_Order model;
        protected List<Lebi_Order_Product> pros;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (!EX_Admin.Power("order_edit", "编辑订单"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
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
            {
                model = new Lebi_Order();

            }
            pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
        }
    }
}