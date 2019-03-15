using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class UserLevel : AdminPageBase
    {
        protected List<Lebi_UserLevel> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            string where = "Grade > 0";
            models = B_Lebi_UserLevel.GetList(where, "Grade asc", PageSize, page);
            int recordCount = B_Lebi_UserLevel.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}" , page, PageSize, recordCount);
        }
    }
}