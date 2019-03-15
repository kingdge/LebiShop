using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;

using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

namespace Shop.Admin.product
{
    public partial class product_batch_price : AdminPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("product_batch_price", "批量调价"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
        }
    }
}