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
namespace Shop.Admin.storeConfig
{
    public partial class Language_Edit_window : AdminAjaxBase
    {
        protected Lebi_Language model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("language_add", "添加网站语言"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("language_edit", "编辑网站语言"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Language.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Language();
            }


        }

        public string Themeslist(int id)
        {
            string str = "";
            List<Lebi_Theme> list = B_Lebi_Theme.GetList("", "Sort desc");
            foreach (Lebi_Theme model in list)
            {
                string sel = "";
                if (id == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.Name + "</option>";
            }

            return str;

        }
        public string Currencylist(int id)
        {
            string str = "";
            List<Lebi_Currency> list = B_Lebi_Currency.GetList("", "Sort desc");
            foreach (Lebi_Currency model in list)
            {
                string sel = "";
                if (id == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.Name + "</option>";
            }

            return str;
        }



    }
}