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
    public partial class Express_LogList : SupplierPageBase
    {
        protected string key;
        protected List<Lebi_Express_LogList> models;
        protected int id;
        protected string PageString;
        protected string Transport_Name;
        protected string Status;
        protected int Tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_express_print", "打印清单"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            key = RequestTool.RequestString("key");
            Status = RequestTool.RequestString("Status");
            id = RequestTool.RequestInt("id", 0);
            Tid = RequestTool.RequestInt("Tid", 0);
            Lebi_Express_Log modellog = B_Lebi_Express_Log.GetModel(id);
            Transport_Name = modellog.Transport_Name;
            PageSize = RequestTool.getpageSize(25);
            string where = "Supplier_id = " + CurrentSupplier.id + " and Express_Log_Id = " + id;
            if (key != "")
                where += " and (Order_Code like lbsql{'%" + key + "%'} or Order_Id in (select Transport_Name from [Lebi_Order] where id = Order_Id and Transport_Name like lbsql{'%" + key + "%'}))";
            if (Status != "")
                where += " and Status = " + Status;
            models = B_Lebi_Express_LogList.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Express_LogList.Counts(where);

            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&Tid=" + Tid + "&id=" + id + "&Status=" + Status + "&key=" + key, page, PageSize, recordCount);
            
        }
    }
}