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

namespace Shop.Admin.agent
{
    public partial class User_Update_Window : AdminAjaxBase
    {
        protected string id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                WindowNoPower();
            }
            id = RequestTool.RequestString("id");
        }
        
    }
}