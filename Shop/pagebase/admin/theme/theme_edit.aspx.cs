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
    public partial class Theme_Edit : AdminPageBase
    {
        protected Lebi_Theme model;
        protected bool IsOwner;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("theme_add", "添加模板"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            else
            {
                if (!EX_Admin.Power("theme_edit", "编辑模板"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
            }
            
            model = B_Lebi_Theme.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme();
                model.LebiUser = SYS.LicenseUserName;
                IsOwner = true;
                model.ImageBig_Width = 800;
                model.ImageBig_Height = 800;
                model.ImageMedium_Width = 320;
                model.ImageMedium_Height = 320;
                model.ImageSmall_Width = 200;
                model.ImageSmall_Height = 200;
            }
            else
            {
                if (model.LebiUser == SYS.LicenseUserName || SYS.LicenseUserName == "jia5255@163.com" || SYS.LicenseUserName == "ljq@lebi.cn" || SYS.LicenseUserName == "demo@lebi.cn")
                    IsOwner = true;
                else
                    IsOwner = false;
            }
        }

        public string GetName()
        {
            if (IsOwner)
                return "<input type=\"text\" id=\"Name\" name=\"Name\" class=\"input\" shop=\"true\" style=\"width: 500px;\" value=\"" + model.Name + "\" />";
            return model.Name;
        }
        public string GetLanguage()
        {
            if (IsOwner)
                return Shop.Bussiness.Language.LanguageCheckbox("Language", model.Language);
            return model.Language;
        }
        /// <summary>
        /// 主题的绑定语言
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public string GetLanguageid(Lebi_Theme theme)
        {
            string str = "";
            List<Lebi_Language> langs = B_Lebi_Language.GetList("Theme_id=" + theme.id + "", "");
            foreach (Lebi_Language lang in langs)
            {
                if (str == "")
                    str = ""+lang.id;
                else
                    str += "," + lang.id;
            }
            return str;
        }
        public string GetUpLoadDiv()
        {
            string str = "";
            //if (IsOwner)
            //{
                str += "<input type=\"hidden\" shop=\"true\" id=\"ImageUrl\" name=\"ImageUrl\" class=\"input\" style=\"width: 200px;\" value=\"" + model.ImageUrl + "\" /> ";
                str += "<input id=\"file_ImageUrl\" name=\"file_ImageUrl\" class=\"input\" type=\"file\" onchange=\"uploadImage('ImageUrl')\" />";
                //str += "<input type=\"button\" value=\"" + Tag("上传") + "\" class=\"button\" onclick=\"uploadImage('ImageUrl')\" />";
            //}
            return str;
        }
        public string publish()
        {
            if (IsOwner)
                return "<li class=\"submit\"><a href=\"javascript:void(0);\" onclick=\"Publish();\"><b></b><span>" + Tag("发布") + "</span></a></li>";
            return "";
        }
    }
}