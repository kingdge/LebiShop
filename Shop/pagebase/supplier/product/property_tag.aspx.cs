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
    public partial class ProPerty_Tag : SupplierPageBase
    {
        protected string lang;
        protected string key;
        protected List<Lebi_ProPerty_Tag> models;
        protected List<Lebi_Type> types;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_property", "属性规格"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            string where = "Supplier_id = " + CurrentSupplier.id + "";
            if (key != "")
                where += " and Name like lbsql{'%"+key+"%'}";
            models = B_Lebi_ProPerty_Tag.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_ProPerty_Tag.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            types = B_Lebi_Type.GetList("Class='ProPertyType' and id in(131,133)", "Sort desc");
        }
    }
}