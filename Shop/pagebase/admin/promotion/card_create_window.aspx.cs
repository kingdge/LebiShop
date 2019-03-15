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
    public partial class Card_Create_window : AdminAjaxBase
    {
        protected Lebi_CardOrder model;
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = RequestTool.RequestInt("id",0);
            if (id == 0)
            {
                if (!EX_Admin.Power("cardtype_add", "添加优惠券"))
                {
                    PageNoPower();
                }
            }
            else
            {
                if (!EX_Admin.Power("cardtype_edit", "编辑优惠券"))
                {
                    PageNoPower();
                }
            }
            model = B_Lebi_CardOrder.GetModel(id);
            if (model == null)
                model = new Lebi_CardOrder();
        }
    }
}