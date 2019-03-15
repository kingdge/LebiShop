using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class url_Edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier_Power model;
        protected Lebi_Supplier_Group group;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_group_edit", "编辑商家分组"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int gid = RequestTool.RequestInt("gid", 0);
            model = B_Lebi_Supplier_Power.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier_Power();
            }
            else
            {
                gid = model.Supplier_Group_id;
            }
            group = B_Lebi_Supplier_Group.GetModel(gid);
        }
    }
}