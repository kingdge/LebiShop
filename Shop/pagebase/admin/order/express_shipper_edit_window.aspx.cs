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
    public partial class Express_Shipper_Edit_window : AdminAjaxBase
    {
        protected Lebi_Express_Shipper model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("express_shipper_add", "添加发货人"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("express_shipper_edit", "编辑发货人"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Express_Shipper.GetModel(id);
            if (model == null)
                model = new Lebi_Express_Shipper();

        }
    }
}