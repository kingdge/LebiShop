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

namespace Shop.Admin.user
{
    public partial class UserLevel_Edit : AdminPageBase
    {
        protected Lebi_UserLevel model;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("userlevel_add", "添加会员分组"))
                {
                    PageNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("userlevel_edit", "编辑会员分组"))
                {
                    PageNoPower();
                }
            }
            model = B_Lebi_UserLevel.GetModel(id);
            if (model == null)
            {
                model = new Lebi_UserLevel();
            }
            
        }
        
    }
}