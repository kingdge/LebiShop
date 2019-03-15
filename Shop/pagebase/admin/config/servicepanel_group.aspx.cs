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
    public partial class ServicePanel_Group : AdminPageBase
    {
        protected string lang;
        protected int type;
        protected string key;
        protected List<Lebi_ServicePanel_Group> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("servicepanel_group_list", "客服面板部门列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            string where = "Supplier_id=0";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            //if (lang != "")
            //    where += " and Language like lbsql{'%" + lang + "%'}";
            if (site.SiteCount > 1 && CurrentAdmin.Site_ids != "")
            {
                string sonwhere = "";
                List<Lebi_Language> ls = B_Lebi_Language.GetList("Site_id in (" + CurrentAdmin.Site_ids + ")", "");
                foreach (Lebi_Language l in ls)
                {

                    if (sonwhere == "")
                        sonwhere = "','+Language_ids+',' like '%," + l.id + ",%'";
                    else
                        sonwhere += " or ','+Language_ids+',' like '%," + l.id + ",%'";

                }
                where += " and (" + sonwhere + " or Language_ids='')";
            }
            models = B_Lebi_ServicePanel_Group.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_ServicePanel_Group.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}