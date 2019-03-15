using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.statis
{
    public partial class supplierorder : AdminPageBase
    {
        protected string key;
        protected string peisongdian;
        protected string dateFrom;
        protected string dateTo;
        protected int IsPay;
        protected int Pay_id;
        protected int Transport_id;
        protected string supplier_id;
        protected List<Lebi_Supplier> suppliers;
        protected List<Lebi_Order> orders;
        protected List<Lebi_Pay> pays;
        protected List<Lebi_Transport> transports;
        protected string PageString;
        protected string where = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("statis_orderproduct", "订单报表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            key = RequestTool.RequestString("key");
            peisongdian = RequestTool.RequestString("peisongdian");
            IsPay = RequestTool.RequestInt("IsPay",-1);
            Pay_id = RequestTool.RequestInt("Pay_id", 0);
            Transport_id = RequestTool.RequestInt("Transport_id", 0);
            supplier_id = RequestTool.RequestString("supplier_id");
            dateFrom = RequestTool.RequestString("dateFrom");
            if (dateFrom == "")
                dateFrom = System.DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            dateTo = RequestTool.RequestString("dateTo");
            if (dateTo == "")
                dateTo = System.DateTime.Now.AddDays(0).ToString("yyyy-MM-dd");

            suppliers = B_Lebi_Supplier.GetList("", "");
            where = "1=1";
            
            if (IsPay !=-1)
                where += " and IsPaid = " + IsPay;
            if (Pay_id != 0)
                where += " and Pay_id = " + Pay_id;
            if (Transport_id != 0)
                where += " and Transport_id = " + Transport_id;
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + dateFrom + "' and Time_Add<='" + dateTo + " 23:59:59'";
            if (supplier_id != "")
            {
                try
                {
                    supplier_id = Convert.ToInt32(supplier_id).ToString();
                }
                catch
                {
                    supplier_id = "0";
                }
                where += " and Supplier_id = " + supplier_id;
            }
            if (peisongdian != "")
                where += " and  Supplier_Delivery_id  in (select w.id from [Lebi_Supplier_Delivery] as w where w.Name like '%" + peisongdian + "%')";
           
            if (key != "")
                where += " and (Code like lbsql{'%" + key + "%'} or T_Name like lbsql{'%" + key + "%'}))";
            PageSize = RequestTool.getpageSize(25);
            page = RequestTool.RequestInt("page");
            orders = B_Lebi_Order.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Order.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&peisongdian=" + peisongdian + "&supplier_id=" + supplier_id + "&dateTo=" + dateTo + "&dateFrom=" + dateFrom + "&IsPay=" + IsPay + "&Pay_id=" + Pay_id + "&Transport_id=" + Transport_id, page, PageSize, recordCount);
            where = Server.UrlEncode(where);
            pays = B_Lebi_Pay.GetList("", "Sort desc");
            transports = B_Lebi_Transport.GetList("", "Sort desc");
            //Response.Write(where);
        }

        public Lebi_Supplier GetSupplier(int id)
        {
            Lebi_Supplier model = B_Lebi_Supplier.GetModel(id);
            if (model == null)
                model = new Lebi_Supplier();
            return model;
        }

        public List<Lebi_Order_Product> GetOrderProduct(int orderid)
        {
            List<Lebi_Order_Product> models = B_Lebi_Order_Product.GetList("Order_id=" + orderid + "", "");
            return models;
        }
        public Lebi_Supplier_Delivery GetDelivery(int id)
        {
            Lebi_Supplier_Delivery model = B_Lebi_Supplier_Delivery.GetModel(id);
            if (model == null)
                model = new Lebi_Supplier_Delivery();
            return model;
        }
    }
}