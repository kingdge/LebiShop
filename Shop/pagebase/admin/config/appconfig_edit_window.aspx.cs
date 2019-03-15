using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Net;

namespace Shop.Admin.storeConfig
{
    public partial class appconfig_edit_window : AdminAjaxBase
    {
        protected BaseConfig model;
        protected bool IsEnable;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("appconfig_edit", "APP设置"))
            {
                WindowNoPower();
            }
            B_BaseConfig bconfig=new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
        }
    }
}