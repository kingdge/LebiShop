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
namespace Shop.Admin.theme
{
    public partial class page_create_window : AdminAjaxBase
    {
        protected Lebi_Theme_Page model;
        protected bool showlanguage = true;
        protected string Select;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Theme_Page.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme_Page();
                model.Type_id_PublishType = 120;
            }
            if (model.Code == "P_ArticleDetails")
            {
                showlanguage = false;
                Select = Article() + TimeStr();
            }
            else if (model.Code == "P_Product")
            {
                //商品页面
                showlanguage = true;
                Select = ProductType() + TimeStr();
            }
            else if (model.Code == "P_NewsDetails")
            {
                //商城动态
                Select = TimeStr();
            }
        }
        public string GetLanguage()
        {
            //List<Lebi_Language> models = Shop.Bussiness.Language.AllLanguages();
            //string str = "";
            //foreach (Lebi_Language model in models)
            //{
            //    str += "<input type=\"checkbox\" name=\"Language\" id=\"Language\" shop=\"true\" value=\"" + model.id + "\" checked>" + model.Name + "&nbsp;";
            //}
            string str = Language.SiteLanguageCheckbox("Language", "", CurrentLanguage.Code);
            return str;
        }
        /// <summary>
        /// 时间处理
        /// </summary>
        /// <returns></returns>
        public string TimeStr()
        {
            string str = "<tr><th>" + Tag("时间") + "：</th><td>";
            str += "<input name=\"time1\" id=\"time1\" style=\"width:150px\" shop=\"true\" value=\"" + System.DateTime.Now.AddDays(-7) + "\"> - ";
            str += "<input name=\"time2\" id=\"time2\" style=\"width:150px\" shop=\"true\" value=\"" + System.DateTime.Now + "\">";
            str += "</td></tr>";
            return str;
        }
        /// <summary>
        /// 文章分类
        /// </summary>
        /// <returns></returns>
        public string Article()
        {
            Lebi_Node node = NodePage.GetNodeByCode("Info");
            List<Lebi_Node> nodes = B_Lebi_Node.GetList("parentid=" + node.id + "", "");
            string str = "<tr><th>" + Tag("类别") + "：</th><td>";
            foreach (Lebi_Node n in nodes)
            {
                str += "<input type=\"checkbox\" name=\"node\" value=\"" + n.id + "\" shop=\"true\" >" + n.Name + "</br>";
            }
            str += "</td></tr>";
            return str;
        }
        //商品分类选择
        public string ProductType()
        {
            string str = "<tr><th>" + Tag("类别") + "：</th><td>";
            str += "<select name=\"Pro_Type_id\" id=\"Pro_Type_id\" shop=\"true\" multiple=\"multiple\" class=\"mutiselect\" size=\"8\">";
            str += Shop.Bussiness.EX_Product.TypeOption(0, "", 0, CurrentLanguage.Code);
            str += "</select>";
            str += "</td></tr>";
            return str;
        }
    }
}