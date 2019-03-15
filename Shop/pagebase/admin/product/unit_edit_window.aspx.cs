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
    public partial class Unit_Edit_window : AdminAjaxBase
    {
        protected Lebi_Units model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("pro_units_add", "添加商品单位"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("pro_units_edit", "编辑商品单位"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Units.GetModel(id);
            if (model == null)
                model = new Lebi_Units();

        }
    }
}