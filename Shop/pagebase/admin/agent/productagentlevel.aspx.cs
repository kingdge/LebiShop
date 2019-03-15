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
    public partial class productagentlevel : AdminPageBase
    {
        protected string key;
        protected List<Lebi_Agent_Product_Level> models;
        protected string PageString;
        protected void Page_Load(object sender, EventArgs e)
        {

            PageSize = RequestTool.getpageSize(25);
            key = RequestTool.RequestString("key");
            string where = "1=1";
            //if (key != "")
            //    where += " and User_UserName like '%" + key + "%'";
            models = B_Lebi_Agent_Product_Level.GetList(where, "Sort desc", PageSize, page);
            int recordCount = B_Lebi_Agent_Product_Level.Counts(where);
            PageString = Pager.GetPaginationString("?page={0}&key=" + key, page, PageSize, recordCount);

        }
        public string GetCOrder(int id)
        {
            Lebi_CardOrder order = B_Lebi_CardOrder.GetModel(id);
            if (order == null)
                order = new Lebi_CardOrder();
            return order.IndexCode + "-" + order.Money;
        }
    }
}