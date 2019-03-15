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
    public partial class ServicePanel_Edit_window : AdminAjaxBase
    {
        protected Lebi_ServicePanel model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("servicepanel_add", "添加客服面板成员"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("servicepanel_edit", "编辑客服面板成员"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_ServicePanel.GetModel(id);
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
            List<Lebi_ServicePanel_Group> models = B_Lebi_ServicePanel_Group.GetList("Supplier_id=0", "Sort desc");
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