using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Collections.Specialized;

namespace Shop.statis
{
    public partial class money : AdminPageBase
    {
        protected int time;
        protected int type;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("statis_money", "资金统计"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            time = RequestTool.RequestInt("time", 0);
            type = RequestTool.RequestInt("type", 0);
        }
    }
}