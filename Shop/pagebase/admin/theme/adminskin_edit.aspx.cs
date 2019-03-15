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
    public partial class adminskin_edit : AdminPageBase
    {
        protected Lebi_AdminSkin model;
        protected string SkinContent = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (RequestTool.GetConfigKey("OnlineFileEdit").Trim() != "1")
            {
                PageReturnMsg = PageNoEditMsg();
            }
            int tid = RequestTool.RequestInt("tid", 0);
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("adminskin_add", "添加模板"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            else
            {
                if (!EX_Admin.Power("adminskin_edit", "编辑模板"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            model = B_Lebi_AdminSkin.GetModel(id);
            if (model == null)
            {
                model = new Lebi_AdminSkin();
            }
            
            SkinContent = GetSkinStr(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="skin"></param>
        /// <returns></returns>
        private string GetSkinStr(Lebi_AdminSkin skin)
        {
            string str = "";
            string path = "";
            if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
            {
                path = site.AdminPath + "/custom/skin2/" + skin.Code + ".html";
            }
            else
            {
                path = site.AdminPath + "/custom/skin/" + skin.Code + ".html";
            }
            path = ThemeUrl.GetFullPath(path);
            str = HtmlEngine.ReadTxt(path);

            return str;

        }
    }
}