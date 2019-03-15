using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class product_add_window : AdminAjaxBase
    {
        protected SearchProduct sp;
        protected int ProductCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            sp = new SearchProduct(CurrentAdmin, CurrentLanguage.Code);
            ProductCount = B_Lebi_Product.Counts("Product_id=0 " + sp.SQL);
        }
    }
}