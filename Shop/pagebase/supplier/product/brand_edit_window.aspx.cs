using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.product
{
    public partial class Brand_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Brand model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (!Power("supplier_brand", "商品品牌"))
            {
                WindowNoPower();
            }
            model = B_Lebi_Brand.GetModel("id = "+ id +" and Supplier_id = "+ CurrentSupplier.id);
            if (model == null)
                model = new Lebi_Brand();

        }
    }
}