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
    public partial class BillType_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Supplier_BillType model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_billtype_list", "发票管理"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Supplier_BillType.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
            {
                model = new Lebi_Supplier_BillType();
                model.Type_id_BillType = 151;
            }
        }
    }
}