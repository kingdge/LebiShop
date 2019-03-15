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
    public partial class FriendLink_Edit_window : AdminAjaxBase
    {
        protected Lebi_FriendLink model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("friendlink_add", "添加友情链接"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("friendlink_edit", "编辑友情链接"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_FriendLink.GetModel(id);
            if (model == null)
                model = new Lebi_FriendLink();
        }
    }
}