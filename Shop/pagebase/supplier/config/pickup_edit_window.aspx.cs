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
    public partial class pickup_edit_window : SupplierAjaxBase
    {
        protected Lebi_PickUp model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (!Power("pickup_manage", "管理自提点"))
            {
                WindowNoPower();
            }

            model = B_Lebi_PickUp.GetModel(id);
            if (model == null)
            {
                model = new Lebi_PickUp();
                model.IsCanWeekend = 1;
            }
        }
        
    }
}