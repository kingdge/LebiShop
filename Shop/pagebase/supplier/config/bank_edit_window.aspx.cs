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
    public partial class Bank_Edit_window : SupplierAjaxBase
    {
        protected Lebi_Supplier_Bank model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_bank_list", "付款账号"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id",0);
            model = B_Lebi_Supplier_Bank.GetModel("id = " + id + " and Supplier_id = " + CurrentSupplier.id + "");
            if (model == null)
                model = new Lebi_Supplier_Bank();
        }
    }
}