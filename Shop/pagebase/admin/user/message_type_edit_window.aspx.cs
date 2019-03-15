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
    public partial class Message_Type_Edit_window : AdminAjaxBase
    {
        protected Lebi_Message_Type model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("message_type_edit", "添加类别"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("message_type_edit", "编辑类别"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Message_Type.GetModel("id = " + id + " and Type_id_MessageTypeClass = 350");
            if (model == null)
                model = new Lebi_Message_Type();
        }
    }
}