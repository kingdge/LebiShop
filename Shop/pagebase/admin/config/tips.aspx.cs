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
    public partial class Tips : AdminPageBase
    {
        protected List<Lebi_Tips> models;
        protected string key;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("tips_list", "每日箴言"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            key = RequestTool.RequestString("key");
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            if (key != "")
                where += " and [Content] like lbsql{'%" + key + "%'}";
            models = B_Lebi_Tips.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Tips.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}