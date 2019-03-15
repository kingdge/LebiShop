using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Text;
namespace Shop.Admin.config
{
    public partial class LanguageTag_Export_window : AdminAjaxBase
    {
        protected Lebi_Site model;
        protected List<Lebi_Language_Code> langs;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Site.GetModel(id);
            if (site == null)
            {
                PageError();
                return;
            }
            if (!EX_Admin.Power("language_tag_list", "语言标签列表"))
            {
                WindowNoPower();
            }
            langs = Language.Languages(); //B_Lebi_Language_Code.GetList("", "Code asc");

        }



    }
}