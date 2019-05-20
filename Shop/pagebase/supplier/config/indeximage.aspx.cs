using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.Config
{
    public partial class indeximage : SupplierPageBase
    {
        protected Lebi_Node node;
        protected List<Lebi_Page> pages;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            PageSize = RequestTool.getpageSize(25);

            node = NodePage.GetNodeByCode("shopindeximages");
            if (node == null)
            {
                PageReturnMsg = PageErrorMsg();
            }

            string where = "Node_id=" + node.id + " and Supplier_id=" + CurrentSupplier.id;
            int recordCount = B_Lebi_Page.Counts(where);
            pages = B_Lebi_Page.GetList(where, "Sort desc,id desc", PageSize, page);
            PageString =Shop.Bussiness.Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
        }
    }
}