using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.promotion
{
    public partial class card_search : AdminAjaxBase
    {
        protected SearchCardModel model;
        protected string callback;
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchCard su = new SearchCard(CurrentAdmin, CurrentLanguage.Code);
            model = su.Model;
            callback = RequestTool.RequestString("callback");

        }

    }
}