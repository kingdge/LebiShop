using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class ServicePanel_Edit_window : SupplierAjaxBase
    {
        protected Lebi_ServicePanel model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                WindowNoPower();
            }
            model = B_Lebi_ServicePanel.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
                model = new Lebi_ServicePanel();
        }
        public string GetOptionServicePanel_Type(int ID)
        {
            List<Lebi_ServicePanel_Type> models = B_Lebi_ServicePanel_Type.GetList("", "Sort desc");
            string str = "";
            foreach (Lebi_ServicePanel_Type model in models)
            {
                string sel = "";
                if (ID == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.Name + "</option>";
            }
            return str;
        }
        public string GetOptionServicePanel_Group(int ID)
        {
            List<Lebi_ServicePanel_Group> models = B_Lebi_ServicePanel_Group.GetList("Supplier_id = " + CurrentSupplier.id + "", "Sort desc");
            string str = "";
            foreach (Lebi_ServicePanel_Group model in models)
            {
                string sel = "";
                if (ID == model.id)
                    sel = "selected";
                str += "<option value=\"" + model.id + "\" " + sel + ">" + model.Name + "</option>";
            }
            return str;
        }
    }
}