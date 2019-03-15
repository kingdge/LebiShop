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
    public partial class Page_Edit_window : AdminAjaxBase
    {
        protected Lebi_Theme_Page model;
        protected string para;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("themepage_edit", "编辑页面"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);

            model = B_Lebi_Theme_Page.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Theme_Page();
                model.Type_id_PublishType = 120;
            }
            para = Getpara(model.Code);
        }
        public string TypeRadio(string class_, string name, int id, string ext, string lang)
        {
            string where = "Class='" + class_ + "'";
            if (model.IsAllowHtml != 1)
            {
                where += " and id!=122";
            }
            if (model.IsCustom != 1)
            {
                where += " and id!=123";
            }
            List<Lebi_Type> models = B_Lebi_Type.GetList(where, "Sort desc");
            string str = "";
            foreach (Lebi_Type t in models)
            {
                string sel = "";
                if (id == t.id)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-radio m-r-20\"><input type=\"radio\" id=\"" + name + "" + t.id + "\" name=\"" + name + "\" value=\"" + t.id + "\" class=\"custom-control-input\" " + sel + " " + ext + "><span class=\"custom-control-label\">" + Language.Tag(t.Name, lang) + "</span></label>";
                }
                else
                {
                    str += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + t.id + "\" " + sel + " " + ext + "/>" + Language.Tag(t.Name, lang) + "</label>";
                }
            }
            return str;

        }

        public string Getpara(string code)
        {
            string res = "";
            switch (code)
            {
                case "P_About":
                    res = "id={0}";
                    break;
                case "P_Article":
                    res = "id={0}&page={1}";
                    break;
                case "P_ArticleDetails":
                    res = "id={0}";
                    break;
                case "P_Brand":
                    res = "id={0}&cid={1}&list={2}&sort={3}&page={4}";
                    break;
                case "P_Cashier":
                    res = "order_id={0}";
                    break;
                case "P_Help":
                    res = "id={0}&type={1}";
                    break;
                case "P_Login":
                    res = "url={0}";
                    break;
                case "P_News":
                    res = "id={0}&page={1}";
                    break;
                case "P_NewsDetails":
                    res = "id={0}";
                    break;
                case "P_Pay":
                    res = "order_id={0}&order_code={1}";
                    break;
                case "P_Product":
                    res = "id={0}";
                    break;
                case "P_ProductCategory":
                    res = "id={0}&pid={1}&cid={2}&list={3}&sort={4}&tid={5}&page={6}";
                    break;
                case "P_Register":
                    res = "url={0}";
                    break;
                case "P_Search":
                    res = "keyword={0}&list={1}&sort={2}&page={3}";
                    break;
                case "P_Tab":
                    res = "id={0}";
                    break;
                case "P_UserAddressEdit":
                    res = "id={0}";
                    break;
                case "P_UserComment":
                    res = "type={0}";
                    break;
                case "P_UserCommentWrite":
                    res = "id={0}";
                    break;
                case "P_UserOrderDetails":
                    res = "id={0}";
                    break;
                case "P_UserReturnApply":
                    res = "id={0}";
                    break;
                case "P_UserReturnDetails":
                    res = "id={0}";
                    break;
                case "P_UserReturnShip":
                    res = "id={0}&tid={1}";
                    break;
                case "P_UserLike":
                    res = "key={0}&dateFrom={1}&dateTo={2}&page={3}";
                    break;
                case "P_UserMessage":
                    res = "type_id={0}";
                    break;

            }
            return Tag("默认")+ "："+res + "<br>";
        }
    }
}