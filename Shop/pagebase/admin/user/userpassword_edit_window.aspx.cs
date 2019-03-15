using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.user
{
    public partial class userpassword_edit_window : AdminAjaxBase
    {
        protected Lebi_User model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("user_edit", "编辑会员"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_User.GetModel(id);
            if (model == null)
            {
                model = new Lebi_User();
            }
        }
    }
}