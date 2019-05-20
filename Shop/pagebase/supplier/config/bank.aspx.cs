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
    public partial class Bank : SupplierPageBase
    {

        protected List<Lebi_Supplier_Bank> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_bank_list", "收款账号"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            models = B_Lebi_Supplier_Bank.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Bank.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
            
        }
    }
}