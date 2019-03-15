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
    public partial class ServicePanel_Type : AdminPageBase
    {
        protected string key;
        protected List<Lebi_ServicePanel_Type> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("servicepanel_list", "客服面板软件列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "1=1";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            models = B_Lebi_ServicePanel_Type.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_ServicePanel_Type.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}