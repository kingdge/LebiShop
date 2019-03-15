using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Message_Write_window : AdminAjaxBase
    {
        protected Lebi_Tab model;
        protected List<Lebi_Supplier> user;
        protected string User_Name;
        protected string ids;
        protected SearchUser su;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_message_write", "发送站内信"))
            {
                WindowNoPower();
            }
            User_Name = RequestTool.RequestString("User_Name");
            ids = RequestTool.RequestString("ids");
            if (ids != "")
            {
                user = B_Lebi_Supplier.GetList("id in (lbsql{" + ids + "})", "id desc");
                if (user != null)
                {

                }
            }
            su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
        }
    }
}