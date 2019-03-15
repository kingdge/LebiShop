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
    public partial class Ipblock : AdminPageBase
    {
        protected string ips;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("ipblock_edit", "IP锁定"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            ips = SYS.IPLock;
            ips = ips.Replace(",", "\r\n");
            

        }
    }
}