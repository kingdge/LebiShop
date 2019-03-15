using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.Config
{
    public partial class Question_Edit_window : AdminAjaxBase
    {
        protected Lebi_User_Question model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (!EX_Admin.Power("question_list", "安全问题"))
            {
                PageNoPower();
            }
            model = B_Lebi_User_Question.GetModel(id);
            if (model == null)
                model = new Lebi_User_Question();

        }
    }
}