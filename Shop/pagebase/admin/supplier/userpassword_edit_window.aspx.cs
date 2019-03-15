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
    public partial class userpassword_edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_user_edit", "编辑商家"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Supplier.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Supplier();
            }
        }
    }
}