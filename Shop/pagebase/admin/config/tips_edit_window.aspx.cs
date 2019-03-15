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
    public partial class Tips_Edit_window : AdminAjaxBase
    {
        protected Lebi_Tips model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("tips_list", "每日箴言"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);
            model = B_Lebi_Tips.GetModel(id);
            if (model == null)
                model = new Lebi_Tips();
        }
    }
}