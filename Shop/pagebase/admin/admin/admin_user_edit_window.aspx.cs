using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class Administrator_Edit_window : AdminAjaxBase
    {
        protected Lebi_Administrator model;
        protected List<Lebi_Admin_Group> groups;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_edit", "编辑系统用户"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Administrator.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Administrator();
                model.Type_id_AdminStatus = 230;
            }

            groups = B_Lebi_Admin_Group.GetList("","Sort desc");
            
        }
        /// <summary>
        ///  多站点选择
        /// </summary>
        /// <param name="InputName"></param>
        /// <param name="ids"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string Pro_TypeCheckbox(string InputName, string ids, string lang)
        {
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("Parentid = 0", "Sort desc");
            string str = "";
            foreach (Lebi_Pro_Type model in models)
            {
                string sel = "";
                if (("," + ids + ",").IndexOf("," + model.id + ",") > -1)
                    sel = "checked";
                str += "<label><input type=\"checkbox\" name=\"" + InputName + "\" id=\"" + InputName + "\" shop=\"true\" value=\"" + model.id + "\" " + sel + ">" + Lang(model.Name) + "&nbsp;</label>";
            }
            return str;

        }
    }
}