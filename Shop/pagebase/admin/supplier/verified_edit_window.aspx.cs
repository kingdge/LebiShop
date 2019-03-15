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
    public partial class Verified_Edit_window : AdminAjaxBase
    {
        protected Lebi_Supplier_Verified model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (!EX_Admin.Power("supplier_verified", "身份验证"))
            {
                WindowNoPower();
            }
            model = B_Lebi_Supplier_Verified.GetModel(id);
            if (model == null)
                model = new Lebi_Supplier_Verified();
        }
    }
}