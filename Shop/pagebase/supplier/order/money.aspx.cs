using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;
namespace Shop.Supplier.Order
{
    public partial class UserMoney : SupplierPageBase
    {
        protected List<Lebi_Supplier_Money> models;
        protected string PageString;
        protected int status;
        protected string key;
        protected string dateFrom;
        protected string dateTo;
        protected int PageSize;
        protected decimal money = 0;
        protected decimal zmoney = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_usermoney_list", "资金明细"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            int user_id = RequestTool.RequestInt("user_id", 0);
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            status = RequestTool.RequestInt("status", 0);
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            if (status > 0)
                where += " and Type_id_PointStatus=" + status + "";
            if (key != "")
                where += " and Remark like lbsql{'%" + key + "%'}";
            if (dateFrom != "" && dateTo != "")
                where += " and (datediff(d,Time_Add,'" + FormatDate(lbsql_dateFrom) + "')<=0 and datediff(d,Time_Add,'" + FormatDate(lbsql_dateTo) + "')>=0)";
            models = B_Lebi_Supplier_Money.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Money.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&key=" + key + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&status = " + status, page, PageSize, recordCount);

            //string money_ = Common.GetValue("select sum(Money) from Lebi_Supplier_Money where Supplier_id=" + CurrentSupplier.id + " and Type_id_MoneyStatus = 181 and datediff(d,Time_add,'" + System.DateTime.Now + "')>=" + CurrentSupplier.BillingDays + "");
            string zmoney_ = Common.GetValue("select sum(Money) from Lebi_Supplier_Money where Supplier_id=" + CurrentSupplier.id + " and Type_id_MoneyStatus = 181");

            string money_fu_ = B_Lebi_Supplier_Money.GetValue("sum(Money)", "Supplier_id=" + CurrentSupplier.id + " and Type_id_MoneyStatus=181 and Type_id_SupplierMoneyType in (342,343)");
            string money_ = B_Lebi_Supplier_Money.GetValue("sum(Money)", "Supplier_id=" + CurrentSupplier.id + " and Type_id_MoneyStatus=181 and Type_id_SupplierMoneyType  in (341,344) and datediff(d,Time_add,'" + System.DateTime.Now + "')>=" + CurrentSupplier.BillingDays + "");
            decimal money_fu = 0;

            decimal.TryParse(money_, out money);
            decimal.TryParse(zmoney_, out zmoney);
            decimal.TryParse(money_fu_, out money_fu);
            money = money + money_fu;


        }
    }
}