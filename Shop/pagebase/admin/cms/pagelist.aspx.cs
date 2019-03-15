using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Shop.Bussiness;
using Shop.Model;using DB.LebiShop;
using LB.Tools;

namespace Shop.Admin.cms
{
    public partial class PageList : AdminPageBase
    {
        protected string lang;
        protected string key;
        protected int Node_id = RequestTool.RequestInt("Node_id", 0);
        protected Lebi_Node node;
        protected Lebi_Node pnode;
        protected List<Lebi_Page> pages;
        protected string PageString;
        protected string dateFrom;
        protected string dateTo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!EX_Admin.Power("page_add", "添加结点内容"))
            {
                PageReturnMsg = PageNoPowerMsg();
            }
            PageSize = RequestTool.getpageSize(25);
            node = B_Lebi_Node.GetModel(Node_id);
            if (node == null)
            {
                string NodeCode = RequestTool.RequestString("code");
                node = B_Lebi_Node.GetModel("Code=lbsql{'" + NodeCode + "'}");
                if (node == null)
                {
                    PageReturnMsg = PageErrorMsg();
                }
            }
            
            pnode = B_Lebi_Node.GetModel(node.parentid);
            if (pnode == null)
                pnode = new Lebi_Node();
            lang = RequestTool.RequestString("lang");
            key = RequestTool.RequestString("key");
            dateFrom = RequestTool.RequestString("dateFrom");
            dateTo = RequestTool.RequestString("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "Node_id=" + node.id + "";
            if (key != "")
                where += " and Name like lbsql{'%" + key + "%'}";
            if (dateFrom != "" && dateTo != "")
                where += " and Time_Add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            if (lang != "")
                where += " and Language like lbsql{'%" + lang + "%'}";
            if (site.SiteCount > 1 && CurrentAdmin.Site_ids != "")
            {
                string sonwhere = "";
                List<Lebi_Language> ls = B_Lebi_Language.GetList("Site_id in (" + CurrentAdmin.Site_ids + ")", "");
                foreach (Lebi_Language l in ls)
                {
                    
                        if (sonwhere == "")
                            sonwhere = "','+Language_ids+',' like '%," + l.id + ",%'";
                        else
                            sonwhere += " or ','+Language_ids+',' like '%," + l.id + ",%'";
                    
                }
                if (sonwhere != "")
                {
                    where += " and (" + sonwhere + " or Language_ids='')";
                }
            }
            int recordCount = B_Lebi_Page.Counts(where);
            pages = B_Lebi_Page.GetList(where, "Sort desc,id desc", PageSize, page);
            PageString = Pager.GetPaginationString("?page={0}&Node_id=" + node.id + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "&lang=" + lang + "&key=" + key, page, PageSize, recordCount);
        }
    }
}