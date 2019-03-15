using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
namespace Shop.admin.plugin.taobao
{
    public partial class _default : AdminPageBase
    {

        protected string taobaobutton;
        protected string shopnick;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("tongbu_taobao", "淘宝同步"))
            {
                PageNoPower();
            }


            shopnick = B_BaseConfig.Get("platform_taobao_shopnick");

            dateFrom = System.DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss");
            dateTo = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

    }
}