using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;

namespace Shop.inc
{
    public partial class productcategory : Bussiness.ShopPage
    {
        protected int cid;
        public void LoadPage()
        {
            cid = Rint("cid");
        }
    }
}