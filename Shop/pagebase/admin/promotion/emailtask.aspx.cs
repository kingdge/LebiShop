using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.promotion
{
    public partial class EmailTask : AdminPageBase
    {
        protected List<Lebi_EmailTask> models;
        protected string PageString;
        protected int t;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("emailtask_edit", "群发邮件"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            t = RequestTool.RequestInt("t", 2);

            string where = "1=1";
            if (t != 2)
                where += " and IsSubmit=" + t;
            models = B_Lebi_EmailTask.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_EmailTask.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&t=" + t, page, PageSize, recordCount);
        }

        public int Count(int ptid)
        {
            return B_Lebi_Promotion.Counts("Promotion_Type_id=" + ptid + "");
        }

        public string userlevel(string ids)
        {
            if (ids == "")
                return "";
            string str = "";
            List<Lebi_UserLevel> ls = B_Lebi_UserLevel.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_UserLevel l in ls)
            {
                if (str == "")
                    str = Lang(l.Name);
                else
                    str += ","+Lang(l.Name);
            }
            return str;
        }
    }
}