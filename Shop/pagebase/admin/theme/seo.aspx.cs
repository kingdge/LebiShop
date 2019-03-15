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
    public partial class SEO : AdminPageBase
    {
        protected List<Lebi_Theme_Page> models;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("seo_edit", "SEO设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            key = RequestTool.RequestString("key");
            string where = "";
            if (key != "")
                where = " (Code like lbsql{'%" + key + "%'} or Name like lbsql{'%" + key + "%'} or PageName like lbsql{'%" + key + "%'})";
            models = B_Lebi_Theme_Page.GetList(where, "Sort desc,Code asc");
        }
    }
}