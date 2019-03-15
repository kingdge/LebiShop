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
    public partial class advertment : AdminPageBase
    {
        protected List<Lebi_Theme> themes;
        protected int id;
        protected string key;
        protected string PageString;
        protected List<Lebi_Theme_Advert> models;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_advert_list", "广告位页面列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            id = RequestTool.RequestInt("id", 0);
            //int tid = RequestTool.RequestInt("tid", 0);
            string themeid = "0";
            themes = B_Lebi_Theme.GetList("id in (select Theme_id from [lebi_Language])", "");
            foreach (Lebi_Theme theme in themes)
            {
                themeid += "," + theme.id;
            }
            key = RequestTool.RequestString("key");
            string where = "Theme_id in (" + themeid + ")";
            if (key != "")
                where += " and (Code like lbsql{'%" + key + "%'} or [Description] like lbsql{'%" + key + "%'})";
            models = B_Lebi_Theme_Advert.GetList(where, "Theme_id desc,Sort desc", PageSize, page);
            int recordCount = B_Lebi_Theme_Advert.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
        }
        public List<Lebi_Theme_Advert> GetAdvert(int themeid)
        {
            List<Lebi_Theme_Advert> models;
            key = RequestTool.RequestString("key");
            string where = "Theme_id=" + themeid + "";
            if (key != "")
                where += " and (Code like lbsql{'%" + key + "%'} or [Description] like lbsql{'%" + key + "%'})";
            models = B_Lebi_Theme_Advert.GetList(where, "Sort desc", PageSize, page);
            return models;
        }
        public List<Lebi_Advert> GetImages(int id)
        {
            List<Lebi_Advert> models = B_Lebi_Advert.GetList("Theme_Advert_id=" + id + "", "Sort desc");
            return models;
        }
        /// <summary>
        /// 主题的名称
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public string GetThemeName(int id)
        {
            string str = "";
            Lebi_Theme model = B_Lebi_Theme.GetModel("id=" + id + "");
            if (model != null)
            {
                str = model.Name;
            }
            return str;
        }
    }
}