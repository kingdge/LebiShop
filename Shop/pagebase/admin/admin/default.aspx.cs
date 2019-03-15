using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.admin
{
    public partial class Default : AdminPageBase
    {
        protected List<Lebi_Administrator> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_list", "系统用户列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            models = B_Lebi_Administrator.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Administrator.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
        }

        public string Admingroup(int id)
        {
            string str = "";
            Lebi_Admin_Group model = new Lebi_Admin_Group();
            model = B_Lebi_Admin_Group.GetModel(id);
            if (model != null)
                str = Language.Content(model.Name,CurrentLanguage.Code);
            return str;
        }
    }
}