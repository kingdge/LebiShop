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
    public partial class ServicePanel_Group_Edit_window : AdminAjaxBase
    {
        protected Lebi_ServicePanel_Group model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("servicepanel_group_add", "添加客服面板部门"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("servicepanel_group_edit", "编辑客服面板部门"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_ServicePanel_Group.GetModel(id);
            if (model == null)
                model = new Lebi_ServicePanel_Group();
        }
    }
}