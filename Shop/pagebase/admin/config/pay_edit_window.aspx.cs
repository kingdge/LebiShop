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
    public partial class Pay_Edit_window : AdminAjaxBase
    {
        protected Lebi_Pay model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("pay_add", "添加付款方式"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("pay_edit", "编辑付款方式"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Pay.GetModel(id);
            if (model == null)
                model = new Lebi_Pay();
        }

        
    }
}