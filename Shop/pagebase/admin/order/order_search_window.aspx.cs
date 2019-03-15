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

namespace Shop.Admin.order
{
    public partial class order_search_widow : AdminAjaxBase
    {
        protected SearchOrderModel model;
        protected string callback;
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchOrder su = new SearchOrder(CurrentAdmin, CurrentLanguage.Code);
            model = su.Model;
            callback = RequestTool.RequestString("callback");

        }

    }
}