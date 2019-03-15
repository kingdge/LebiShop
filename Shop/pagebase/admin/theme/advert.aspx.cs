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
    public partial class Advert : AdminPageBase
    {
        protected List<Lebi_Theme_Advert> models;
        protected string PageString;
        protected Lebi_Theme theme;
        protected int id;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_advert_list", "广告位页面列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            id = RequestTool.RequestInt("id", 0);
            key = RequestTool.RequestString("key");
            //int tid = RequestTool.RequestInt("tid", 0);
            theme = B_Lebi_Theme.GetModel(id);
            if (theme == null)
            {
                PageReturnMsg = PageErrorMsg();
                
                theme = new Lebi_Theme();
            }
            PageSize = RequestTool.getpageSize(25);
            string where = "Theme_id=" + id;
            if (key != "")
                where += " and (Code like lbsql{'%" + key + "%'} or [Description] like lbsql{'%" + key + "%'})";
            models = B_Lebi_Theme_Advert.GetList(where, "Sort desc,Code asc", PageSize, page);
            int recordCount = B_Lebi_Theme_Advert.Counts(where);
            PageString = Pager.GetPaginationString("?id=" + id + "&key=" + key +"&page={0}", page, PageSize, recordCount);
        }


        public List<Lebi_Advert> GetImages(int id)
        {
            List < Lebi_Advert> models= B_Lebi_Advert.GetList("Theme_Advert_id="+id+"", "Sort desc");
            return models;
        }

    }
}