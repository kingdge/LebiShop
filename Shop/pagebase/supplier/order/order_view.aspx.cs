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
    public partial class Order_View : SupplierAjaxBase
    {
        protected Lebi_Order model;
        protected List<Lebi_Order_Product> pros;
        protected List<Lebi_Log> Logs;
        protected List<Lebi_Transport_Order> torders;
        protected List<Lebi_Comment> comms;
        protected List<Lebi_Language_Code> langs;
        protected List<Lebi_Supplier_Delivery> deliveries;
        protected int DeliveryCount;
        protected int CommentCount;
        protected int LogCount;
        protected int TransportCount;
        protected string str = "";
        protected string billstatus = "";//发票状态
        protected Lebi_Supplier shop;
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
                PageError();
                return;
            }
            else
            {
                shop = CurrentSupplier;
                if (shop == null)
                    shop = new Lebi_Supplier();

                if (CurrentSupplier.id != model.Supplier_id)
                {
                    PageError();
                    return;
                }

            }
            if (model.Type_id_OrderType == 212)
                str = "- ";
            //if (model.Type_id_OrderType == 212)
            //    Response.Redirect("Torder_view.aspx?id=" + id);
            pros = B_Lebi_Order_Product.GetList("Order_id=" + model.id + "", "");
            Logs = B_Lebi_Log.GetList("TableName='Order' and Keyid='" + model.id + "'", "id desc");
            torders = B_Lebi_Transport_Order.GetList("Order_id=" + model.id + "", "id desc");
            comms = B_Lebi_Comment.GetList("TableName='Order' and Keyid=" + model.id + "", "id desc");
            langs = Language.Languages();
            Lebi_Bill bill = B_Lebi_Bill.GetModel("Order_id=" + model.id + "");
            if (bill == null)
            {
                billstatus = Tag("不开发票");
            }
            else
            {
                billstatus = Tag(EX_Type.TypeName(bill.Type_id_BillStatus));
            }
            TransportCount = B_Lebi_Transport_Order.Counts("Order_id=" + model.id + "");
            CommentCount = B_Lebi_Comment.Counts("TableName='Order' and Keyid=" + model.id + "");
            LogCount = B_Lebi_Log.Counts("TableName='Order' and Keyid='" + model.id + "'");
            deliveries = B_Lebi_Supplier_Delivery.GetList("Supplier_id = " + model.Supplier_id + "", "Sort desc");
            DeliveryCount = B_Lebi_Supplier_Delivery.Counts("Supplier_id = " + model.Supplier_id + "");
        }

    }
}