using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.product
{
    public partial class Property_Edit_window : SupplierAjaxBase
    {
        protected Lebi_ProPerty model;
        protected int tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_property", "属性规格"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int tid = RequestTool.RequestInt("tid", 131);
            model = B_Lebi_ProPerty.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = "+ id);
            if (model == null)
            {
                model = new Lebi_ProPerty();
                model.Type_id_ProPertyType = tid;
            }
        }
        /// <summary>
        /// 返回单选内容
        /// </summary>
        /// <param name="class_"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string TypeRadio(string class_, string name, int id, string ext, string lang)
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_))
            {
                string sel = "";
                if (id == model.id)
                    sel = "checked";
                str += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + Language.Tag(model.Name, lang) + "</label>";
            }
            return str;
        }
        public static string TypeRadio(string class_, string name, int id, string ext)
        {
            string str = "";
            foreach (Lebi_Type model in GetTypes(class_))
            {
                string sel = "";
                if (id == model.id)
                    sel = "checked";
                str += "<label><input type=\"radio\" name=\"" + name + "\" value=\"" + model.id + "\" " + sel + " " + ext + "/>" + model.Name + "</label>";
            }
            return str;
        }
        /// <summary>
        /// 返回一组类型并排除授权之外的类型
        /// </summary>
        /// <param name="class_"></param>
        /// <returns></returns>
        public static List<Lebi_Type> GetTypes(string class_)
        {
            string where = "Class='" + class_ + "' and id in(131,133)";
            List<Lebi_Type> models = B_Lebi_Type.GetList(where, "Sort desc");
            return models;
        }
    }
}