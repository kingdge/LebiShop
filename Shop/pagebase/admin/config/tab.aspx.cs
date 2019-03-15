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
    public partial class Tab : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected int mode;
        protected int position;
        protected List<Lebi_Tab> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            mode = RequestTool.RequestInt("mode",0);
            position = RequestTool.RequestInt("position", 0);
            if (lang == "")
                lang = "CN";
            string where = "1=1";
            if (mode > 0)
                where += " and Mode = " + mode + "";
            if (position > 0)
                where += " and Position = " + position + "";
            if (key != "")
                where += " and Tname like lbsql{'%" + key + "%'}";
            models = B_Lebi_Tab.GetList(where, "", PageSize, page);
            int recordCount=B_Lebi_Tab.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&lang=" + lang + "&mode=" + mode + "&position=" + position, page, PageSize, recordCount);
            
        }
    }
}