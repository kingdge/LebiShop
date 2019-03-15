using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

namespace Shop.Admin.agent
{
    public partial class config : AdminPageBase
    {
        protected BaseConfig model;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            B_BaseConfig bconfig = new B_BaseConfig();
            //model = bconfig.LoadConfig();
            model = ShopCache.GetBaseConfig();
        }
        

    }
}