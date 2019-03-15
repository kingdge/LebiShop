using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using System.Linq;
using System.Collections.Specialized;
using Shop.Bussiness;
namespace Shop
{

    public class P_UserAgentMoney : ShopPageUser
    {
        protected string key;
        protected List<Lebi_Agent_Money> models;
        protected string PageString;
        protected int status;
        protected string dateFrom;
        protected string dateTo;
        protected decimal money;//可提现金额
        protected decimal zmoney;//总金额
        protected string NextPage;
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (!Shop.LebiAPI.Service.Instanse.Check("plugin_agent"))
            {
                Response.Write(Tag("权限不足"));
                Response.End();
            }
            if (CurrentUser.id == 0)
            {
                Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain())+ ""));
            }
            LoadTheme(themecode, siteid, languagecode, pcode);
            path = "<a href=\"" + URL("P_Index", "") + "\" class=\"home\" title=\"" + Tag("首页") + "\"><span>" + Tag("首页") + "</span></a><em class=\"home\">&raquo;</em><a href=\"" + URL("P_UserCenter", "") + "\"><span>" + Tag("会员中心") + "</span></a><em>&raquo;</em><a href=\"" + URL("P_UserAgent", "") + "\"><span>" + Tag("推广佣金") + "</span></a><em>&raquo;</em><a class=\"text\"><span>" + Tag("佣金查询") + "</span></a>";
            PageSize = RequestTool.getpageSize(25);
            pageindex = RequestTool.RequestInt("page", 1);
            key = RequestTool.RequestString("key");
            status = RequestTool.RequestInt("status", 0);
            dateFrom = Rstring("dateFrom");
            dateTo = Rstring("dateTo");
            DateTime lbsql_dateFrom = RequestTool.RequestDate("dateFrom");
            DateTime lbsql_dateTo = RequestTool.RequestDate("dateTo");
            string where = "User_id=" + CurrentUser.id;
            if (key != "")
                where += " and (Order_Code like lbsql{'%" + key + "%'} or Product_Number like lbsql{'%" + key + "%'})";
            if (status > 0)
                where += " and Type_id_AgentMoneyStatus=" + status;
            if (dateFrom != "" && dateTo != "")
                where += " and Time_add>='" + FormatDate(lbsql_dateFrom) + "' and Time_Add<='" + FormatDate(lbsql_dateTo) + " 23:59:59'";
            models = B_Lebi_Agent_Money.GetList(where, "Time_add desc", PageSize, pageindex);
            int recordCount = B_Lebi_Agent_Money.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&key=" + key + "&status=" + status + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo, pageindex, PageSize, recordCount, CurrentLanguage);
            NextPage = "?page=" + (pageindex + 1) + "&key=" + key + "&status=" + status + "&dateFrom=" + dateFrom + "&dateTo=" + dateTo + "";
            int d = 0;
            int.TryParse(SYS.CommissionMoneyDays,out d);
            d = 0 - d;
            string money_ = Common.GetValue("select sum(Money) from Lebi_Agent_Money where User_id=" + CurrentUser.id + " and Type_id_AgentMoneyStatus=382 and Time_add<'" + System.DateTime.Now.AddDays(d) + "')");
            string zmoney_ = Common.GetValue("select sum(Money) from Lebi_Agent_Money where User_id=" + CurrentUser.id + " and Type_id_AgentMoneyStatus=382");
            decimal.TryParse(money_, out money);
            decimal.TryParse(zmoney_, out zmoney);
        }
        public override string ThemePageMeta(string code, string tag)
        {
            string str = "";
            Lebi_Theme_Page theme_page = B_Lebi_Theme_Page.GetModel("Code='" + code + "'");
            if (theme_page == null)
                return "";
            switch (tag.ToLower())
            {
                case "description":
                    if (Lang(theme_page.SEO_Description) == "")
                        str = Lang(SYS.Description);
                    else
                        str = Lang(theme_page.SEO_Description);
                    break;
                case "keywords":
                    if (Lang(theme_page.SEO_Keywords) == "")
                        str = Lang(SYS.Keywords);
                    else
                        str = Lang(theme_page.SEO_Keywords);
                    break;
                default:
                    if (Lang(theme_page.SEO_Title) == "")
                        str = Tag("佣金查询") + " - " + Tag("推广佣金") + " - " + Tag("会员中心");
                    else
                        str = Lang(theme_page.SEO_Title);
                    break;
            }
            return ThemePageMeta(code, tag, str);
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