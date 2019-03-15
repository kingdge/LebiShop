using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
namespace Shop.Admin.Config
{
    public partial class Currency : AdminPageBase
    {
        protected List<Lebi_Currency> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("currency_list", "货币列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            string where = "1=1";

            PageSize = RequestTool.getpageSize(25);

            models = B_Lebi_Currency.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Currency.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
        }


    }
}