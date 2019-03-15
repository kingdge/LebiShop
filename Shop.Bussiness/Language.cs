using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace Shop.Bussiness
{
    public class Language
    {
        /// <summary>
        /// 处理文字标签
        /// </summary>
        /// <param name="str"></param>
        /// <param name="langFlag"></param>
        /// <returns></returns>
        public static string Tag(string str, string langFlag)
        {
            List<Lebi_Language_Tag> models = ShopCache.GetLanguageTag();
            Lebi_Language_Tag tag = (from m in models
                                     where m.tag == str
                                     select m).ToList().FirstOrDefault();
            if (tag == null)
                tag = B_Lebi_Language_Tag.GetModel("tag='" + str + "'");
            if (tag == null)
            {
                tag = new Lebi_Language_Tag();
                tag.tag = str;
                tag.CN = str;
                //tag.JP = str;
                tag.EN = str;
                B_Lebi_Language_Tag.Add(tag);
            }
            else
            {
                try
                {
                    Type t = tag.GetType();
                    PropertyInfo pi_count = t.GetProperty(langFlag);
                    str = pi_count.GetValue(tag, null).ToString();
                }
                catch
                {
                }
            }
            return str;
        }
        public static string Tag(string str, Lebi_Language_Code lang)
        {
            return Tag(str, lang.Code);
        }
        public static string Tag(string str)
        {
            Lebi_Language_Code lang = CurrentLanguage();
            return Tag(str, lang.Code);
        }
        /// <summary>
        /// 返回序列化的文字
        /// </summary>
        /// <returns></returns>
        public static string GetTag(string text)
        {
            string str = "[";
            List<Lebi_Language_Code> models = Languages();
            foreach (Lebi_Language_Code model in models)
            {
                str += "{\"L\":\"" + model.Code + "\",\"C\":\"" + Tag(text, model.Code) + "\"},";
            }
            str = str.Remove(str.Length - 1);
            str += "]";
            return str;
        }
        /// <summary>
        /// 后台使用的语言
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Language_Code> AdminLanguages()
        {
            string langs = ShopCache.GetBaseConfig().AdminLanguages;
            langs = langs == "" ? "CN" : langs;
            langs = langs.Replace(",", "','");
            langs = "'" + langs + "'";
            List<Lebi_Language_Code> tmodels = B_Lebi_Language_Code.GetList("Code in (" + langs + ")", "Code asc");
            return tmodels;
        }
        /// <summary>
        /// 后台使用的语言代码
        /// </summary>
        /// <returns></returns>
        public static string LanguageCodes()
        {
            string str = "";
            List<Lebi_Language_Code> tmodels = B_Lebi_Language_Code.GetList("Code in (select Code from Lebi_Language)", "Code asc");
            foreach (Lebi_Language_Code model in tmodels)
            {
                str += model.Code +",";
            }
            str = str.Substring(0,str.Length - 1);
            return str;
        }
        /// <summary>
        /// 返回用户的语种-不重复
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Language_Code> Languages()
        {
            List<Lebi_Language_Code> tmodels = B_Lebi_Language_Code.GetList("Code in (select Code from Lebi_Language)", "Code asc");
            return tmodels;
        }
        /// <summary>
        /// 返回用户的语种
        /// </summary>
        /// <returns></returns>
        public static List<Lebi_Language> AllLanguages()
        {
            List<Lebi_Language> tmodels = B_Lebi_Language.GetList("", "Sort desc");
            return tmodels;
        }
        /// <summary>
        /// 返回站点语言
        /// </summary>
        /// <param name="siteid"></param>
        /// <returns></returns>
        public static List<Lebi_Language> SiteLanguages(int siteid)
        {
            List<Lebi_Language> tmodels = B_Lebi_Language.GetList("Site_id=" + siteid + "", "Sort desc");
            return tmodels;
        }
        /// <summary>
        /// 语言下拉框选项
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static string LanguageOption(string Code)
        {
            string str = "";
            List<Lebi_Language_Code> models = Languages();
            foreach (Lebi_Language_Code model in models)
            {
                string sel = "";
                if (Code == model.Code)
                    sel = "selected";
                str += "<option value=\"" + model.Code + "\" " + sel + ">" + model.Name + "</option>";
            }

            return str;

        }
        public static string SiteLanguageOption(string Code, string LanuageCode)
        {
            Site sitem = new Site();
            List<Lebi_Site> sites = B_Lebi_Site.GetList("", "Sort desc", sitem.SiteCount, 1);
            string str = "";
            foreach (Lebi_Site site in sites)
            {
                List<Lebi_Language> models = B_Lebi_Language.GetList("Site_id=" + site.id + "", "");
                foreach (Lebi_Language model in models)
                {
                    string sel = "";
                    if (Code == model.Code)
                        sel = "selected";
                    string oname = model.Name;
                    if (sitem.SiteCount > 1)
                        oname = model.Name + "[" + Content(site.Name, LanuageCode) + "]";
                    str += "<option value=\"" + model.id + "\" " + sel + ">" + oname + "</option>";
                }
            }
            return str;

        }
        /// <summary>
        /// 语言Checkbox框选项
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static string LanguageCheckbox(string InputName, string Code)
        {
            List<Lebi_Language_Code> models = Languages();
            string str = "";
            foreach (Lebi_Language_Code model in models)
            {
                string sel = "";
                if (Code.IndexOf(model.Code) > -1)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + InputName + "" + model.id + "\" name=\"" + InputName + "\" value=\"" + model.Code + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + model.Name + "</span></label>";
                }
                else
                {
                    str += "<input type=\"checkbox\" name=\"" + InputName + "\" id=\"" + InputName + "" + model.id + "\" shop=\"true\" value=\"" + model.Code + "\" " + sel + "><label for=\"" + InputName + "" + model.id + "\">" + model.Name + "</label>&nbsp;";
                }
            }
            return str;

        }
        public static string LanguageCheckbox(string InputName, string Code, Lebi_Language_Code lang)
        {
            List<Lebi_Language_Code> models = Languages();
            string str = "";
            foreach (Lebi_Language_Code model in models)
            {
                string sel = "";
                if (Code.IndexOf(model.Code) > -1)
                    sel = "checked";
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + InputName + "" + model.id + "\" name=\"" + InputName + "\" value=\"" + model.Code + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + Language.Tag(model.Name, lang.Code) + "</span></label>";
                }
                else
                {
                    str += "<input type=\"checkbox\" name=\"" + InputName + "\" id=\"" + InputName + "" + model.id + "\" shop=\"true\" value=\"" + model.Code + "\" " + sel + "><label for=\"" + InputName + "" + model.id + "\">" + Language.Tag(model.Name, lang.Code) + "</label>";
                }
            }
            return str;

        }
        /// <summary>
        /// 站点语言
        /// </summary>
        /// <param name="InputName"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public static string SiteLanguageCheckbox(string InputName, string Code, string languagecode, Lebi_Administrator admin = null)
        {
            string where = "";
            if (admin != null)
            {
                if (admin.Site_ids != "" && Site.Instance.SiteCount > 1)
                    where = "id in (" + admin.Site_ids + ")";
            }
            List<Lebi_Site> sites = B_Lebi_Site.GetList(where, "Sort desc", Site.Instance.SiteCount, 1);
            string str = "";
            Code = "," + Code + ",";
            foreach (Lebi_Site site in sites)
            {
                if (Site.Instance.SiteCount > 1)
                {
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        str += "<div class=\"col-sm-12\"><div class=\"row\"><div class=\"input-group\"><label for=\"\">" + site.SubName + "：</label>";
                    }
                    else
                    {
                        if (str == "")
                            str = site.SubName + "：";
                        else
                            str += "<br/>" + site.SubName + "：";
                    }
                }
                List<Lebi_Language> models = B_Lebi_Language.GetList("Site_id=" + site.id + "", "");
                foreach (Lebi_Language model in models)
                {
                    string sel = "";
                    if (Code.Contains("," + model.id + ","))
                        sel = "checked";
                    if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                    {
                        str += "<label class=\"custom-control custom-checkbox m-r-20\"><input type=\"checkbox\" id=\"" + InputName + "" + model.id + "\" name=\"" + InputName + "\" value=\"" + model.id + "\" class=\"custom-control-input\" shop=\"true\" " + sel + "><span class=\"custom-control-label\">" + model.Name + "</span></label>";
                    }else
                    {
                        str += "<input type=\"checkbox\" name=\"" + InputName + "\" id=\"" + InputName + "" + model.id + "\" shop=\"true\" value=\"" + model.id + "\" " + sel + "><label for=\"" + InputName + "" + model.id + "\">" + model.Name + "</label>";
                    }
                }
                if (!string.IsNullOrEmpty(RequestTool.GetConfigKey("SystemAdmin").Trim()))
                {
                    str += "</div></div></div>";
                }
            }
            return str;

        }
        /// <summary>
        /// 根据站点语言IDS返回逗分割的语言代码
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public static string LanuageidsToCodes(string ids)
        {
            if (ids == "")
                return "";
            List<Lebi_Language_Code> langs = B_Lebi_Language_Code.GetList("Code in (select Code from Lebi_Language where id in (lbsql{" + ids + "}))", "");
            string str = "";
            foreach (Lebi_Language_Code lang in langs)
            {
                if (str == "")
                    str = lang.Code;
                else
                    str += "," + lang.Code;
            }
            return str;
        }
        /// <summary>
        /// 后台编辑页面语言切换标签
        /// </summary>
        /// <param name="sel">选中语言</param>
        /// <returns></returns>
        public static string AdminLanguageTab(string sel)
        {
            List<Lebi_Language_Code> models = Languages();
            string str = "<ul class=\"tablist languagetab\">";
            Site site = new Site();
            foreach (Lebi_Language_Code model in models)
            {
                string s = "";
                if (sel == model.Code)
                    s = "class=\"selected\"";
                str += "<li id=\"li_" + model.Code + "\" " + s + " language=\"" + model.Code + "\" onclick=\"LanguageTab_EditPage('" + model.Code + "')\">";
                str += "<a><span>";
                //if (model.ImageUrl != "")
                //{
                //    str += "<em><img src=\"" + site.WebPath + model.ImageUrl + "\" /></em>";
                //    str += "<strong>" + model.Name + "</strong>";
                //}
                //else
                //{
                str += "" + model.Name + "";
                //}
                str += "</span></a></li>";
            }
            str += "</ul>";
            return str;
        }
        /// <summary>
        /// 后台编辑页面语言切换标签
        /// </summary>
        /// <param name="sel"></param>
        /// <param name="ex"></param>
        /// <param name="href"></param>
        /// <returns></returns>
        public static string AdminLanguageTab(int siteid, string ex, string href)
        {
            List<Lebi_Language> models = B_Lebi_Language.GetList("Site_id=" + siteid + "", "");
            string str = "<ul class=\"tablist languagetab\">";
            Site site = new Site();
            foreach (Lebi_Language model in models)
            {
                string s = "";
                str += "<li id=\"li_" + model.Code + "\" " + s + " onclick=\"LanguageTab_EditPage('" + model.Code + "')\">";
                str += "<a><span>";
                if (model.ImageUrl != "")
                {
                    str += "<em><img src=\"" + site.WebPath + model.ImageUrl + "\" /></em>";
                    str += "<strong>" + model.Name + "</strong>";
                }
                else
                {
                    str += "" + model.Name + "";
                }
                str += "</span></a></li>";
            }
            str += "<li onclick=\"" + href + "\">";
            str += "<a ><span>";
            str += "" + ex + "";
            str += "</span></a></li>";
            str += "</ul>";
            return str;
        }

        #region 翻译相关
        /// <summary>
        /// 返回指定语言内容
        /// </summary>
        /// <param name="con"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string Content(string con, string lang)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            //string szJson = @"[{""L"":""CN"",""C"":""全局""},{""L"":""EN"",""C"":""全局EN""}]";
            try
            {
                List<LanguageContent> langs = jss.Deserialize<List<LanguageContent>>(con);
                if (langs == null)
                    return "";
                //foreach (LanguageContent m in langs)
                //{
                //    if (m.L == lang)
                //        return m.C;
                //}
                LanguageContent model = (from m in langs
                                         where m.L == lang
                                         select m).ToList().FirstOrDefault();
                string res = "";
                if (model == null)
                    res = ""; // langs.FirstOrDefault().C;
                else
                    res = model.C;
                return res;

            }
            catch (Exception)
            {
                return "";
            }
        }
        public static string Content(string con, Lebi_Language_Code lang)
        {

            return Content(con, lang.Code);
        }
        public static string EditContent(string con, string code)
        {

            return HttpUtility.HtmlEncode(Content(con, code));
        }
        /// <summary>
        /// 序列化语言内容
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToJson(List<LanguageContent> list)
        {
            string json = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            json = jss.Serialize(list);
            return json;
        }
        /// <summary>
        /// 序列化内容
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string ToJson(string content)
        {
            string json = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            json = jss.Serialize(content);
            return json;
        }
        /// <summary>
        /// 从多语言表单中取值，转化为JSON
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RequestString(string key)
        {
            List<Lebi_Language_Code> langs = Languages();
            return RequestString(key, langs);
        }
        public static string RequestString(string key, List<Lebi_Language_Code> langs)
        {
            string json = "";
            List<LanguageContent> list = new List<LanguageContent>();

            LanguageContent con = new LanguageContent();
            foreach (Lebi_Language_Code lang in langs)
            {
                con = new LanguageContent();
                con.L = lang.Code;
                con.C = RequestTool.RequestString(key + lang.Code);
                list.Add(con);
            }
            json = ToJson(list);
            return json;
        }
        /// <summary>
        /// 前台编辑器获取内容方法
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RequestStringForUserEditor(string key)
        {
            List<Lebi_Language_Code> langs = Languages();
            return RequestStringForUserEditor(key, langs);
        }
        public static string RequestStringForUserEditor(string key, List<Lebi_Language_Code> langs)
        {
            string json = "";
            List<LanguageContent> list = new List<LanguageContent>();

            LanguageContent con = new LanguageContent();
            foreach (Lebi_Language_Code lang in langs)
            {
                con = new LanguageContent();
                con.L = lang.Code;
                con.C = RequestTool.RequestStringForUserEditor(key + lang.Code, "");
                list.Add(con);
            }
            json = ToJson(list);
            return json;
        }
        /// <summary>
        /// 获取安全字符串方法
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RequestSafeString(string key)
        {
            List<Lebi_Language_Code> langs = Languages();
            return RequestSafeString(key, langs);
        }
        public static string RequestSafeString(string key, List<Lebi_Language_Code> langs)
        {
            string json = "";
            List<LanguageContent> list = new List<LanguageContent>();

            LanguageContent con = new LanguageContent();
            foreach (Lebi_Language_Code lang in langs)
            {
                con = new LanguageContent();
                con.L = lang.Code;
                con.C = RequestTool.RequestSafeString(key + lang.Code, "");
                list.Add(con);
            }
            json = ToJson(list);
            return json;
        }
        /// <summary>
        /// 将输入字符串，转化为JSON
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            string json = "";
            List<LanguageContent> list = new List<LanguageContent>();

            LanguageContent con = new LanguageContent();
            foreach (Lebi_Language_Code lang in Languages())
            {
                con = new LanguageContent();
                con.L = lang.Code;
                con.C = key;
                list.Add(con);
            }
            json = ToJson(list);
            return json;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldstr"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static string GetString(string key, string oldstr, string langcode)
        {
            string json = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();
            bool flag = false;

            List<LanguageContent> newlist = new List<LanguageContent>();
            try
            {
                List<LanguageContent> list = jss.Deserialize<List<LanguageContent>>(oldstr);
                if (list != null)
                {
                    foreach (LanguageContent lang in list)
                    {
                        if (lang.L == langcode)
                        {
                            flag = true;
                            lang.C = key;
                        }
                        newlist.Add(lang);
                    }
                }
            }
            catch
            {
                flag = false;
            }
            if (!flag)
            {
                LanguageContent con = new LanguageContent();
                con.L = langcode;
                con.C = key;
                newlist.Add(con);
            }
            json = ToJson(newlist);
            return json;
        }
        /// <summary>
        /// 合并多语言字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="oldstr"></param>
        /// <param name="langcode"></param>
        /// <returns></returns>
        public static string ComboString(string oldstr, string str)
        {
            string json = "";
            JavaScriptSerializer jss = new JavaScriptSerializer();

            try
            {
                List<LanguageContent> oldlist = jss.Deserialize<List<LanguageContent>>(oldstr);
                List<LanguageContent> list = jss.Deserialize<List<LanguageContent>>(str);
                if (list != null)
                {
                    foreach (LanguageContent oldlc in oldlist)
                    {
                        foreach (LanguageContent lc in list)
                        {
                            if (oldlc.L == lc.L)
                            {
                                oldlc.C = oldlc.C + "," + lc.C;
                            }
                        }

                    }
                }
                json = ToJson(oldlist);
                return json;
            }
            catch
            {
                return oldstr;
            }


        }
        /// <summary>
        /// 更新多语言字符串
        /// </summary>
        /// <param name="oldstr"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string UpdateString(string oldstr, List<LanguageContent> list)
        {
            string json = "";
            if (list == null)
                return oldstr;
            if (list.Count == 0)
                return oldstr;
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<LanguageContent> oldlist;
            if (oldstr.Trim() == "")
            {
                oldlist = new List<LanguageContent>();
            }
            else
            {
                try
                {
                    oldlist = jss.Deserialize<List<LanguageContent>>(oldstr);
                }
                catch (Exception)
                {
                    oldlist = new List<LanguageContent>();
                }
            }
            // List<LanguageContent> list = jss.Deserialize<List<LanguageContent>>(str);
            if (oldlist.Count > 0)
            {
                for (int i = 0; i < oldlist.Count; i++)
                {
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (oldlist[i].L == list[j].L)
                        {
                            oldlist[i].C = list[j].C;
                        }
                    }
                }
            }
            foreach (LanguageContent lc in list)
            {
                if (oldlist.Count(o => o.L == lc.L) == 0)
                {
                    oldlist.Add(lc);
                }
            }
            json = ToJson(oldlist);
            return json;



        }
        #endregion
        /// <summary>
        /// 根据语言代码返回一个语言的实体
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        public static Lebi_Language_Code GetLanguage(string lang)
        {

            Lebi_Language_Code model = null;
            if (lang != "")
                model = B_Lebi_Language_Code.GetModel("Code=lbsql{'" + lang + "'}");
            //if (model == null)
            //    model = B_Lebi_Language.GetList("Code='" + RequestTool.GetConfigKey("DefaultLanguage") + "'", "").FirstOrDefault();
            if (model == null)
                model = DefaultLanguage();
            return model;
        }
        /// <summary>
        /// 返回默认语言
        /// </summary>
        /// <returns></returns>
        public static Lebi_Language_Code DefaultLanguage()
        {
            Lebi_Site site = ShopCache.GetMainSite();
            Lebi_Language_Code lang = B_Lebi_Language_Code.GetList("Code in (select Code from [Lebi_Language] where Site_id=" + site.id + ")", "Code asc").FirstOrDefault();
            if (lang == null)
                lang = new Lebi_Language_Code();
            return lang;
        }
        /// <summary>
        /// 当前语言
        /// </summary>
        /// <returns></returns>
        public static Lebi_Language_Code CurrentLanguage()
        {
            string Language = CookieTool.GetCookieString("Language");
            return GetLanguage(Language);

        }
        public static Lebi_Language_Code AdminCurrentLanguage()
        {
            string Language = CookieTool.GetCookieString("AdminLanguage");
            return AdminGetLanguage(Language);

        }
        public static Lebi_Language_Code AdminGetLanguage(string lang)
        {

            Lebi_Language_Code model = null;
            if (lang != "")
                model = B_Lebi_Language_Code.GetModel("Code='" + lang + "'");
            if (model == null)
                model = AdminDefaultLanguage();
            return model;
        }
        /// <summary>
        /// 返回默认语言
        /// </summary>
        /// <returns></returns>
        public static Lebi_Language_Code AdminDefaultLanguage()
        {
            Lebi_Language_Code lang = AdminLanguages().FirstOrDefault();
            if (lang == null)
                lang = new Lebi_Language_Code();
            return lang;
        }
        /// <summary>
        /// 当前语言
        /// </summary>
        /// <returns></returns>
        public static string CurrentLanguageFlag()
        {
            return CookieTool.GetCookieString("Language");
        }
        #region  币种相关
        /// <summary>
        /// 返回默认币种
        /// </summary>
        /// <returns></returns>
        public static Lebi_Currency DefaultCurrency()
        {
            List<Lebi_Currency> models = B_Lebi_Currency.GetList("", "IsDefault desc,Sort desc", 1, 1);
            Lebi_Currency model = models.FirstOrDefault();
            if (model == null)
                model = new Lebi_Currency();
            return model;
        }
        /// <summary>
        /// 返回当前币种
        /// 前台使用
        /// </summary>
        /// <returns></returns>
        public static Lebi_Currency CurrentCurrency(Lebi_Language lang)
        {
            string code = CookieTool.GetCookieString("Currency");
            Lebi_Currency model = B_Lebi_Currency.GetModel("Code=lbsql{'" + code + "'}");
            if (model == null)
                model = B_Lebi_Currency.GetModel(lang.Currency_id);
            if (model == null)
                return DefaultCurrency();
            return model;
        }
        /// <summary>
        /// 格式化货币
        /// </summary>
        /// <param name="money"></param>
        /// <param name="cur"></param>
        /// <returns></returns>
        public static string FormatMoney(decimal money, Lebi_Currency Currency)
        {
            if (Currency == null)
                Currency = DefaultCurrency();
            return FormatMoney(money, Currency.ExchangeRate, Currency.Msige, Currency.DecimalLength);
        }
        public static string FormatMoney(decimal money, string CurrencyCode)
        {
            if (CurrencyCode.Trim() == "Number")
            {
                return (money * DefaultCurrency().ExchangeRate).ToString("f" + DefaultCurrency().DecimalLength);
            }
            else
            {
                Lebi_Currency model = B_Lebi_Currency.GetModel("Code='" + CurrencyCode + "'");
                return FormatMoney(money, model);
            }
        }
        public static string FormatMoney(decimal money, int Currencyid)
        {
            Lebi_Currency model = B_Lebi_Currency.GetModel(Currencyid);
            return FormatMoney(money, model);
        }
        public static string FormatMoney(decimal money, decimal ExchangeRate, string Msige, int DecimalLength)
        {
            return Msige.Trim() + (money * ExchangeRate).ToString("f" + DecimalLength + "");
        }

        #endregion  币种相关
        /// <summary>
        /// 根据主题重新设置系统图片尺寸
        /// </summary>
        public static void UpdteImageSize()
        {
            BaseConfig mx = ShopCache.GetBaseConfig();
            List<Lebi_Language> langs = B_Lebi_Language.GetList("", "");
            Lebi_ImageSize model;

            string ids = "0";
            //检查系统默认的规格
            model = B_Lebi_ImageSize.GetModel("Width=" + mx.ImageBigWidth + " and Height=" + mx.ImageBigHeight + "");
            if (model == null)
            {
                model = new Lebi_ImageSize();
                model.Width = mx.ImageBigWidth;
                model.Height = mx.ImageBigHeight;
                B_Lebi_ImageSize.Add(model);
                ids += "," + B_Lebi_ImageSize.GetMaxId();
            }
            else
            {
                ids += "," + model.id;
            }
            model = B_Lebi_ImageSize.GetModel("Width=" + mx.ImageMediumWidth + " and Height=" + mx.ImageMediumHeight + "");
            if (model == null)
            {
                model = new Lebi_ImageSize();
                model.Width = mx.ImageMediumWidth;
                model.Height = mx.ImageMediumHeight;
                B_Lebi_ImageSize.Add(model);
                ids += "," + B_Lebi_ImageSize.GetMaxId();
            }
            else
            {
                ids += "," + model.id;
            }
            model = B_Lebi_ImageSize.GetModel("Width=" + mx.ImageSmallWidth + " and Height=" + mx.ImageSmallHeight + "");
            if (model == null)
            {
                model = new Lebi_ImageSize();
                model.Width = mx.ImageSmallWidth;
                model.Height = mx.ImageSmallHeight;
                B_Lebi_ImageSize.Add(model);
                ids += "," + B_Lebi_ImageSize.GetMaxId();
            }
            else
            {
                ids += "," + model.id;
            }
            foreach (Lebi_Language lang in langs)
            {
                Lebi_Theme theme = B_Lebi_Theme.GetModel(lang.Theme_id);
                if (theme == null)
                    continue;
                //model = B_Lebi_ImageSize.GetModel("Width=" + theme.ImageBig_Width + " and Height=" + theme.ImageBig_Height + "");
                //if (model == null)
                //{
                //    model = new Lebi_ImageSize();
                //    model.Width = theme.ImageBig_Width;
                //    model.Height = theme.ImageBig_Height;
                //    B_Lebi_ImageSize.Add(model);
                //    ids += "," + B_Lebi_ImageSize.GetMaxId();
                //}
                //else
                //{
                //    ids += "," + model.id;
                //}
                model = B_Lebi_ImageSize.GetModel("Width=" + theme.ImageMedium_Width + " and Height=" + theme.ImageMedium_Height + "");
                if (model == null)
                {
                    model = new Lebi_ImageSize();
                    model.Width = theme.ImageMedium_Width;
                    model.Height = theme.ImageMedium_Height;
                    B_Lebi_ImageSize.Add(model);
                    ids += "," + B_Lebi_ImageSize.GetMaxId();
                }
                else
                {
                    ids += "," + model.id;
                }
                //model = B_Lebi_ImageSize.GetModel("Width=" + theme.ImageSmall_Width + " and Height=" + theme.ImageSmall_Height + "");
                //if (model == null)
                //{
                //    model = new Lebi_ImageSize();
                //    model.Width = theme.ImageSmall_Width;
                //    model.Height = theme.ImageSmall_Height;
                //    B_Lebi_ImageSize.Add(model);
                //    ids += "," + B_Lebi_ImageSize.GetMaxId();
                //}
                //else
                //{
                //    ids += "," + model.id;
                //}
            }
            B_Lebi_ImageSize.Delete("id not in (" + ids + ")");
        }
    }

}

