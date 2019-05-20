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

namespace Shop.Supplier.Config
{
    public partial class indeximage_edit : SupplierPageBase
    {
        protected Lebi_Page model;
        protected Lebi_Node node;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            int Page_id = RequestTool.RequestInt("id", 0);
            model = B_Lebi_Page.GetModel(Page_id);
            node = NodePage.GetNodeByCode("shopindeximages");
            if (node == null)
            {
                PageReturnMsg = PageErrorMsg();
            }
            if (model == null)
            {
                model = new Lebi_Page();
                model.Supplier_id = CurrentSupplier.id;
            }
        }
        
    }
}