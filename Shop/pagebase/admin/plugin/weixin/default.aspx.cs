using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;

using LB.Tools;
namespace Shop.admin.plugin.weinxin
{
    public partial class _default : AdminPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("weixin", "微信管理"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
        }
        public List<Lebi_weixin_menu> GetwxMenus(int pid)
        {
            List<Lebi_weixin_menu> models = B_Lebi_weixin_menu.GetList("parentid=" + pid + "", "Sort desc,id desc");
            return models;
        }

    }
}