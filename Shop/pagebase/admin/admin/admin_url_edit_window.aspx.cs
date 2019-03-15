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
    public partial class Admin_url_Edit_window : AdminAjaxBase
    {
        protected Lebi_Admin_Power model;
        protected Lebi_Admin_Group group;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_url_edit", "编辑过滤页面"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int gid = RequestTool.RequestInt("gid", 0);
            model = B_Lebi_Admin_Power.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Admin_Power();
            }
            else
            {
                gid = model.Admin_Group_id;
            }
            group = B_Lebi_Admin_Group.GetModel(gid);
        }
    }
}