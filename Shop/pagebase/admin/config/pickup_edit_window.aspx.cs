using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class pickup_edit_window : AdminAjaxBase
    {
        protected Lebi_PickUp model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("pickup_add", "添加自提点"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("pickup_edit", "编辑自提点"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_PickUp.GetModel(id);
            if (model == null)
            {
                model = new Lebi_PickUp();
                model.IsCanWeekend = 1;
            }
        }
        public string SupplierOptions(int sid)
        {
            string str = "";
            List<Lebi_Supplier> models = B_Lebi_Supplier.GetList("Type_id_SupplierStatus=442", "");
            foreach (Lebi_Supplier model in models)
            {
                string sel = "";
                if (model.id == sid)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.SubName + "</option>";
            }
            return str;
        }
    }
}