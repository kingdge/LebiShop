using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Order
{
    public partial class Express_Shipper_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Express_Shipper model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            id = RequestTool.RequestInt("id", 0);
            if (!Power("supplier_express_shipper_list", "发货人管理"))
            {
                WindowNoPower();
            }
            model = B_Lebi_Express_Shipper.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
                model = new Lebi_Express_Shipper();

        }
    }
}