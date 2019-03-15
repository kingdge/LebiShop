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

namespace Shop.Admin.user
{
    public partial class user_search_widow : AdminAjaxBase
    {
        protected SearchUserModel model;
        protected string callback;
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchUser su = new SearchUser(CurrentAdmin, CurrentLanguage.Code);
            model = su.Model;
            callback = RequestTool.RequestString("callback");

        }

    }
}