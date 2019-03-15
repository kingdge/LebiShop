using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;
using System.Collections.Specialized;

namespace Shop.Supplier.Statis
{
    public partial class Statis_Default : SupplierPageBase
    {
        protected string serviceinfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_statis", "数据统计"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
        }
    }
}