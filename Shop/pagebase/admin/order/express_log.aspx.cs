using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class Express_Log : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Express_Log> models;
        protected string PageString;
        protected string Status;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("express_log_list", "打印清单列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            Status = RequestTool.RequestString("Status");
            string where = "1=1";
            if (key != "")
                where += " and (Number like lbsql{'%" + key + "%'} or Transport_Name like lbsql{'%" + key + "%'})";
            if (Status != "")
                where += " and Status = " + Status;
            models = B_Lebi_Express_Log.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Express_Log.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&Status=" + Status + "&key=" + key, page, PageSize, recordCount);
            
        }

        public void LicenseWord()
        {
            if (!Shop.LebiAPI.Service.Instanse.Check("kuaididan"))
            {
                Response.Write("<div class=\"licensealt\"><p class=\"title\">" + Tag("敬告") + "：</p>");
                Response.Write("打印物流单是受限功能，现在您可以免费开通所有受限功能，<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/free\" target=\"_bank\">点此开通</a>");
                Response.Write("</div>");
                return;
            }
        }
    }
}