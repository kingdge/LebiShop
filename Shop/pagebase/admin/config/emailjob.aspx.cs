using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.Config
{
    public partial class EmailJob : AdminPageBase
    {

        protected List<Lebi_Email> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("emailtask_edit", "群发邮件"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            models = B_Lebi_Email.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Email.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}", page, PageSize, recordCount);
            
        }
    }
}