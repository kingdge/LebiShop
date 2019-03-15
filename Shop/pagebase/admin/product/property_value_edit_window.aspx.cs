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
    public partial class Property_Value_Edit_window : AdminAjaxBase
    {
        protected Lebi_ProPerty model;
        protected Lebi_ProPerty pmodel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("property_add", "添加属性规格") || !EX_Admin.Power("property_edit", "编辑属性规格"))
            {
                WindowNoPower();
            }
            int pid = RequestTool.RequestInt("pid", 0);
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_ProPerty.GetModel(id);
            pmodel = B_Lebi_ProPerty.GetModel(pid);
            if (pmodel == null)
            {
                pmodel = new Lebi_ProPerty();
                Response.Write("参数错误");
                Response.End();
            }
            if (model == null)
                model = new Lebi_ProPerty();
            
        }
    }
}