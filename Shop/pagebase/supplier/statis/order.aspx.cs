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
    public partial class Statis_Order : SupplierPageBase
    {
        protected int time;
        protected int type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_statis", "数据统计"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            time = RequestTool.RequestInt("time", 0);
            type = RequestTool.RequestInt("type", 0);
        }
    }
}