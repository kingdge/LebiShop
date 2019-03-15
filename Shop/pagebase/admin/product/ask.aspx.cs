using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.product
{
    public partial class Ask : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected int status;
        protected List<Lebi_Comment> models;
        protected string PageString;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("ask_list", "商品咨询列表"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            
            PageSize = RequestTool.getpageSize(25);
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            status = RequestTool.RequestInt("status",0);
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "Parentid = 0 and TableName = 'Product_Ask'";
            if (key != "")
                where += " and Content like lbsql{'%" + key + "%'}";
            if (status > 0)
                where += " and Status like lbsql{'%" + status + "%'}";
            if (lang != "")
                where += " and Language_Code = lbsql{'" + lang + "'}";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            models = B_Lebi_Comment.GetList(where, "id desc", PageSize, page);
            int recordCount = B_Lebi_Comment.Counts(where);

            PageString = Pager.GetPaginationString("?page={0}&lang=" + lang + "&status=" + status + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&&key=" + key, page, PageSize, recordCount);
            
        }
        public Lebi_Product GetProduct(int id)
        {
            Lebi_Product model = B_Lebi_Product.GetModel(id);
            if (model == null)
                model = new Lebi_Product();
            return model;
        }
    }
}