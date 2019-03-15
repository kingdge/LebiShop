using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Reflection;
using System.Globalization;
using System.Collections.Specialized;
using Shop.Model;
using Shop.Tools;
using Shop.DataAccess;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Linq;
using Shop.Bussiness;
namespace Shop.Bussiness
{
    public class SupplierBase : ShopPage
    {
        protected Lebi_Supplier CurrentSupplier;
        protected Lebi_Supplier_Group CurrentSupplierGroup;
        protected Lebi_Supplier_UserGroup CurrentSupplierUserGroup;
        protected Lebi_Supplier_User CurrentSupplierUser;
        protected Site_Supplier site;
        protected new Lebi_Language_Code CurrentLanguage;
        protected int page = RequestTool.RequestInt("page", 1);
        protected Lebi_Currency DefaultCurrency;
        protected List<Lebi_Language_Code> langs;
        protected List<Lebi_Supplier> Suppliers;//返回当前账号的可管理超级用户
        protected string PageReturnMsg;
        public SupplierBase()
        {

            site = new Site_Supplier();
            CurrentLanguage = Language.CurrentLanguage();

            DefaultCurrency = B_Lebi_Currency.GetModel("IsDefault=1");
            if (DefaultCurrency == null)
                DefaultCurrency = B_Lebi_Currency.GetList("", "Sort desc").FirstOrDefault();

            CurrentCurrency = DefaultCurrency;
            site = new Site_Supplier();
         
            langs = Language.Languages();
            reqPage = RequestTool.GetRequestUrl().ToLower();

        }
        public void PageNoPower()
        {
            if (RequestTool.GetConfigKey("DemoSite").Trim() != "1")
            {
                try
                {
                    Response.Redirect(site.AdminPath + "/nopower.aspx");
                }
                catch (System.NullReferenceException)
                {
                    Response.Write(Tag("权限不足"));
                    Response.End();
                }
            }
        }
        public void NewPageNoPower()
        {
            PageNoPower();
        }
        public void WindowNoPower()
        {
            PageNoPower();
        }
        public void AjaxNoPower()
        {
            Response.Write("{\"msg\":\"" + Tag("权限不足") + "\"}");
            Response.End();
        }
        public string PageErrorMsg()
        {
            return Tag("参数错误");
        }
        public string PageNoPowerMsg()
        {
            if (RequestTool.GetConfigKey("DemoSite").Trim() == "1")
            {
                return "";
            }
            return Tag("权限不足");
        }
        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Power(string code, string name)
        {
            if (RequestTool.GetConfigKey("DemoSite").Trim() == "1")
            {
                return false;
            }
            if (CurrentSupplier.User_id == CurrentUser.id)
                return true;
            return EX_Supplier.Power(code, name, CurrentSupplierUserGroup.Limit_Codes);
        }
        /// <summary>
        /// 将金额格式化为指定货币
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public string FormatMoney(decimal money, string CurrencyCode)
        {
            return Language.FormatMoney(money, CurrencyCode);
        }
        /// <summary>
        /// 返回当前账号的可管理超级用户
        /// </summary>
        /// <returns></returns>
        public List<Lebi_Supplier> GetSuppliers()
        {
            List<Lebi_Supplier> models = B_Lebi_Supplier.GetList("id in (select Supplier_id from [Lebi_Supplier_User] where User_id=" + CurrentSupplierUser.User_id + " and Type_id_SupplierUserStatus=9011) and id!=" + CurrentSupplier.id + "", "");
            return models;
        }
        /// <summary>
        /// 语言名称
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns></returns>
        public string LanguageName(string langids)
        {
            if (langids == "")
                return "";
            string res = "";
            List<Lebi_Site> sites = Shop.Bussiness.Site.Instance.Sites();
            foreach (Lebi_Site site in sites)
            {
                List<Lebi_Language> langs = B_Lebi_Language.GetList("Site_id=" + site.id + " and id in (" + langids + ")", "");
                if (langs.Count > 0)
                {
                    if (sites.Count > 1)
                    {
                        if (res == "")
                            res = site.SubName + ":";
                        else
                            res += "|" + site.SubName + ":";
                    }
                    foreach (Lebi_Language lang in langs)
                    {
                        res += lang.Code + " ";
                    }
                }
            }
            return res;
        }
        public string Lang(string jsonstr, string lang)
        {
            return Bussiness.Language.Content(jsonstr, lang);
        }
    }


}
