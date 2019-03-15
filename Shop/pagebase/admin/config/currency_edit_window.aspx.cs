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
    public partial class Currency_Edit_window : AdminAjaxBase
    {
        protected Lebi_Currency model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id", 0);
            if (id == 0)
            {
                if (!EX_Admin.Power("currency_add", "添加币种"))
                {
                    WindowNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("currency_edit", "编辑币种"))
                {
                    WindowNoPower();
                }
            }
            model = B_Lebi_Currency.GetModel(id);
            if (model == null)
            {
                model = new Lebi_Currency();
            }


        }





    }
}