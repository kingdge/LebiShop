using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.user
{
    public partial class user_edit_window : SupplierAjaxBase
    {
        protected Lebi_Supplier_User model;
        protected Lebi_User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Supplier_User.GetModel(id);
            if (model==null)
            {
                if (!Power("supplier_user_add", "添加用户"))
                {
                    AjaxNoPower();
                }
                model = new Lebi_Supplier_User();
            }
            else
            {
                if (!Power("supplier_user_edit", "编辑用户"))
                {
                    AjaxNoPower();
                }
            }
            user = B_Lebi_User.GetModel(model.User_id);
            if (user == null)
                user = new Lebi_User();
        }
        
    }
}