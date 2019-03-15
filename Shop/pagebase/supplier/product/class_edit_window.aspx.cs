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
    public partial class Class_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Supplier_ProductType model;
        protected Lebi_Supplier_ProductType pmodel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_pro_type", "商品分类"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            int pid = RequestTool.RequestInt("pid", 0);
            model = B_Lebi_Supplier_ProductType.GetModel("Supplier_id = " + CurrentSupplier.id + " and id = " + id);
            if (model == null)
                model = new Lebi_Supplier_ProductType();
            else
                pid = model.parentid;
            pmodel = B_Lebi_Supplier_ProductType.GetModel(pid);
            if (pmodel == null)
            {
                pmodel = new Lebi_Supplier_ProductType();
            }
        }
        
    }
}