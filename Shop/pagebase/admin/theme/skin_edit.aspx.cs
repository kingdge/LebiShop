using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.theme
{
    public partial class Skin_Edit : AdminPageBase
    {
        protected Lebi_Theme theme;
        protected Lebi_Theme_Skin model;
        protected string SkinContent = "";
        protected Lebi_Theme_Page tpage;
        protected void Page_Load(object sender, EventArgs e)
        {
            int tid = RequestTool.RequestInt("tid", 0);
            int id = RequestTool.RequestInt("id", 0);
            if (RequestTool.GetConfigKey("OnlineFileEdit").Trim() != "1")
            {
                PageReturnMsg = PageNoEditMsg();
            }
            if (id == 0)
            {
                if (!EX_Admin.Power("theme_skin_add", "添加模板页面"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            else
            {
                if (!EX_Admin.Power("theme_skin_edit", "编辑模板页面"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            
            model = B_Lebi_Theme_Skin.GetModel(id);
            if (model != null)
            {
                tid = model.Theme_id;
            }
            else
            {
                model = new Lebi_Theme_Skin();
            }
            theme = B_Lebi_Theme.GetModel(tid);
            if (theme == null)
            {
                theme = new Lebi_Theme();
            }

            SkinContent = GetSkinStr(theme, model);
            tpage = B_Lebi_Theme_Page.GetModel("Code='" + model.Code + "'");
            if (tpage == null)
            {
                tpage = new Lebi_Theme_Page();
                model.IsPage = 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="skin"></param>
        /// <returns></returns>
        private string GetSkinStr(Lebi_Theme theme, Lebi_Theme_Skin skin)
        {
            string str = "";
            string path = theme.Path_Files + "/" + skin.Path_Skin;
            path = ThemeUrl.GetFullPath(path);
            str = HtmlEngine.ReadTxt(path);

            return str;

        }
    }
}