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
    public partial class UserLevel_Edit_Window : AdminAjaxBase
    {
        protected Lebi_Agent_UserLevel model;
        protected int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("agent_config", "代理-参数设置"))
            {
                WindowNoPower();
            }
            id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Agent_UserLevel.GetModel("UserLevel_id = "+ id +"");
            if (model == null)
            {
                model = new Lebi_Agent_UserLevel();
                model.Angent1_Commission = -1;
                model.Angent2_Commission = -1;
                model.Angent3_Commission = -1;
            }
            
        }
        
    }
}