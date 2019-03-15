using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.supplier.product
{
    public partial class product_search_window : SupplierAjaxBase
    {
        protected SearchProductModel model;
        protected string callback;
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchProduct su = new SearchProduct(CurrentSupplier, CurrentLanguage.Code);
            model = su.Model;
            callback = RequestTool.RequestString("callback");

        }

    }
}