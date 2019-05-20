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
    public partial class Express_Log : SupplierPageBase
    {
        protected string key;
        protected List<Lebi_Express_Log> models;
        protected string PageString;
        protected string Status;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_express_print", "打印清单"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            Status = RequestTool.RequestString("Status");
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            if (key != "")
                where += " and (Number like lbsql{'%" + key + "%'} or Transport_Name like lbsql{'%" + key + "%'})";
            if (Status != "")
                where += " and Status = " + Status;
            models = B_Lebi_Express_Log.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Express_Log.Counts(where);

            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&Status=" + Status + "&key=" + key, page, PageSize, recordCount);
            
        }
    }
}