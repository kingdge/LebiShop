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
    public partial class Profile : SupplierPageBase
    {
        protected Lebi_Supplier model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_profile", "编辑资料"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            int id = RequestTool.RequestInt("id",0);
            model = CurrentSupplier;
            if (model == null)
                model = new Lebi_Supplier();
        }
    }
}