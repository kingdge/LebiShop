using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class Delivery : SupplierPageBase
    {

        protected List<Lebi_Supplier_Delivery> models;
        protected string PageString;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_delivery_list", "配送点管理"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            if (key != "")
                where += " and ([Name] like lbsql{'%" + key + "%'} or [Remark] like lbsql{'%" + key + "%'})";
            models = B_Lebi_Supplier_Delivery.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Delivery.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
            
        }
    }
}