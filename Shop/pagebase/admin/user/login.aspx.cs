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
    public partial class Login : AdminAjaxBase
    {
        protected Lebi_User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (!EX_Admin.Power("user_edit", "编辑会员"))
            {
                PageNoPower();
            }
            user = B_Lebi_User.GetModel(id);
            if (user == null)
            {
                PageError();
            }
            int AdminLogin = 1; //管理员登录标识 by Lebi.Kingdge 2017-6-23
            if (EX_User.UserLogin(user.UserName, user.Password, user.DT_id, false, AdminLogin))
            {
                Response.Redirect(Shop.Bussiness.ThemeUrl.GetURL("P_UserCenter", "", "", CurrentLanguage.Code));
            }
        }
    }
}