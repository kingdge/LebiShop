using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
namespace Shop.admin.product
{
    public partial class datainout : AdminPageBase
    {


        protected SearchProduct sp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("prodesc_datainout", "导入导出"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            sp = new SearchProduct(CurrentAdmin, CurrentLanguage.Code);

            
        }

    }
}