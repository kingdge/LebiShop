using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.theme
{
    public partial class adminskin : AdminPageBase
    {
        protected List<Lebi_AdminSkin> models;
        protected string key;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_skin_list", "模板页面列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            key = RequestTool.RequestString("key");
            string where = "";
            if (key != "")
                where = "Code like lbsql{'%" + key + "%'} or [Name] like lbsql{'%" + key + "%'}";
            models = B_Lebi_AdminSkin.GetList(where, "Sort desc,Code asc", PageSize, page);
            int recordCount = B_Lebi_AdminSkin.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
        }

    }
}