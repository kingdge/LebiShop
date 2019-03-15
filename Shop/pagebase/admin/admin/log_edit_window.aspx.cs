using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class Log_Edit_window : AdminAjaxBase
    {
        protected Lebi_Log model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("log_view", "查看操作日志"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Log.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Log();
            }
        }
    }
}