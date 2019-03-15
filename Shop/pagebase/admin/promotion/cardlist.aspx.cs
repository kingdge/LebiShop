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
    public partial class cardtlist : AdminPageBase
    {

        protected List<Lebi_Card> models;
        protected string PageString;
        protected int status = 0;
        protected string key = "";
        protected int type = 0;
        protected int user_id = 0;
        protected SearchCard su;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("cardtype_list", "优惠券列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            su = new SearchCard(CurrentAdmin, CurrentLanguage.Code);
            type = RequestTool.RequestInt("type", 311);
            PageSize = RequestTool.getpageSize(25);
            string where = "1=1";
            key = RequestTool.RequestString("key");
            user_id = RequestTool.RequestInt("user_id");
            if (key != "")
                where += " and Code like lbsql{'%" + key + "%'} or User_UserName like lbsql{'%"+key+"%'}";
            if (user_id > 0)
                where += " and User_id=" + user_id + "";
            where += su.SQL;
            models = B_Lebi_Card.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Card.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key + "&user_id=" + user_id + "&" + su.URL, page, PageSize, recordCount);

        }
        public string TypeName(int id)
        {
            Lebi_CardOrder model = B_Lebi_CardOrder.GetModel(id);
            if (model == null)
                return "";
            return Lang(model.Name);
        }

    }
}