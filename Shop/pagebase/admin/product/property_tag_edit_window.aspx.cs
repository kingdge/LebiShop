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
    public partial class ProPerty_Tag_Edit_window : AdminAjaxBase
    {
        protected Lebi_ProPerty_Tag model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (!EX_Admin.Power("property_list", "属性规格列表"))
            {
                WindowNoPower();
            }
            model = B_Lebi_ProPerty_Tag.GetModel(id);
            if (model == null)
            {
                model = new Lebi_ProPerty_Tag();
                model.Type_id_ProPertyType = 131;
            }
        }
    }
}