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
    public partial class skin : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Supplier_Skin> models;
        protected string PageString;
        protected string type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_skin_list", "店铺皮肤列表"))
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
            models = B_Lebi_Supplier_Skin.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Skin.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);

        }
    }
}