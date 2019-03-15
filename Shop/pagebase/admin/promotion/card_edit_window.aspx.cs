using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.promotion
{
    public partial class card_edit_window : AdminAjaxBase
    {

        protected List<Lebi_Card> models;
        protected string card_ids;
        protected SearchCard su;
        protected int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("card_edit", "修改卡券"))
            {
                WindowNoPower();
            }
            int user_id = RequestTool.RequestInt("user_id", 0);
            card_ids = RequestTool.RequestString("ids");
            su = new SearchCard(CurrentAdmin, CurrentLanguage.Code);
            if (card_ids != "")
            {
                models = B_Lebi_Card.GetList("id in (lbsql{" + card_ids + "})", "");
                count = models.Count;
            }
            else
            {
                count = B_Lebi_Card.Counts("1=1" + su.SQL);
            }


        }

    }
}