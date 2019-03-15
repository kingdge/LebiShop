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
    public partial class Tab_Edit_window : AdminAjaxBase
    {
        protected Lebi_Tab model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);
            model = B_Lebi_Tab.GetModel(id);
            if (model == null)
                model = new Lebi_Tab();
        }
    }
}