using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Supplier_Group_Edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier_Group model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("supplier_group_edit", "添加商家分组"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
                {
                    WindowNoPower();
                }
            }
            string type = RequestTool.RequestString("type");
            if (type == "")
                type = "supplier";
            model = B_Lebi_Supplier_Group.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Group();
                model.type = type;
            }
        }
        /// <summary>
        /// 商家分组的验证内容
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string CheckBox(string ids)
        {
            string str = "";
            List<Lebi_Supplier_Verified> models = B_Lebi_Supplier_Verified.GetList("", "Sort desc");
            foreach (Lebi_Supplier_Verified model in models)
            {
                string sel = "";
                if (("," + ids + ",").Contains("," + model.id + ","))
                    sel = "checked";
                str += "<label><input name=\"Verified_ids\" type=\"checkbox\" shop=\"true\" " + sel + " value=\"" + model.id + "\">" + Lang(model.Name) + "</label>";
            }
            return str;
        }

        public string Skins(string ids)
        {
            string str = "";
            List<Lebi_Supplier_Skin> models = B_Lebi_Supplier_Skin.GetList("IsShow=1", "Sort desc");
            foreach (Lebi_Supplier_Skin model in models)
            {
                string sel = "";
                if (("," + ids + ",").Contains("," + model.id + ","))
                    sel = "checked";
                str += "<label><input name=\"Supplier_Skin_ids\" type=\"checkbox\" shop=\"true\" " + sel + " value=\"" + model.id + "\">" + model.Name + "</label>";
            }
            return str;
        }
    }
}