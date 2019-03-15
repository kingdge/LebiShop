using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class paoductagentuser_edit_window : AdminAjaxBase
    {
        protected Lebi_Agent_Product_User model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            //if (id == 0)
            //{
            //    if (!EX_Admin.Power("supplier_group_edit", "添加商家分组"))
            //    {
            //        WindowNoPower();
            //    }
            //}
            //else
            //{
            //    if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            //    {
            //        WindowNoPower();
            //    }
            //}
            model = B_Lebi_Agent_Product_User.GetModel(id);
            if (model == null)
                model = new Lebi_Agent_Product_User();
        }


        public string LevelOption(int selid)
        {
            List<Lebi_Agent_Product_Level> models = B_Lebi_Agent_Product_Level.GetList("", "Sort desc");
            string str = "";
            foreach (Lebi_Agent_Product_Level model in models)
            {
                string sel = "";
                if (selid == model.id)
                    sel = "selected";
                str += "<option " + sel + " value=" + model.id + ">" + model.Name + "</option>";
            }
            return str;
        }
    }
}