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
    public partial class Skin : AdminPageBase
    {
        protected List<Lebi_Theme_Skin> models;
        protected string PageString;
        protected Lebi_Theme theme;
        protected int ispage;
        protected int id;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("theme_skin_list", "模板页面列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            id = RequestTool.RequestInt("id", 0);
            key = RequestTool.RequestString("key");
            //int tid = RequestTool.RequestInt("tid", 0);
            theme = B_Lebi_Theme.GetModel(id);
            if (theme == null)
            {
                Response.Write("参数错误");
                Response.End();
                theme = new Lebi_Theme();
            }
            CheckCode(theme);
            string where = "IsPage=1 and Theme_id=" + id;
            if (key != "")
                where += " and (Code like lbsql{'%" + key + "%'} or Name like lbsql{'%" + key + "%'} or PageName like lbsql{'%" + key + "%'})";
            ispage = RequestTool.RequestInt("ispage", 2);
            //if (ispage == 0 || ispage == 1)
            //{
            //    where += " and IsPage=" + ispage;
            //}
            PageSize = RequestTool.getpageSize(25);
            models = B_Lebi_Theme_Skin.GetList(where, "Code asc,Sort desc", PageSize, page);
            int recordCount = B_Lebi_Theme_Skin.Counts(where);
            PageString = Pager.GetPaginationString("?id=" + theme.id + "&ispage=" + ispage + "&page={0}", page, PageSize, recordCount);
        }

        public Lebi_Node GetNode(string code)
        {
            return NodePage.GetNodeByCode(code);
        }
        /// <summary>
        /// 检查模板的页面与结点是否相符
        /// </summary>
        /// <param name="tid"></param>
        private void CheckCode(Lebi_Theme theme)
        {
            //Lebi_Theme theme = B_Lebi_Theme.GetModel(tid);
            List<Lebi_Theme_Page> nodes = B_Lebi_Theme_Page.GetList("", "");
            foreach (Lebi_Theme_Page node in nodes)
            {
                if (node.Code == "")
                    continue;
                Lebi_Theme_Skin skin = B_Lebi_Theme_Skin.GetList("Theme_id=" + theme.id + " and Code='" + node.Code + "' and IsPage=1", "").FirstOrDefault();
                if (skin == null)
                {
                    skin = new Lebi_Theme_Skin();
                    skin.Code = node.Code;
                    skin.IsPage = 1;
                    skin.Name = Language.Content(node.Name, "CN");
                    skin.PageName = node.PageName;
                    skin.PageParameter = node.PageParameter;
                    skin.Path_Skin = node.PageName.Replace("aspx", "html");
                    //skin.Path_Create = node.pat
                    skin.Sort = 0;
                    skin.StaticPageName = node.StaticPageName;
                    skin.Theme_id = theme.id;
                    B_Lebi_Theme_Skin.Add(skin);
                }
            }
        }
    }
}