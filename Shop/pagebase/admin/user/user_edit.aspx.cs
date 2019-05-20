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
    public partial class User_Edit : AdminPageBase
    {
        protected Lebi_User model;
        protected List<Lebi_UserLevel> leaves;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_User.GetModel(id);
            if (model == null)
            {
                if (!EX_Admin.Power("user_add", "添加会员"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                
                model = new Lebi_User();
                model.Sex = "男";
                if (SYS.IsOpenUserEnd == "1")
                {
                    int days = 365;
                    try
                    {
                        int.TryParse(SYS.DefaultUserEndDays, out days);
                    }
                    catch
                    {
 
                    }
                    model.Time_End = DateTime.Now.AddDays(days);
                }
            }
            else
            {
                if (!EX_Admin.Power("user_edit", "编辑会员"))
                {
                    PageReturnMsg = PageNoPowerMsg();
                }
                
                if (domain3admin && CurrentAdmin.Site_ids != "")
                {
                    if (!("," + CurrentAdmin.Site_ids + ",").Contains("," + model.Site_id + ","))
                    {
                        PageReturnMsg = PageNoPowerMsg();
                    }
                }
            }

            leaves = B_Lebi_UserLevel.GetList("1=1", "Grade asc");

        }

    }
}