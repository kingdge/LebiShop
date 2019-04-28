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

namespace Shop.Admin
{
    public partial class Default : AdminPageBase
    {
        protected string serviceinfo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            desk = 1;
            if (!EX_Admin.Power("admin_data", "系统桌面"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            if (!EX_Admin.Power("product_price_cost", "管理成本价"))
            {
            }
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                serviceinfo = "<div class=\"float_notice\"><iframe src=\"http://www.lebi.cn/support/notice/index.html\" name=\"news\" width=\"100%\" marginwidth=\"0\" marginheight=\"0\" hspace=\"0\" vspace=\"0\" frameborder=\"0\" scrolling=\"no\"></iframe></div>";
            }
        }
    }
}