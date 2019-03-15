using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.User
{
    public partial class Message_Type : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Message_Type> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("message_type_list", "类别列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "Type_id_MessageTypeClass = 350";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            models = B_Lebi_Message_Type.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Message_Type.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);
            
        }
    }
}