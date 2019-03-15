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
    public partial class Delivery_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Supplier_Delivery model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_delivery_list", "配送点管理"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);
            model = B_Lebi_Supplier_Delivery.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
                model = new Lebi_Supplier_Delivery();
        }
    }
}