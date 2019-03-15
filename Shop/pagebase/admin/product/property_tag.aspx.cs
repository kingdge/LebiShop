using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class ProPerty_Tag : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected List<Lebi_ProPerty_Tag> models;
        protected List<Lebi_Type> types;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("property_list", "属性规格列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            string where = "1=1";
            if (key != "")
                where += " and Name like lbsql{'%"+key+"%'}";
            models = B_Lebi_ProPerty_Tag.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_ProPerty_Tag.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            types = B_Lebi_Type.GetList("Class='ProPertyType'", "Sort desc");
        }
    }
}