using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.user
{
    public partial class Supplier_Group_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Supplier_UserGroup model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (!Power("supplier_group_edit", "编辑用户分组"))
            {
                AjaxNoPower();
            }
            model = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
                model = new Lebi_Supplier_UserGroup();
        }
        
    }
}