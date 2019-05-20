using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.order
{
    public partial class Default : SupplierPageBase
    {
        protected List<Lebi_Order> models;
        protected string PageString;
        protected int t;
        protected string mark;
        protected string type;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            type = RequestTool.RequestString("type");
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            t = RequestTool.RequestInt("t", 211);
            mark = RequestTool.RequestString("mark");
            if (t == 212)
            {
                if (!Power("supplier_order_return_list", "退货订单列表"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            else
            {
                if (!Power("supplier_order_list", "订单列表"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            //string where = "Supplier_id = " + CurrentSupplier.id + " and Type_id_OrderType = 213 and IsVerified=1";
            string where = "Supplier_id = " + CurrentSupplier.id + " and Type_id_OrderType = " + t;
            if (key != "")
                where += " and (Code like lbsql{'%" + key + "%'} or User_UserName like lbsql{'%" + key + "%'} or T_Name like lbsql{'%" + key + "%'} or Transport_Name like lbsql{'%" + key + "%'})";
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Add,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Add,'" + FormatDate(lbsql_dateTo) + "')>=0)";
            switch (type.ToLower())
            {
                case "1":
                    where += " and IsVerified = 0 and IsInvalid = 0";
                    break;
                case "2":
                    where += " and IsVerified = 1 and IsInvalid = 0";
                    break;
                case "3":
                    where += " and IsVerified = 1 and IsPaid = 0 and IsInvalid = 0";
                    break;
                case "4":
                    where += " and IsPaid = 1 and IsShipped = 0 and IsInvalid = 0";
                    break;
                case "5":
                    where += " and IsVerified = 1 and IsShipped = 0 and IsInvalid = 0";
                    break;
                case "6":
                    where += " and IsVerified = 1 and IsShipped = 1 and IsInvalid = 0";
                    break;
                case "7":
                    where += " and IsVerified = 1 and IsShipped_All = 1 and IsInvalid = 0";
                    break;
                case "8":
                    where += " and IsVerified = 1 and IsReceived = 0 and IsInvalid = 0";
                    break;
                case "9":
                    where += " and IsVerified = 1 and IsReceived = 1 and IsInvalid = 0";
                    break;
                case "10":
                    where += " and IsVerified = 1 and IsCompleted = 1 and IsInvalid = 0";
                    break;
                case "11":
                    where += " and IsInvalid = 1";
                    break;
                case "12":
                    where += " and IsRefund = 2";
                    break;
                case "13":
                    where += " and IsRefund = 1";
                    break;
            }
            if (mark != "")
                where += " and Mark = " + int.Parse(mark);
            PageSize = RequestTool.getpageSize(25);

            models = B_Lebi_Order.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Order.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&t=" + t + "&type=" + type + "&mark=" + mark + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&key=" + key, page, PageSize, recordCount);
        }

        public string getstatusicon(int stat)
        {
            string str = "";
            if (stat == 1)
                str = site.AdminImagePath + "icon_yes.gif";
            else
                str = site.AdminImagePath + "icon_no.gif";
            return "<img src=\"" + str + "\" />";
        }

    }
}