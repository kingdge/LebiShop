using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.promotion
{
    public partial class Promotion : AdminPageBase
    {
        protected List<Lebi_Promotion> models;

        protected Lebi_Promotion_Type pt;
        protected string PageString;
        protected int tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("promotion_list", "促销活动列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            tid = RequestTool.RequestInt("tid", 0);

            pt = B_Lebi_Promotion_Type.GetModel(tid);
            if (pt == null)
            {
                PageError();
                return;
            }
            string where = "Promotion_Type_id=" + tid;
            PageSize = RequestTool.getpageSize(25);
            models = B_Lebi_Promotion.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Promotion.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&tid=" + tid, page, PageSize, recordCount);
        }
        public string getstatusicon(int stat)
        {
            string str = "";
            if (stat == 1)
                str = site.AdminImagePath + "icon_yes.gif";
            else
                str = site.AdminImagePath + "icon_no.gif";
            return "<img src=\"" + str + "\" />";
        }

    }
}