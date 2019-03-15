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
    public partial class Property_Value : SupplierPageBase
    {

        protected List<Lebi_ProPerty> models;
        protected Lebi_ProPerty pmodel;
        protected Lebi_Type tmodel;
        protected string PageString;
        protected int pid;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_property", "属性规格"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            Lebi_ProPerty check = B_Lebi_ProPerty.GetModel(pid);
            if (check == null)
            {
                pid = B_Lebi_ProPerty.GetList("Supplier_id = " + CurrentSupplier.id + " and parentid = 0", "").FirstOrDefault().id;
            }
            PageSize = RequestTool.getpageSize(25);
            pid = RequestTool.RequestInt("pid",0);
            key = RequestTool.RequestString("key");
            pmodel = B_Lebi_ProPerty.GetModel(pid);
            string where = "parentid=" + pid;
            if (key != "")
                where += " and (Name like lbsql{'%" + key + "%'} or Code like lbsql{'%" + key + "%'})";
            models = B_Lebi_ProPerty.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_ProPerty.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&pid=" + pid + "&key=" + key + "", page, PageSize, recordCount);
            tmodel = B_Lebi_Type.GetModel(pmodel.Type_id_ProPertyType);

        }

      
    }
}