using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.agent
{
    public partial class agentmoney : AdminPageBase
    {

        protected List<Lebi_Agent_Money> models;
        protected string PageString;
        protected int status;
        protected int type;
        protected int supplier_id;
        protected int user_id;
        protected string dateFrom;
        protected string dateTo;
        protected string key;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("agent_money_list", "佣金查询"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            key = RequestTool.RequestString("key");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            status = RequestTool.RequestInt("status", 0);
            type = RequestTool.RequestInt("type", 0);
            supplier_id = RequestTool.RequestInt("supplier_id", 0);
            user_id = RequestTool.RequestInt("user_id", 0);
            string where = "1=1";
            if (key != "")
                where += " and (User_UserName like lbsql{'%" + key + "%'} or Order_Code like lbsql{'%" + key + "%'})";
            if (status > 0)
                where += " and Type_id_AgentMoneyStatus=" + status;
            if (type > 0)
                where += " and Type_id_AgentMoneyType=" + type;
            if (supplier_id > 0)
                where += " and Supplier_id=" + supplier_id;
            if (user_id > 0)
                where += " and User_id=" + user_id;
            if (dateFrom != "" && dateTo != "")
                where += " and Time_add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            models = B_Lebi_Agent_Money.GetList(where, "Time_add desc", PageSize, page);
            int recordCount = B_Lebi_Agent_Money.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&status=" + status + "&type=" + type + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&key=" + key + "&supplier_id=" + supplier_id + "&user_id=" + user_id, page, PageSize, recordCount);
        }
        public Lebi_Product GetProduct(int id)
        {
            Lebi_Product pro = B_Lebi_Product.GetModel(id);
            if (pro == null)
                pro = new Lebi_Product();
            return pro;
        }


    }
}