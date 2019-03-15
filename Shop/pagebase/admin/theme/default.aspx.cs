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
    public partial class Default : AdminPageBase
    {
        protected List<Lebi_Theme> models;
        protected string PageString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_list", "模板列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            string where = "1=1";
            PageSize = RequestTool.getpageSize(25);
            models = B_Lebi_Theme.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Theme.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
        }
        /// <summary>
        /// 主题的绑定语言
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public string GetLanguage(Lebi_Theme theme)
        {
            string str = "";
            List<Lebi_Language> langs = B_Lebi_Language.GetList("Theme_id=" + theme.id + "", "");
            foreach (Lebi_Language lang in langs)
            {
                if (str == "")
                    str = "<span title=\""+GetSiteName(lang.Site_id)+"\">"+lang.Name +"</span>";
                else
                    str += "&nbsp;<span title=\"" + GetSiteName(lang.Site_id) + "\">" + lang.Name + "</span>";
            }
            return str;
        }
        /// <summary>
        /// 主题的使用站点
        /// </summary>
        /// <param name="Site_id"></param>
        /// <returns></returns>
        public string GetSiteName(int Site_id)
        {
            string str = "";
            Lebi_Site model = B_Lebi_Site.GetModel("id=" + Site_id + "");
            if (model != null)
            {
                str = model.SubName;
            }
            return str;
        }
    }
}