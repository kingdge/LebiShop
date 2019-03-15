using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.storeConfig
{
    public partial class Transport_kuaidi100_window : AdminAjaxBase
    {
        protected BaseConfig model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("transport_kuaidi100_edit", "快递100配置"))
            {
                WindowNoPower();
            }
            B_BaseConfig bconfig = new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
        }
    }
}