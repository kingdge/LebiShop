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
    public partial class Supplier_Group : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Supplier_Group> models;
        protected string PageString;
        protected string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_group_list", "商家分组列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            type = RequestTool.RequestString("type");
            string where = "1=1";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            if (type != "")
                where += " and type=lbsql{'" + type + "'}";
            models = B_Lebi_Supplier_Group.GetList(where, "Grade asc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Group.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);

        }
    }
}