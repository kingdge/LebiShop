using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class Admin_Group_Edit_window : AdminAjaxBase
    {
        protected Lebi_Admin_Group model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_group_edit", "编辑权限组"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Admin_Group.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Admin_Group();
            }
        }
    }
}