using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.config
{
    public partial class EmailJob_Edit_window : AdminAjaxBase
    {
        protected Lebi_Email model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("emailtask_edit", "群发邮件"))
            {
                PageNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Email.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Email();
            }
        }
    }
}