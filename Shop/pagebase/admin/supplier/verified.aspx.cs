using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Verified : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Supplier_Verified> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_verified", "身份验证"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "1=1";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            models = B_Lebi_Supplier_Verified.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Verified.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}