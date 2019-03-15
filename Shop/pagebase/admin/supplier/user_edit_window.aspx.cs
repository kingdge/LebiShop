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
    public partial class Supplier_User_Edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier model;
        protected int Count_pay = 0;
        protected Lebi_User user;
        protected string type = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("supplier_user_add", "添加商家"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("supplier_user_edit", "编辑商家"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Supplier.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier();
                model.Type_id_SupplierStatus = 441;
                user = new Lebi_User();
            }
            else
            {
                user = B_Lebi_User.GetModel(model.User_id);
            }
            Count_pay = B_Lebi_User_Money.Counts("User_id=" + model.User_id + " and Type_id_MoneyType=198");
            type = RequestTool.RequestString("type");
        }
        public string LevelOption(int level_id)
        {
            if (type == "")
                type = "supplier";
            List<Lebi_Supplier_Group> levels = B_Lebi_Supplier_Group.GetList("type=lbsql{'"+type+"'}", "Grade asc");
            string str = "";
            foreach (Lebi_Supplier_Group level in levels)
            {
                string sel = "";
                if (level_id == level.id)
                    sel = "selected";
                str += "<option value=\"" + level.id + "\" " + sel + " days=\"" + level.ServiceDays + "\">" + Shop.Bussiness.Language.Content(level.Name, CurrentLanguage) + "</option>";
            }
            return str;
        }
    }
}