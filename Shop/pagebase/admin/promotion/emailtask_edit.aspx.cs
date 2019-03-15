using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.promotion
{
    public partial class EmailTask_Edit : AdminPageBase
    {
        protected Lebi_EmailTask model;
        protected int id;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!EX_Admin.Power("emailtask_edit", "群发邮件"))
            {
                PageNoPower();
            }
            id = RequestTool.RequestInt("id",0);
            model = B_Lebi_EmailTask.GetModel(id);
            if (model == null)
                model = new Lebi_EmailTask();

        }

    }
}