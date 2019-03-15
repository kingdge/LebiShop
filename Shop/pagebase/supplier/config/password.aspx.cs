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
    public partial class Password : SupplierPageBase
    {
        protected Lebi_Supplier model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_password", "编辑密码"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
        }
    }
}