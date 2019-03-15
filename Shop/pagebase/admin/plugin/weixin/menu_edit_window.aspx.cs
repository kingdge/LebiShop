using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.plugin.weinxin
{
    public partial class menu_edit_window : AdminAjaxBase
    {
        protected Lebi_weixin_menu model;
        protected Lebi_weixin_menu pmodel;
        protected int IsLogin = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);

            int pid = RequestTool.RequestInt("pid", 0);
            model = B_Lebi_weixin_menu.GetModel(id);
            if (model == null)
                model = new Lebi_weixin_menu();
            else
                pid = model.parentid;
            pmodel = B_Lebi_weixin_menu.GetModel(pid);
            if (pmodel == null)
            {
                pmodel = new Lebi_weixin_menu();
            }
            if (model.url.Contains("weixinlogin=1"))
                IsLogin = 1;
        }

    }
}