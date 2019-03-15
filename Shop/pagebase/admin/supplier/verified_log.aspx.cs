using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Admin.Supplier
{
    public partial class Verified_Log : AdminPageBase
    {
        protected List<Lebi_Supplier_Verified_Log> models;
        protected Lebi_Supplier user;
        protected string PageString;
        protected int user_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("supplier_verified", "身份验证"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            user_id = RequestTool.RequestInt("user_id", 0);
            string where = "1=1";
            if (user_id > 0)
                where += " and Supplier_id=" + user_id;
            models = B_Lebi_Supplier_Verified_Log.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Supplier_Verified_Log.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&user_id=" + user_id, page, PageSize, recordCount);
            user = B_Lebi_Supplier.GetModel("id = " + user_id);
            if (user == null)
                user = new Lebi_Supplier();
        }

        public string VerifiedName(int id)
        {
            Lebi_Supplier_Verified model = B_Lebi_Supplier_Verified.GetModel(id);
            if (model == null)
                return "";
            return Lang(model.Name);
        }
        public Lebi_Supplier GetSupplier(int id)
        {
            Lebi_Supplier model = B_Lebi_Supplier.GetModel(id);
            if (model == null)
                return new Lebi_Supplier();
            return model;
        }
    }
}