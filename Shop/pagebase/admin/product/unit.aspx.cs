using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class Unit : AdminPageBase
    {

        protected List<Lebi_Units> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("pro_units_list", "商品单位列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            models = B_Lebi_Units.GetList(where, "", PageSize, page);
            int recordCount = B_Lebi_Units.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
            
        }
    }
}