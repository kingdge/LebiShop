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
    public partial class Order_View : AdminAjaxBase
    {
        protected Lebi_Order model;
        protected Lebi_Order oldmodel;
        protected List<Lebi_Order_Product> pros;
        protected List<Lebi_Log> Logs;
        protected List<Lebi_Transport_Order> torders;
        protected List<Lebi_Comment> comms;
        protected List<Lebi_Language_Code> langs;
        protected List<Lebi_Supplier_Delivery> deliveries;
        protected int DeliveryCount;
        protected string str = "";
        protected string billstatus = "";//发票状态
        protected Lebi_Supplier shop;
        protected int CommentCount;
        protected int LogCount;
        protected int TransportCount;
        protected string Promotion = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            string where = "id = "+ id +"";
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
                shop = new Lebi_Supplier();
            }
            else
            {
                if (model.Type_id_OrderType == 212)
                {
                    if (!EX_Admin.Power("order_return_list", "退货订单列表"))
                    {
                        NewPageNoPower();
                    }
                }
                else
                {
                    if (!EX_Admin.Power("order_list", "订单列表"))
                    {
                        NewPageNoPower();
                    }
                }
                if (model.Supplier_id > 0)
                    shop = B_Lebi_Supplier.GetModel(model.Supplier_id);
                if (shop == null)
                    shop = new Lebi_Supplier();
                if (domain3admin && CurrentAdmin.Site_ids != "")
                {
                    if (!("," + CurrentAdmin.Site_ids + ",").Contains("," + model.Site_id + ","))
                    {
                        PageError();
                        return;
                    }
                }
            }
            if (model.Type_id_OrderType == 212)
                str = "- ";
            oldmodel = B_Lebi_Order.GetModel(model.Order_id);
            if (oldmodel == null)
            {
                oldmodel = new Lebi_Order();
            }
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
                billstatus = EX_Type.TypeName(bill.Type_id_BillStatus);
            }
            TransportCount = B_Lebi_Transport_Order.Counts("Order_id=" + model.id + "");
            CommentCount = B_Lebi_Comment.Counts("TableName='Order' and Keyid=" + model.id + "");
            LogCount = B_Lebi_Log.Counts("TableName='Order' and Keyid='" + model.id + "'");
            deliveries = B_Lebi_Supplier_Delivery.GetList("Supplier_id = " + model.Supplier_id + "", "Sort desc");
            DeliveryCount = B_Lebi_Supplier_Delivery.Counts("Supplier_id = " + model.Supplier_id + "");
            if (model.Promotion_Type_ids != "")
            {
                //List<Lebi_Promotion_Type> Promotions = B_Lebi_Promotion_Type.GetList("id in (" + model.Promotion_Type_ids + ")", "");
                //foreach (Lebi_Promotion_Type p in Promotions)
                //{
                //    if (Promotion == "")
                //        Promotion = Lang(p.Name);
                //    else
                //        Promotion += "<br/>" + Lang(p.Name);
                //}
                Promotion = Lang(model.Promotion_Type_Name);
            }
        }

    }
}