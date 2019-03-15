using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Text;
namespace Shop.Admin.Config
{
    public partial class Inquiry_Edit_window : AdminAjaxBase
    {
        protected Lebi_Inquiry model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("inquiry_list", "留言反馈"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Inquiry.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Inquiry();
            }
        }
    }
}