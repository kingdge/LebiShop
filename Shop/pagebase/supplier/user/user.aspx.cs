using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;
using Shop.Tools;

namespace Shop.Supplier.user
{
    public partial class user : SupplierPageBase
    {
        protected string key;
        protected List<Lebi_Supplier_User> models;
        protected string PageString;
        protected int status;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Power("supplier_user_list", "用户列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            status = RequestTool.RequestInt("status", 0);
            string where = "Supplier_id=" + CurrentSupplier.id;
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            if (status > 0)
                where += " and Type_id_SupplierUserStatus=" + status;
            models = B_Lebi_Supplier_User.GetList(where, "", PageSize, page);
            int recordCount = B_Lebi_Supplier_User.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationString("?page={0}&status=" + status + "&key=" + key, page, PageSize, recordCount);

        }

        public string GetGroupName(int id)
        {
            Lebi_Supplier_UserGroup model = B_Lebi_Supplier_UserGroup.GetModel("Supplier_id=" + CurrentSupplier.id +" and id = "+ id);
            if (model == null)
                return Tag("未分组");
            return model.Name;
        }
    }
}