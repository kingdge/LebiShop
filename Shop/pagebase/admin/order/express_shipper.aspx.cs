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
    public partial class Express_Shipper : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Express_Shipper> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("express_shipper_list", "发货人列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "1=1";
            if (key != "")
                where += " and SiteName like lbsql{'%" + key + "%'}";
            models = B_Lebi_Express_Shipper.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Express_Shipper.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}