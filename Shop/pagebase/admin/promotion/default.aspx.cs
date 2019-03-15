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
    public partial class Default : AdminPageBase
    {
        protected List<Lebi_Promotion_Type> models;
        protected string PageString;
        protected int t;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("promotion_list", "促销活动列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            t = RequestTool.RequestInt("t",211);
            string where = "1=1";
            models = B_Lebi_Promotion_Type.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Promotion_Type.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&t="+t, page, PageSize, recordCount);
        }

        public string getstatusicon(int stat)
        {
            string str = "";
            if (stat == 1)
                str=site.AdminImagePath + "icon_yes.gif";
            else
                str=site.AdminImagePath + "icon_no.gif";
            return "<img src=\""+str+"\" />";
        }
        public int Count(int ptid)
        {
            return B_Lebi_Promotion.Counts("Promotion_Type_id="+ptid+"");
        }

        public void LicenseWord()
        {
            if (!Shop.LebiAPI.Service.Instanse.Check("cuxiao"))
            {
                Response.Write("<div class=\"licensealt\"><p class=\"title\">" + Tag("敬告") + "：</p>");
                Response.Write("促销是受限功能，您可以编辑促销规则但是在商城前台还不能生效。</br>");
                Response.Write("现在您可以免费开通所有受限功能。<a href=\"" + Shop.LebiAPI.Service.Instanse.weburl + "/free\" target=\"_bank\">点此开通</a>");
                Response.Write("</div>");
                return;
            }
        }
    }
}