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
    public partial class Type : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Type> models;
        protected string PageString;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("type_list", "类型管理"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(50);
            key = RequestTool.RequestString("key");
            string where = "Class like '%Status%' and Class not like '%Upload%'";
            if (key != "")
                where += " and (Name like lbsql{'%" + key + "%'} or Class like lbsql{'%" + key + "%'})";
            models = B_Lebi_Type.GetList(where, "Class asc,Sort desc", PageSize, page);
            int recordCount = B_Lebi_Type.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&&key=" + key, page, PageSize, recordCount);
            
        }
    }
}