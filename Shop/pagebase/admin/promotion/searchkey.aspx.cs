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
    public partial class SearchKey : AdminPageBase
    {
        protected string lang;
        protected int type;
        protected string key;
        protected List<Lebi_Searchkey> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("searchkey_list", "关键词列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            type = RequestTool.RequestInt("type", 0);
            if (lang == "")
                lang = "CN";
            string where = "1=1";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            if(type>0)
                where += " and Type ="+type+"";
            models = B_Lebi_Searchkey.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Searchkey.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&lang=" + lang + "&key=" + key + "&type=" + type, page, PageSize, recordCount);
            
        }
    }
}