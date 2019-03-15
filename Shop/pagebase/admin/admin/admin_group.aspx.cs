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
    public partial class Admin_Group : AdminPageBase
    {
        protected List<Lebi_Admin_Group> models;
        protected string PageString;
        protected int PageSize;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("admin_group_list", "权限组列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            models = B_Lebi_Admin_Group.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Admin_Group.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
        }

        
    }
}