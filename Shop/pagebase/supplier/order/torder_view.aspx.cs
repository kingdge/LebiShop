using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.order
{
    public partial class torder_view : SupplierAjaxBase
    {
        protected Lebi_Order model;
        protected List<Lebi_Order_Product> pros;
        protected List<Lebi_Log> Logs;
        protected List<Lebi_Transport_Order> torders;
        protected List<Lebi_Comment> comms;
        protected List<Lebi_Language_Code> langs;
        protected string str = "";
        protected string billstatus = "";//发票状态
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_order_edit", "编辑订单"))
            {
                NewPageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);

            model = B_Lebi_Order.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
            {
                model = new Lebi_Order();
            }
            if (CurrentSupplier.id != model.Supplier_id)
            {
                PageError();
                return;
            }
            //if (model.Type_id_OrderType == 212)
            //    str = "- ";
            //if (model.Type_id_OrderType == 212)
            //    Response.Redirect("Torder_view.aspx?id=" + id);
            pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
            Logs = B_Lebi_Log.GetList("TableName='Order' and Keyid='" + model.id + "'", "id desc");
            torders = B_Lebi_Transport_Order.GetList("Order_id="+model.id+"", "id desc");
            comms = B_Lebi_Comment.GetList("TableName='Order' and Keyid=" + model.id + "", "id desc");
            langs = Language.Languages();
            Lebi_Bill bill = B_Lebi_Bill.GetModel("Order_id="+model.id+"");
            if (bill == null)
            {
                billstatus = Tag("不开发票");
            }
            else
            {
                billstatus = Tag(EX_Type.TypeName(bill.Type_id_BillStatus));
            }
        }

    }
}