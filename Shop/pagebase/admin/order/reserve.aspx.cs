using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class reserve : AdminPageBase
    {
        protected List<Lebi_Order_Product> models;
        protected string PageString;
        protected string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("order_list", "订单列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            type = RequestTool.RequestString("type");
            string where = "Type_id_OrderProductType=256 and IsPaidReserve=1";
            switch (type.ToLower())
            {
                case "1":
                    where += " and IsStockOK=0";
                    break;
                case "2":
                    where += " and IsStockOK = 1";
                    break;
                case "3":
                    where += " and IsPaid = 0";
                    break;
                case "4":
                    where += " and IsPaid = 1";
                    break;
                case "5":
                    where += " and Count_Shipped=0";
                    break;
                case "6":
                    where += " and Count_Shipped>0";
                    break;
            }
            PageSize = RequestTool.getpageSize(25);
            models = B_Lebi_Order_Product.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Order_Product.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&type=" + type, page, PageSize, recordCount);
        }
        public Lebi_Order getorder(int id)
        {
            Lebi_Order order = B_Lebi_Order.GetModel(id);
            if (order == null)
                order = new Lebi_Order();
            return order;
        }
        public Lebi_Product getproduct(int id)
        {
            Lebi_Product p = B_Lebi_Product.GetModel(id);
            return p;
        }
    }
}