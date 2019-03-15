using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using Shop.Model;
using Shop.Tools;
using System.Linq;
using System.Collections.Specialized;

namespace Shop.Bussiness
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
        protected override void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            if (!Shop.Bussiness.B_API.Check("plugin_agent"))
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
            PageSize = RequestTool.getpageSize(20);
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
                where += " and (datediff(d,Time_add,'" + lbsql_dateFrom + "')<=0 and datediff(d,Time_add,'" + lbsql_dateTo + "')>=0)";
            models = B_Lebi_Agent_Money.GetList(where, "", PageSize, pageindex);
            int recordCount = B_Lebi_Agent_Money.Counts(where);
            PageString = Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}&key=" + key + "&status=" + status + " & dateFrom = " + dateFrom + " & dateTo = " + dateTo, pageindex, PageSize, recordCount, CurrentLanguage);
            string money_ = Common.GetValue("select sum(Money) from Lebi_Agent_Money where User_id=" + CurrentUser.id + " and Type_id_AgentMoneyStatus=382 and datediff(d,Time_add,'" + System.DateTime.Now + "')>" + SYS.CommissionMoneyDays + "");
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