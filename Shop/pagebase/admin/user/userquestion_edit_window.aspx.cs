using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.user
{
    public partial class userquestion_edit_window : AdminAjaxBase
    {
        protected List<Lebi_User_Question> user_questions;
        protected List<Lebi_User_Answer> user_answers;
        protected string where;
        protected int recordCount;
        protected int id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("user_edit", "编辑会员"))
            {
                WindowNoPower();
            }
            id = RequestTool.RequestInt("id", 0);
            user_questions = B_Lebi_User_Question.GetList("", "Sort desc");
            where = "User_id=" + id + "";
            user_answers = B_Lebi_User_Answer.GetList(where, "id asc", 20, 1);
            recordCount = B_Lebi_User_Answer.Counts(where);
        }
        public string QuestionName(int id)
        {
            Lebi_User_Question model = B_Lebi_User_Question.GetModel(id);
            if (model != null)
                return Lang(model.Name);
            return "";
        }
    }
}