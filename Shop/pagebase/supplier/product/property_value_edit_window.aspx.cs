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
    public partial class Property_Value_Edit_window : SupplierAjaxBase
    {
        protected Lebi_ProPerty model;
        protected Lebi_ProPerty pmodel;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_property", "属性规格"))
            {
                WindowNoPower();
            }
            int pid = RequestTool.RequestInt("pid", 0);
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_ProPerty.GetModel(id);
            Lebi_ProPerty check = B_Lebi_ProPerty.GetModel(pid);
            if (check == null)
            {
                pid = B_Lebi_ProPerty.GetList("Supplier_id = " + CurrentSupplier.id + " and parentid = 0", "").FirstOrDefault().id;
            }
            pmodel = B_Lebi_ProPerty.GetModel(pid);
            if (pmodel == null)
            {
                pmodel = new Lebi_ProPerty();
                Response.Write("参数错误");
                Response.End();
            }
            if (model == null)
                model = new Lebi_ProPerty();
            
        }
    }
}