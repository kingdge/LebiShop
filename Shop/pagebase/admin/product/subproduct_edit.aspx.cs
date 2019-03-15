using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class subproduct_Edit : AdminPageBase
    {
        protected List<Lebi_Product> models;
        protected List<Lebi_ProPerty> ggs;
        protected Lebi_Product modelp;
        protected Lebi_Language_Code modelLan;
        protected int id;
        protected int randnum;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = RequestTool.RequestInt("id", 0);
            modelp = B_Lebi_Product.GetModel(id);
            if (modelp == null)
            {
                modelp = new Lebi_Product();
                modelp.ProPertyMain = RequestTool.RequestInt("ProPertyMain", 0);
                modelp.ProPerty131 = RequestTool.RequestString("ProPerty131");
            }
            if (!EX_Admin.Power("product_edit", "编辑商品"))
            {
                WindowNoPower();
            }
            string property = EX_Product.ProductType_ProPertystr(modelp.Pro_Type_id);
            if (property != "")
            {
                try
                {
                    ggs = B_Lebi_ProPerty.GetList("Type_id_ProPertyType=131 and id in (" + property + ")", "Sort desc");
                }
                catch (System.Data.SqlClient.SqlException)
                {
                    ggs = new List<Lebi_ProPerty>();
                }
            }
            if (ggs == null)
            {
                ggs = new List<Lebi_ProPerty>();
            }
            modelLan = Language.DefaultLanguage();
            string where = "";
            where = "Product_id=" + id + " and Product_id!=0 and (IsDel!=1 or IsDel is null)";
            models = B_Lebi_Product.GetList(where, "");
        }
        /// <summary>
        /// 返回规格
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string getproperty(string ids)
        {
            string str = "";
            List<Lebi_ProPerty> list;
            try
            {
                list = B_Lebi_ProPerty.GetList("id in (lbsql{" + ids + "})", "");
            }
            catch (System.Data.SqlClient.SqlException)
            {
                list = new List<Lebi_ProPerty>();
            }
            foreach (Lebi_ProPerty model in list)
            {
                str += Language.Content(model.Name, CurrentLanguage.Code) + ", ";
            }
            return str;
        }
        public string Getpro131List(int pid)
        {
            string str = "";
            List<Lebi_ProPerty> ps = B_Lebi_ProPerty.GetList("parentid=" + pid + "", "Sort desc");
            foreach (Lebi_ProPerty p in ps)
            {
                string sel = "";

                if (("," + modelp.ProPerty131 + ",").Contains("," + p.id + ","))
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"Property131" + p.id + "\" name=\"Property131\" value=\"" + p.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + Language.Content(p.Name, CurrentLanguage.Code) + "</span></label>";
                }
                else
                {
                    str += " <label><input type=\"checkbox\" name=\"Property131\" shop=\"true\" " + sel + " value=\"" + p.id + "\"" + " />" + Language.Content(p.Name, CurrentLanguage.Code) + "</label>&nbsp;";
                }
            }
            return str;
        }
    }
}