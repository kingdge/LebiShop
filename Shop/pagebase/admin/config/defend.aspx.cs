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
    public partial class Defend : AdminPageBase
    {
        protected BaseConfig model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("defend_edit", "网站维护"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            B_BaseConfig bconfig=new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
        }
    }
}