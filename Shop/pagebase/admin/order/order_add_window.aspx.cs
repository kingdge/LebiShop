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
namespace Shop.Admin.order
{
    public partial class order_add_window : AdminAjaxBase
    {
        protected Lebi_User user;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int id = RequestTool.RequestInt("userid", 0);
            user = B_Lebi_User.GetModel(id);
            if (user == null)
            {
                user = new Lebi_User();
            }


        }
    }
}