using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;
namespace Shop.supplier.product
{
    public partial class datainout : SupplierPageBase
    {
        protected SearchProduct sp;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_product_datainout", "导入导出"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            sp = new SearchProduct(CurrentSupplier, CurrentLanguage.Code);
        }

    }
}