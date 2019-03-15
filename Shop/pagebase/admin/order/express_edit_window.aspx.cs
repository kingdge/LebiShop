using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.order
{
    public partial class Express_Edit_window : AdminAjaxBase
    {
        protected Lebi_Express model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("express_add", "添加模板"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("express_edit", "编辑模板"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Express.GetModel(id);
            if (model == null)
                model = new Lebi_Express();

        }
        public string GetOptionTransport(int ID)
        {
            List<Lebi_Transport> models = B_Lebi_Transport.GetList("", "");
            string str = "<option value=\"\">请选择</option>";
            foreach (Lebi_Transport model in models)
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