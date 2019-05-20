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
    public partial class Brand : SupplierPageBase
    {
        protected string lang;
        protected string key;
        protected string type;
        protected List<Lebi_Brand> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_brand", "商品品牌"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            type = RequestTool.RequestString("type");
            string where = "Supplier_id = "+ CurrentSupplier.id;
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            if (type !="")
                where += " and Pro_Type_id like lbsql{'%" + type + "%'}";
            models = B_Lebi_Brand.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Brand.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&type=" + type + "&key=" + key, page, PageSize, recordCount);
        }
    }
}