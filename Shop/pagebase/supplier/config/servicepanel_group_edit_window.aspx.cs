using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class ServicePanel_Group_Edit_window : SupplierAjaxBase
    {
        protected Lebi_ServicePanel_Group model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (!Power("supplier_servicepanel_list", "客服面板"))
            {
                WindowNoPower();
            }
            model = B_Lebi_ServicePanel_Group.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
                model = new Lebi_ServicePanel_Group();
        }
    }
}