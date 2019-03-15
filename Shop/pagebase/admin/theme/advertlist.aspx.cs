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
    public partial class AdvertList : AdminPageBase
    {
        protected List<Lebi_Advert> imgs;
        protected Lebi_Advert img;
        protected Lebi_Theme theme;
        protected Lebi_Theme_Advert adv;
        protected int id;
        protected int tid;
        protected string key;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_advert_list", "广告位页面列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            id = RequestTool.RequestInt("id", 0);
            key = RequestTool.RequestString("key");
            tid = RequestTool.RequestInt("tid", 0);
            adv = B_Lebi_Theme_Advert.GetModel(id);
            if (adv == null)
            {
                adv = new Lebi_Theme_Advert();
            }
            theme = B_Lebi_Theme.GetModel(tid);
            if (theme == null)
            {
                theme = new Lebi_Theme();
            }
            string where = "Theme_Advert_id=" + id;
            if (key != "")
                where += " and (Title like lbsql{'%" + key + "%'} or [URL] like lbsql{'%" + key + "%'})";
            imgs = B_Lebi_Advert.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Advert.Counts(where);
            PageString = Pager.GetPaginationString("?id=" + id + "&tid=" + tid + "&key=" + key +"&page={0}", page, PageSize, recordCount);
        }
    }
}