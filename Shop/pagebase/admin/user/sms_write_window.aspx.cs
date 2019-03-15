using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.User
{
    public partial class SMS_Write_window : AdminAjaxBase
    {
        protected Lebi_Tab model;
        protected List<Lebi_User> user;
        protected string User_Name;
        protected string ids;
        protected SearchUser su;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("sms_write", "发送手机短信"))
            {
                WindowNoPower();
            }
            User_Name = RequestTool.RequestString("User_Name");
            ids = RequestTool.RequestString("ids");
            if (ids != "")
            {
                user = B_Lebi_User.GetList("id in (lbsql{" + ids + "})", "id desc");
                if (user != null)
                {

                }
            }
            su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
        }
    }
}