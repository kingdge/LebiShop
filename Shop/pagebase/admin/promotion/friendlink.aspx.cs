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
    public partial class FriendLink : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected int show;
        protected int pic;
        protected List<Lebi_FriendLink> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("friendlink_list", "友情链接列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            show = RequestTool.RequestInt("show",2);
            pic = RequestTool.RequestInt("pic", 2);

            string where = "1=1";
            if (show != 2)
                where += "and IsShow=" + show + "";
            if (pic != 2)
                where += "and IsPic=" + pic + "";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            if (lang != "")
                where += " and Lanaguage like lbsql{'%" + lang + "%'}";
            models = B_Lebi_FriendLink.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_FriendLink.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&lang=" + lang + "&show=" + show + "&key=" + key + "&pic=" + pic, page, PageSize, recordCount);
            
        }
    }
}