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
    public partial class Express_Shipper : SupplierPageBase
    {
        protected string key;
        protected List<Lebi_Express_Shipper> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_express_shipper_list", "发货人管理"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            if (key != "")
                where += " and SiteName like lbsql{'%" + key + "%'}";
            models = B_Lebi_Express_Shipper.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Express_Shipper.Counts(where);

            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}