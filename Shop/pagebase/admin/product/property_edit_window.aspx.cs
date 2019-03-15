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
    public partial class Property_Edit_window : AdminAjaxBase
    {
        protected Lebi_ProPerty model;
        protected int tid;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("property_add", "添加属性规格"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("property_edit", "编辑属性规格"))
                {
                    WindowNoPower();
                }
            }
            int tid = RequestTool.RequestInt("tid", 131);
            model = B_Lebi_ProPerty.GetModel(id);
            if (model == null)
            {
                model = new Lebi_ProPerty();
                model.Type_id_ProPertyType = tid;
            }



        }
    }
}