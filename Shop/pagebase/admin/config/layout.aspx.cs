using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

namespace Shop.Admin.storeConfig
{
    public partial class Layout : AdminPageBase
    {
        protected BaseConfig model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("baseconfig_edit", "基本设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            model = ShopCache.GetBaseConfig();
        }
    }
}