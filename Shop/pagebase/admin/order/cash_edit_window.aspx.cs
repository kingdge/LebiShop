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
    public partial class cash_edit_window : AdminAjaxBase
    {
        protected Lebi_Cash model;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("cash_edit", "提现管理"))
            {
                WindowNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Cash.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Cash();
            }


        }
    }
}