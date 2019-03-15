using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class BillType : AdminPageBase
    {

        protected List<Lebi_BillType> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("billtype_list", "发票类型列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            models = B_Lebi_BillType.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_BillType.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
            
        }
    }
}