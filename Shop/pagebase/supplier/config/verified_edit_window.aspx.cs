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
    public partial class Verified_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Supplier_Verified Verified;
        protected Lebi_Supplier_Verified_Log model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            int vid = RequestTool.RequestInt("vid", 0);
            if (!Power("supplier_verified", "身份验证"))
            {
                WindowNoPower();
            }
            Verified = B_Lebi_Supplier_Verified.GetModel(id);
            if (Verified == null)
                Verified = new Lebi_Supplier_Verified();
            model = B_Lebi_Supplier_Verified_Log.GetModel("Supplier_id = " + CurrentSupplier.id + " and id =" + vid + "");
            if (model == null)
                model = new Lebi_Supplier_Verified_Log();
        }
    }
}