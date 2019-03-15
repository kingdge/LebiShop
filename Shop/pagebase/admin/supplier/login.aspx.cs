using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Login : AdminAjaxBase
    {
        protected Lebi_User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (!EX_Admin.Power("supplier_user_login", "商家后台维护"))
            {
                PageNoPower();
            }
            Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(id);
            if (supplier == null)
            {
                PageError();
            }
            user = B_Lebi_User.GetModel(supplier.User_id);
            if (user == null)
            {
                PageError();
            }
            int AdminLogin = 1; //管理员登录标识 by Lebi.Kingdge 2017-6-23
            if (EX_User.UserLogin(user.UserName, user.Password, user.DT_id, false, AdminLogin))
            {
                string msg = "";
                EX_Supplier.Login(user,"", supplier.id, out msg, 1);
                Response.Redirect(site.SupplierPath);
            }
        }
    }
}