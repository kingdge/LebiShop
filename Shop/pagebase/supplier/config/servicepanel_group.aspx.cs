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
    public partial class ServicePanel_Group : SupplierPageBase
    {
        protected int type;
        protected string key;
        protected List<Lebi_ServicePanel_Group> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            models = B_Lebi_ServicePanel_Group.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_ServicePanel_Group.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}