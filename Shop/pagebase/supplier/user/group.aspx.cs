using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.user
{
    public partial class Group : SupplierPageBase
    {
        protected string key;
        protected List<Lebi_Supplier_UserGroup> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_usergroup_list", "用户分组列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "Supplier_id=" + CurrentSupplier.id;
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            models = B_Lebi_Supplier_UserGroup.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_UserGroup.Counts(where);

            PageString =Shop.Bussiness.Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);

        }
    }
}