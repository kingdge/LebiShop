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
using System.Web.UI.WebControls;
namespace Shop.Bussiness
{
    /// <summary>
    /// 前台页面的直接后台文件
    /// </summary>

    public class ShopPage : PageBase
    {

        protected string Table = "";
        protected string Where = "";
        protected string Order = "";
        protected int PageSize = 20;
        protected int pageindex = 1;
        protected int RecordCount = 0;
        protected string path;//面包屑导航
        protected int DT_id;
        //protected int CurrentLanguageID = 0;
        //protected string CurrentLanguage = "";
        private Lebi_Language CurrentLanguage_ = null;
        private Lebi_Theme CurrentTheme_ = null;
        private Lebi_User CurrentUser_;
        private Lebi_UserLevel CurrentUserLevel_;
        //protected Lebi_Currency CurrentCurrency;
        protected bool IsMainSite = false;//标记当前站点是否为主站点
        protected string ProductWhere = "";//商品展示条件，多站点，上架状态等
        protected string ProductCategoryWhere = "";//商品分类展示条件
        protected Shop.Model.ServicePanel servicepannel;
        protected Shop.Model.ServicePanel supplierservicepannel;
        protected static List<Lebi_Currency> Currencys = new List<Lebi_Currency>();
        public ShopPage()
        {
            pageindex = RequestTool.RequestInt("pageindex", 0);
            string servicepannelcon = SYS.ServicePanel;
            servicepannel = B_ServicePanel.GetModel(servicepannelcon);
            CurrentUser_ = EX_User.CurrentUser();
            CurrentUserLevel_ = B_Lebi_UserLevel.GetModel("id=" + CurrentUser_.UserLevel_id + "");

            if (CurrentUserLevel_ == null)
            {
                CurrentUserLevel_ = B_Lebi_UserLevel.GetList("Grade=0", "Grade asc").FirstOrDefault();
                if (CurrentUserLevel_ == null)
                {
                    CurrentUserLevel_ = new Lebi_UserLevel();
                    Log.Add("会员分组有误", "User_id=" + CurrentUser_.id.ToString());
                }
            }
            DT_id = GetDT();
            Currencys = B_Lebi_Currency.GetList("1=1", "Sort desc");
        }
        protected Lebi_User CurrentUser
        {
            get { return CurrentUser_; }
            set { CurrentUser_ = value; }
        }
        protected Lebi_UserLevel CurrentUserLevel
        {
            get { return CurrentUserLevel_; }
            set { CurrentUserLevel_ = value; }
        }

        public static bool IsWap()
        {
            string agent = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
            if (agent == "" || agent.IndexOf("mobile") != -1 || agent.IndexOf("mobi") != -1 || agent.IndexOf("nokia") != -1 || agent.IndexOf("samsung") != -1 || agent.IndexOf("sonyericsson") != -1 || agent.IndexOf("mot") != -1 || agent.IndexOf("blackberry") != -1 || agent.IndexOf("lg") != -1 || agent.IndexOf("htc") != -1 || agent.IndexOf("j2me") != -1 || agent.IndexOf("ucweb") != -1 || agent.IndexOf("opera mini") != -1 || agent.IndexOf("mobi") != -1)
            {
                //终端可能是手机 
                return true;
            }
            return false;
        }
        public static bool IsWechat()
        {
            string agent = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
            if (agent.Contains("micromessenger"))
            {
                return true;
            }
            return false;
        }
        public static bool IsAPP()
        {
            string agent = (HttpContext.Current.Request.UserAgent + "").ToLower().Trim();
            if (agent.Contains("lebishopapp"))
            {
                return true;
            }
            return false;
        }
        protected override void OnLoad(EventArgs e)
        {
            //if (IsWap())
            //{
            //    int pt = RequestTool.RequestInt("pt", 0);
            //    reqPage = reqPage.ToLower();
            //    if (pt == 0 && !reqPage.Contains("/wap") && !reqPage.Contains("/inc/") && !reqPage.Contains("ajax") && !reqPage.Contains("/theme/"))
            //    {
            //        reqPage = "/wap" + RequestTool.GetRequestUrlNonDomain();
            //        if (reqPage.Contains("?"))
            //        {
            //            reqPage = reqPage + "&pt=1";
            //        }
            //        else
            //        {
            //            reqPage = reqPage + "?pt=1";
            //        }
            //        Server.Transfer(reqPage);
            //        Response.End();
            //        return;
            //    }
            //}

            //页面载入检查
            PageLoadCheck();
            //推广账户代码
            string v = RequestTool.RequestString("v");
            if (v != "")
            {
                NameValueCollection nvs = new NameValueCollection();
                nvs.Add("id", v);
                CookieTool.WriteCookie("parentuser", nvs, 7);
            }
            //微信访问需要先登录
            string weixinlogin = RequestTool.RequestString("weixinlogin");
            if (weixinlogin == "1")
            {
                if (CurrentUser.id == 0 && IsWechat())
                {
                    string backurl = HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain());
                    Response.Redirect(WebPath + "/platform/redirect_weixin.aspx?backurl=" + backurl);
                    return;
                }
            }
            //Response.Write(CurrentSite.id);
            //<-{获取分销ID
            DT_id = GetDT();
            base.OnLoad(e);

        }
        #region 主题与页面
        /// <summary>
        /// 语言路径
        /// </summary>
        protected string LanguagePath
        {
            get
            {
                string lang = CurrentLanguage.Path;
                if (lang == "/")
                    lang = "";
                return lang;
            }
        }
        /// <summary>
        /// 当前页面
        /// </summary>
        protected Lebi_Theme_Page CurrentPage;
        /// <summary>
        /// 当前主题
        /// </summary>
        protected Lebi_Theme CurrentTheme
        {
            get
            {
                if (CurrentTheme_ != null)
                    return CurrentTheme_;
                if (Session["CurrentTheme"] != null)
                    return Session["CurrentTheme"] as Lebi_Theme;
                return new Lebi_Theme();
            }
        }
        /// <summary>
        /// 当前语言
        /// </summary>
        protected Lebi_Language CurrentLanguage
        {
            //get
            //{
            //    if (CurrentLanguage_ != null)
            //        return CurrentLanguage_;
            //    if (Session["CurrentLanguage"] != null)
            //        return Session["CurrentLanguage"] as Lebi_Language;
            //    return B_Lebi_Language.GetList("Code='CN'", "").FirstOrDefault();
            //}
            get
            {
                if (CurrentLanguage_ != null)
                    return CurrentLanguage_;
                if (Session["CurrentLanguage"] != null)
                    return Session["CurrentLanguage"] as Lebi_Language;
               
                CurrentLanguage_ = B_Lebi_Language.GetModel("Site_id = " + CurrentSite.id + " and Path='/'");
                if (CurrentLanguage_ != null)
                    return CurrentLanguage_;
                CurrentLanguage_ = B_Lebi_Language.GetModel("Site_id = " + CurrentSite.id + "");
                if (CurrentLanguage_ != null)
                    return CurrentLanguage_;
                return B_Lebi_Language.GetList("Code='CN'", "").FirstOrDefault();
            }
        }
        /// <summary>
        /// 当前币种
        /// </summary>
        Lebi_Currency Lebi_Currency_;
        protected Lebi_Currency CurrentCurrency
        {
            get
            {
                if (Lebi_Currency_ != null)
                    return Lebi_Currency_;
                return Language.CurrentCurrency(CurrentLanguage);
            }
            set
            {
                Lebi_Currency_ = value;
            }
        }
        /// <summary>
        /// 当前站点
        /// </summary>
        Lebi_Site CurrentSite_ = null;
        protected Lebi_Site CurrentSite
        {
            get
            {
                if (CurrentSite_ != null)
                    return CurrentSite_;
                if (Session["CurrentSite"] != null)
                    return Session["CurrentSite"] as Lebi_Site;
                var nv = CookieTool.GetCookie("ThemeStatus");
                int siteid = 0;
                if (!string.IsNullOrEmpty(nv.Get("site")))
                {
                    int.TryParse(nv.Get("site"), out siteid);
                }
                if (siteid == 0)
                    return ShopCache.GetMainSite();
                CurrentSite_ = B_Lebi_Site.GetModel(siteid);
                if (CurrentSite_ == null)
                    return ShopCache.GetMainSite();
                return CurrentSite_;
            }
            set
            {
                CurrentSite_ = value;
            }
        }
        /// <summary>
        /// 载入主题
        /// </summary>
        /// <param name="tcode"></param>
        /// <param name="lcode"></param>
        /// <param name="pcode"></param>
        protected virtual void LoadPage(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode);
        }
        public void LoadTheme(string themecode, string languagecode, string pcode)
        {
            LoadTheme(themecode, 1, languagecode, pcode);
        }
        protected virtual void LoadTheme(string themecode, int siteid, string languagecode, string pcode)
        {
            LoadTheme(themecode, siteid, languagecode, pcode, false);
        }
        /// <summary>
        /// 载入主题
        /// </summary>
        /// <param name="themecode"></param>
        /// <param name="siteid"></param>
        /// <param name="languagecode"></param>
        /// <param name="pcode"></param>
        /// <param name="checklogin">是否检查用户登录</param>
        public void LoadTheme(string themecode, int siteid, string languagecode, string pcode, bool checklogin)
        {
            CurrentSite_ = B_Lebi_Site.GetModel(siteid);
            if (ShopCache.GetMainSite().id == CurrentSite_.id || CurrentSite_.Domain == "")
                IsMainSite = true;
            CurrentLanguage_ = B_Lebi_Language.GetModel("Code='" + languagecode + "' and Site_id=" + siteid + "");
            if (CurrentLanguage_ == null)
            {
                CurrentLanguage_ = new Lebi_Language();
            }

            if (IsMainSite)
                CurrentLanguage_.Path = (CurrentSite_.Path + CurrentLanguage_.Path).Replace("//", "/").TrimEnd('/');
            if (checklogin)
            {
                if (CurrentUser.id == 0 || CurrentUser.IsAnonymous == 1)
                {
                    HttpContext.Current.Response.Redirect(URL("P_Login", "" + HttpUtility.UrlEncode(RequestTool.GetRequestUrlNonDomain()) + "," + GetUrlToken(RequestTool.GetRequestUrlNonDomain()) + ""));
                }
            }
            CurrentTheme_ = B_Lebi_Theme.GetModel("Code='" + themecode + "'");
            CurrentPage = B_Lebi_Theme_Page.GetModel("Code='" + pcode + "'");
            if (CurrentTheme_ == null)
                CurrentTheme_ = new Lebi_Theme();
            Lebi_Theme_Skin skin = B_Lebi_Theme_Skin.GetModel("Theme_id=" + CurrentTheme_.id + " and Code='" + pcode + "'");
            if (skin != null)
            {
                if (skin.PageSize > 0)
                    PageSize = skin.PageSize;
            }
            if (CurrentSite_ != null)
            {
                SYS.Copyright = CurrentSite_.Copyright;
                SYS.Domain = CurrentSite_.Domain;
                SYS.Email = CurrentSite_.Email;
                SYS.Fax = CurrentSite_.Fax;
                SYS.Keywords = CurrentSite_.Keywords;
                SYS.Logoimg = CurrentSite_.Logoimg;
                SYS.Description = CurrentSite_.Description;
                SYS.Phone = CurrentSite_.Phone;
                SYS.QQ = CurrentSite_.QQ;
                SYS.ServiceP = CurrentSite_.ServiceP;
                SYS.Name = CurrentSite_.Name;
                SYS.Title = CurrentSite_.Title;
                SYS.FootHtml = CurrentSite_.FootHtml;
                SYS.TopAreaid = CurrentLanguage_.TopAreaid.ToString();
            }
            LBTITLE = "";
            if (!Shop.LebiAPI.Service.Instanse.Check("lebilicense"))
            {
                LBTITLE += " - Powered by LebiShop";
                //底部版权信息
                //if (CurrentPage != null)
                //{
                //    StringBuilder sb = new StringBuilder();
                //    sb.Append("Powered by <a style=\"font-size:12px;color:#00497f\" href=\"http://www.lebi.cn\" target=\"_blank\" title=\"LebiShop多语言网上商店系统\">LebiShop</a> ");
                //    sb.Append("V" + SYS.Version + "." + SYS.Version_Son);
                //    try
                //    {
                //        Label label = (Label)this.Page.FindControl("LeBiLicense");
                //        label.Text = sb.ToString();
                //    }
                //    catch (System.NullReferenceException)
                //    {
                //        Response.Write("<div style=\"height:100px;padding-top:10px;text-align:left;font-size:12;\">内部错误，请到【配置】=》【站点语言】栏目中重新生成网站<br>");
                //        Response.Write(sb.ToString() + "</div>");
                //        Response.End();
                //    }
                //}
            }
            Session["CurrentTheme"] = CurrentTheme_;//session主要是为那些没有LoadTheme方法的页面服务的，如ajax
            Session["CurrentLanguage"] = CurrentLanguage_;
            Session["CurrentSite"] = CurrentSite_;
            //写入cookie
            NameValueCollection nvs = new NameValueCollection();
            nvs.Add("theme", CurrentTheme_.Code);
            nvs.Add("language", CurrentLanguage_.Code);
            nvs.Add("site", CurrentSite_.id.ToString());
            CookieTool.WriteCookie("ThemeStatus", nvs, 10);
            //多站点商品显示
            if (Shop.Bussiness.Site.Instance.SiteCount > 1)
            {
                //if (Shop.LebiAPI.Service.Instanse.Check("domain3admin"))
                //{
                if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
                {
                    ProductWhere = "(Charindex('," + CurrentSite_.id + ",',','+Site_ids+',')>0 or Site_ids='') and Product_id = 0 and Type_id_ProductStatus = 101";
                    ProductCategoryWhere = "(Charindex('," + CurrentSite_.id + ",',','+Site_ids+',')>0 or Site_ids='')";
                }
               else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
                {
                    ProductWhere = "(Instr(','+Site_ids+',','," + CurrentSite_.id + ",')>0 or Site_ids='') and Product_id = 0 and Type_id_ProductStatus = 101";
                    ProductCategoryWhere = "(Instr(','+Site_ids+',','," + CurrentSite_.id + ",')>0 or Site_ids='')";
                }
                else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
                {
                    ProductWhere = "(Instr(','+Site_ids+',','," + CurrentSite_.id + ",')>0 or Site_ids='') and Product_id = 0 and Type_id_ProductStatus = 101";
                    ProductCategoryWhere = "(Instr(','+Site_ids+',','," + CurrentSite_.id + ",')>0 or Site_ids='')";
                }
                //}
                //else
                //{
                //    ProductWhere = "Product_id = 0 and Type_id_ProductStatus = 101";
                //}
            }
            else
            {
                ProductWhere = "Product_id = 0 and Type_id_ProductStatus = 101";
                ProductCategoryWhere = "1=1";
            }
            //ProductWhere += " and (UserLevel_ids_show='' or UserLevel_ids_show is null or ','+UserLevel_ids_show+',' like '%," + CurrentUserLevel.id + ",%'  )";
            //用户的商品权限
            if (Shop.LebiAPI.Service.Instanse.Check("plugin_productlimit"))
            {
                if (SYS.ProductLimitType == "1")//选择表示允许
                {
                    if (CurrentUser.id == 0)
                    {
                        if (CurrentUserLevel_.id > 0)
                            ProductWhere += " and (id　not in (" + ProductLimit.UserLevelLimit(CurrentUserLevel_.id) + "))";
                    }
                    else
                    {
                        int lc = B_Lebi_Product_Limit.Counts("User_id=" + CurrentUser.id + "");
                        if (lc == 0)
                            ProductWhere += " and (id not in (" + ProductLimit.UserLevelLimit(CurrentUserLevel_.id) + "))";
                        else
                            ProductWhere += " and (id not in (" + ProductLimit.UserLevelLimit(CurrentUserLevel_.id) + ") or id in (" + ProductLimit.UserLimit(CurrentUser.id) + "))";
                    }
                }
                else//选择表示拒绝
                {
                    if (CurrentUser.id == 0)
                    {
                        if (CurrentUserLevel_.id > 0)
                            ProductWhere += " and (id not in (" + ProductLimit.UserLevelLimit(CurrentUserLevel_.id) + "))";
                    }
                    else
                    {
                        int lc = B_Lebi_Product_Limit.Counts("User_id=" + CurrentUser.id + "");
                        if (lc == 0)
                            ProductWhere += " and (id not in (" + ProductLimit.UserLevelLimit(CurrentUserLevel_.id) + "))";
                        else
                            ProductWhere += " and (id not in (" + ProductLimit.UserLevelLimit(CurrentUserLevel_.id) + ") and id not in (" + ProductLimit.UserLimit(CurrentUser.id) + "))";
                        //ProductWhere += " and (id not in (" + ProductLimit.UserLimit(CurrentUser.id) + "))";
                    }
                }
                //SystemLog.Add(ProductWhere);
            }
            //<-{分销站点
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                string DT_Product_ids = "";
                Lebi_DT CurrentDT = B_Lebi_DT.GetModel(DT_id);
                if (CurrentDT != null)
                {
                    DT_Product_ids = CurrentDT.Product_ids;
                    if (DT_Product_ids == "")
                        DT_Product_ids = "0";
                    ProductWhere += " and id in(select dt_p.Product_id from [Lebi_DT_Product] as dt_p where dt_p.DT_id = " + DT_id + ")";
                }
            }
            ProductWhere += " and (IsDel!=1 or IsDel is null)";
            //}->
        }

        public void LoadTheme()
        {
            string themecode = "";
            string language = "";
            int siteid = 0;
            var nv = CookieTool.GetCookie("ThemeStatus");
            if (!string.IsNullOrEmpty(nv.Get("language")))
            {
                language = nv.Get("language");
            }
            if (!string.IsNullOrEmpty(nv.Get("theme")))
            {
                themecode = nv.Get("theme");
            }
            if (!string.IsNullOrEmpty(nv.Get("site")))
            {
                int.TryParse(nv.Get("site"), out siteid);
            }
            if (siteid == 0)
                siteid = ShopCache.GetMainSite().id;
            LoadTheme(themecode, siteid, language, "", false);
        }
        /// <summary>
        /// 处理前台商品图片
        /// </summary>
        /// <param name="imageold"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ProImg(string imageold, string type)
        {
            return Image(imageold, type);
        }
        public string Image(Lebi_Product pro, string type)
        {
            return Image(pro.ImageOriginal, type);
        }

        public void Advertisement(string code)
        {
            Lebi_Theme_Advert model = B_Lebi_Theme_Advert.GetModel("Code='" + code + "' and Theme_id=" + CurrentTheme.id + "");
            if (model == null)
                return;
            if (model.Content == "")
            {
                model.Content = "${<a href=\"{%URL%}\"><img src=\"{%Image%}\" title=\"{%Title%}\" width=\"{%Width%}\" height=\"{%Height%}\" /></a>}$";
            }
            string vv = Shop.Bussiness.Theme.DoAdvertCodeConvert(model.Content, model, CurrentLanguage, CurrentTheme);
            Response.Write(vv);
        }

        public string ImgSrc(string str, int IsLazyLoad = 0)
        {
            return ContentImage(str, IsLazyLoad);
        }

        #endregion
        #region 处理url
        public string URL(string code, string para)
        {
            return ThemeUrl.GetURL(code, para, "", CurrentSite, CurrentLanguage);
        }
        public string URL(string code, int para)
        {
            return ThemeUrl.GetURL(code, para.ToString(), "", CurrentSite, CurrentLanguage);
        }
        public string URL(string code, string para, string url)
        {
            return ThemeUrl.GetURL(code, para, url, CurrentSite, CurrentLanguage);
        }
        public string URL(string code, int para, string url)
        {
            return ThemeUrl.GetURL(code, para.ToString(), url, CurrentSite, CurrentLanguage);
        }
        /// <summary>
        /// 返回相应主题的跳转地址
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string JumpURL(string url)
        {
            url = "/" + CurrentLanguage.Path + "/" + url;
            url = ThemeUrl.CheckURL(url);
            return url;
        }
        #endregion
        #region 参数获取
        public int Rint(string KeyName, int def = 0)
        {
            return RequestTool.RequestInt(KeyName, def);
        }
        public string Rstring(string KeyName)
        {
            return StringTool.HtmlFiltrate(RequestTool.RequestString(KeyName));
        }
        public string Rstring_Para(string para)
        {
            string key = CurrentPage.PageParameter;
            string[] arr = key.Split('&');
            foreach (string k in arr)
            {
                if (k.Contains("{" + para + "}"))
                {
                    key = k.Remove(k.IndexOf("="));
                    break;
                }
            }
            // key = RegexTool.GetRegValue(key, @".*?&(.*?)=\{" + para + @"\}.*?");
            return Rstring(key);
        }
        public int Rint_Para(string para, int def = 0)
        {
            string key = CurrentPage.PageParameter;
            string[] arr = key.Split('&');
            foreach (string k in arr)
            {
                if (k.Contains("{" + para + "}"))
                {
                    key = k.Remove(k.IndexOf("="));
                    break;
                }
            }
            return Rint(key, def);
        }
        /// <summary>
        /// 根据结点代码返回结点
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public Lebi_Node Node(string code)
        {
            return NodePage.GetNodeByCode(code);

        }
        #endregion
        #region 处理HTML
        public string RemoveHtml(string strIn)
        {
            strIn = StringTool.FilterAll(strIn);

            return strIn;
        }
        public string Sub(string strIn, int len)
        {
            if (strIn.Length > len)
            {
                strIn = strIn.Substring(0, len);
            }
            return strIn;
        }
        #endregion
        #region 处理分页简洁
        public string PagerSimple(string para)
        {
            return Shop.Bussiness.Pager.GetPaginationStringForWebSimple("?page={0}" + para, pageindex, PageSize, RecordCount, CurrentLanguage);
        }
        public string PagerSimple()
        {
            return PagerSimple("");
        }
        #endregion
        #region 处理分页
        public string Pager(string para)
        {
            para = "&" + para.TrimStart('&');
            return Shop.Bussiness.Pager.GetPaginationStringForWeb("?page={0}" + para, pageindex, PageSize, RecordCount, CurrentLanguage);
        }
        public string Pager()
        {
            return Pager("");
        }
        #endregion
        #region 杂项
        /// <summary>
        /// 页面错误，
        /// </summary>
        public void PageError()
        {
            Response.Write("参数错误");
            Response.End();
        }
        /// <summary>
        /// 处理语言
        /// </summary>
        /// <param name="content"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public string Lang(string content)
        {
            var nv = CookieTool.GetCookie("ThemeStatus");
            //if (!string.IsNullOrEmpty(nv.Get("language")))
            //{
            //    langcode = nv.Get("language");
            //    //return langcode;
            //    return Language.Content(content, langcode);
            //}
            //return CurrentLanguage.Code;
            return Language.Content(content, CurrentLanguage.Code);
        }
        /// <summary>
        /// 处理语言标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Tag(string content)
        {
            var nv = CookieTool.GetCookie("ThemeStatus");
            string langcode = "";
            if (!string.IsNullOrEmpty(nv.Get("language")))
            {
                langcode = nv.Get("language");
                return Language.Tag(content, langcode);
            }

            return Language.Tag(content, CurrentLanguage.Code);
        }
        /// <summary>
        /// 返回系统类型名称
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string TypeName(int id)
        {
            return Tag(EX_Type.TypeName(id));
        }
        public Lebi_Product GetProduct(int id)
        {
            return EX_Product.GetProduct(id);
        }
        public string GetCNZZ()
        {
            Lebi_Cnzz model = B_Lebi_Cnzz.GetList("", "").FirstOrDefault();
            if (model == null)
            {
                return "";
            }
            else
            {
                if (model.state == 0)
                    return "";
                string[] list = model.Ccontent.Split('@');
                if (list.Length > 1)
                {
                    return "<script src=\"http://pw.cnzz.com/c.php?id=" + list[0] + "&l=2\" language=\"JavaScript\"></script>";
                }
                return "";
            }
        }
        #endregion
        #region 商品货币金额相关
        /// <summary>
        /// 格式化金额，加上当前货币符号
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public string FormatMoney(decimal money)
        {
            StringBuilder sb = new StringBuilder();
            if (SYS.IsMutiCurrencyShow == "0")
            {
                sb.Append("<span class='lebimoney'>");
                sb.Append("<font class='msige'>" + CurrentCurrency.Msige + "</font>");
                if (money == -9999999999)
                {
                    sb.Append("<font class='money_1 price'> - </font>");
                }
                else
                {
                    sb.Append("<font class='money_1 price'>" + (money * CurrentCurrency.ExchangeRate).ToString("f" + CurrentCurrency.DecimalLength + "") + "</font>");
                    sb.Append("<font class='money_0' style='display:none;'>" + money + "</font>");
                }
                sb.Append("</span>");
            }
            else
            {
                foreach (Lebi_Currency cny in Currencys)
                {
                    sb.Append("<span class='lebimoney currency" + cny.Code + "'>");
                    sb.Append("<font class='msige'>" + cny.Msige + "</font>");
                    sb.Append("<font class='money_1 price'>" + (money * cny.ExchangeRate).ToString("f" + CurrentCurrency.DecimalLength + "") + "</font>");
                    sb.Append("<font class='money_0' style='display:none;'>" + money + "</font>");
                    sb.Append("</span>");
                }
            }
            return sb.ToString();
            //return  CurrentCurrency.  + "" + money.ToString("0.00");
        }
        public string FormatMoney(string money)
        {
            return FormatMoney(Convert.ToDecimal(money));
        }
        public string FormatMoney(decimal money, string format)
        {
            return (money * CurrentCurrency.ExchangeRate).ToString("f" + CurrentCurrency.DecimalLength);
        }
        public decimal FormatMoneyValue(decimal money, Lebi_Currency cny)
        {
            return money * cny.ExchangeRate;
        }
        public decimal FormatMoneyValue(decimal money, string cnycode)
        {
            Lebi_Currency cny = B_Lebi_Currency.GetModel("Code='" + cnycode + "'");
            if (cny == null)
                return money;
            return money * cny.ExchangeRate;
        }
        public decimal ProductPrice(Lebi_Product pro)
        {
            return EX_Product.ProductPrice(pro, CurrentUserLevel, CurrentUser);
        }
        public decimal ProductPrice_Market(Lebi_Product pro)
        {
            //<-{获取分销价格
            int DT_id = ShopPage.GetDT();
            if (DT_id > 0)
            {
                Lebi_DT_Product DT_product = B_Lebi_DT_Product.GetModel("DT_id = " + DT_id + " and Product_id = " + pro.id);
                if (DT_product != null)
                {
                    return DT_product.Price_Market;
                }
            }
            //}->
            return pro.Price_Market;
        }
        public int ProductLevelCount(Lebi_Product pro)
        {
            return EX_Product.ProductLevelCount(pro, CurrentUserLevel, CurrentUser);
        }
        public int ProductStock(Lebi_Product pro)
        {
            return EX_Product.ProductStock(pro);
        }
        public int ProductCountSon(int pid)
        {
            return B_Lebi_Product.Counts("(IsDel!=1 or IsDel is null) and Product_id=" + pid + " and Product_id<>0");
        }
        #endregion
        #region meta标签
        virtual public string ThemePageMeta(string code, string tag)
        {
            return ThemePageMeta(code, tag, "");
        }
        virtual public string ThemePageMeta(string code, string tag, string defaultstr)
        {
            string res = "";
            if (defaultstr != "")
            {
                res = defaultstr;
                if (tag == "title")
                    res += " - " + Lang(SYS.Name);
            }
            else
            {
                Lebi_Theme_Page model = B_Lebi_Theme_Page.GetModel("Code='" + code + "'");
                if (model == null)
                    res = "";
                else
                {
                    switch (tag.ToLower())
                    {
                        case "description":
                            if (Lang(model.SEO_Description) != "")
                                res = Lang(model.SEO_Description);
                            break;
                        case "keywords":
                            if (Lang(model.SEO_Keywords) != "")
                                res = Lang(model.SEO_Keywords);
                            break;
                        default:
                            if (Lang(model.SEO_Title) != "")
                                res = Lang(model.SEO_Title) + " - ";
                            res += Lang(SYS.Name);
                            break;
                    }
                }
            }
            return res + LBTITLE;
        }
        virtual public string ThemePageMeta(string code, string tag, string defaultstr, string page)
        {
            string res = "";
            if (defaultstr != "")
            {
                res = defaultstr;
                if (page != "P_Index")
                {
                    if (tag == "title")
                        res += " - " + Lang(SYS.Name);
                }
            }
            else
            {
                Lebi_Theme_Page model = B_Lebi_Theme_Page.GetModel("Code='" + code + "'");
                if (model == null)
                    res = "";
                else
                {
                    switch (tag.ToLower())
                    {
                        case "description":
                            if (Lang(model.SEO_Description) != "")
                                res = Lang(model.SEO_Description);
                            break;
                        case "keywords":
                            if (Lang(model.SEO_Keywords) != "")
                                res = Lang(model.SEO_Keywords);
                            break;
                        default:
                            if (page != "P_Index")
                            {
                                if (Lang(model.SEO_Title) != "")
                                    res = Lang(model.SEO_Title) + " - ";
                                res += Lang(SYS.Name);
                            }
                            break;
                    }
                }
            }
            return res + LBTITLE;
        }
        #endregion
        #region 商品分类相关
        /// <summary>
        /// 商品分类查询页面的规格筛选参数
        /// sel 格式为 
        /// 面料id_子id，颜色id——子id  如 14|0$47|22
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="sel"></param>
        /// <returns></returns>
        public string Categoryhref(int pid, int id, string sel)
        {
            if (sel == "")
                return pid + "|" + id;

            string reg = pid + @"\|[\d]*";
            int count = RegexTool.GetRegCount(sel, reg);
            if (count > 0)
            {
                sel = RegexTool.ReplaceRegValue(sel, reg, pid + "|" + id);
            }
            else
            {
                sel += "$" + pid + "|" + id;
            }
            return sel;
        }
        /// <summary>
        /// 商品分类查询页面-根据筛选规格返回查询参数
        /// </summary>
        /// <param name="sel"></param>
        /// <returns></returns>
        public string Categorywhere(string sel)
        {
            if (sel == "")
                return "1=1";
            string where = "";
            string[] arr = sel.Split('$');
            for (int i = 0; i < arr.Length; i++)
            {
                int id = 0;
                string id_ = RegexTool.GetRegValue(arr[i], @"[\d]*\|([\d]*)");
                int.TryParse(id_, out id);
                if (id > 0)
                {
                    if (where == "")
                        where = " ProPerty132 like '%" + id + "%'";
                    else
                        where += " and ProPerty132 like '%" + id + "%'";
                }

            }
            if (where == "")
                where = "1=1";
            return where;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Categorywhereforid(int id)
        {
            string str = "select Lebi_Pro_Type.id from Lebi_Pro_Type where Lebi_Pro_Type.id=" + id + " or Lebi_Pro_Type.Path like '%," + id + ",%'";
            return str;
        }
        /// <summary>
        /// 返回某个商品分类下，所有子id的组合
        /// 逗号分隔
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string Categoryid(int id)
        {
            string str = id.ToString();
            List<Lebi_Pro_Type> ts = B_Lebi_Pro_Type.GetList("Parentid=" + id + " and IsShow = 1", "Sort desc");
            foreach (Lebi_Pro_Type t in ts)
            {
                str += "," + Categoryid(t.id);
            }
            return str;
        }
        public string Categoryidspeed(int id)
        {
            string str = id.ToString();
            List<Lebi_Pro_Type> ts = B_Lebi_Pro_Type.GetList("Parentid=" + id + " and IsShow = 1", "Sort desc", 10, 1);
            foreach (Lebi_Pro_Type t in ts)
            {
                str += "," + Categoryidspeed(t.id);
            }
            return str;
        }
        /// <summary>
        /// 某个商品分类的路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string[,] Categorypath(int id)
        {
            string str = Categorypath_ids(id);
            string[] arr = str.Split(',');
            string[,] res = new string[arr.Length, 3];
            for (int i = 0; i < arr.Length; i++)
            {
                Lebi_Pro_Type model = B_Lebi_Pro_Type.GetModel("id = " + int.Parse(arr[i]) + " and IsShow = 1");
                if (model == null)
                    continue;
                int j = arr.Length - i - 1;
                res[j, 0] = model.id.ToString();
                res[j, 1] = Lang(model.Name);
                res[j, 2] = Lang(model.Url);
            }
            return res;
        }
        public string Categorypath_ids(int id)
        {
            Lebi_Pro_Type model = B_Lebi_Pro_Type.GetModel(id);
            if (model != null)
            {
                string str = model.id.ToString();
                if (model.Parentid > 0)
                    str += "," + Categorypath_ids(model.Parentid);
                return str;
            }
            else
            {
                return "0";
            }
        }
        /// <summary>
        /// 返回商品类别查询语句
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string CategoryWhere(int id)
        {
            string where = "(Pro_Type_id in (" + Categorywhereforid(id) + ") or ";
            if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
            {
                where += " Charindex('," + id + ",',','+Pro_Type_id_other+',')>0)";
            }
           else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                where += " Instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                where += " Instr(','+Pro_Type_id_other+',','," + id + ",')>0)";
            }
            return where;
        }
        /// <summary>
        /// 返回店铺商品类别查询语句
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string ShopCategoryWhere(int id)
        {
            string where = "";
            if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
            {
                where = " Charindex('," + id + ",',','+Supplier_ProductType_ids+',')>0";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                where = " Instr(','+Supplier_ProductType_ids+',','," + id + ",')>0";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                where = " instr(','+Supplier_ProductType_ids+',','," + id + ",')>0";
            }
            return where;
        }
        /// <summary>
        /// 返回商品标签查询语句
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string TagWhere(int id)
        {
            string where = "";
            if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "sqlserver")
            {
                where += " Charindex('," + id + ",',','+Pro_Tag_id+',')>0";
            }
          else  if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "access")
            {
                where += " Instr(','+Pro_Tag_id+',','," + id + ",')>0";
            }
            else if (LB.DataAccess.BaseUtils.BaseUtilsInstance.DBType == "mysql")
            {
                where += " Instr(','+Pro_Tag_id+',','," + id + ",')>0";
            }
            return where;
        }
        #endregion
        #region 购物车收藏夹相关
        /// <summary>
        /// 购物车商品总数
        /// </summary>
        /// <returns></returns>
        public int Basket_Product_Count()
        {

            Basket basket = new Basket(0);
            return basket.Count;
        }
        /// <summary>
        /// 购物车商品总价格
        /// </summary>
        /// <returns></returns>
        public decimal Basket_Product_Price()
        {
            Basket basket = new Basket(0);
            if (basket == null)
            {
                return 0;
            }
            else
            {
                return basket.Money_Product;
            }
            /*
            decimal price = 0;
            List<Lebi_User_Product> models = Basket_Product();
            foreach (Lebi_User_Product model in models)
            {
                Lebi_Product pro = B_Lebi_Product.GetModel(model.Product_id);
                if (pro != null)
                    price = price + pro.Price;
            }
            
            return price;
             */
        }
        /// <summary>
        /// 购物车商品
        /// </summary>
        /// <returns></returns>
        public List<Lebi_User_Product> Basket_Product()
        {
            return Basket.UserProduct(CurrentUser, 142);
        }
        /// <summary>
        /// 收藏夹商品总数
        /// </summary>
        /// <returns></returns>
        public int Like_Product_Count()
        {
            return Like_Product().Count;
        }
        /// <summary>
        /// 收藏夹商品
        /// </summary>
        /// <returns></returns>
        public List<Lebi_User_Product> Like_Product()
        {
            return Basket.UserProduct(CurrentUser, 141);
        }
        /// <summary>
        /// 浏览历史商品
        /// </summary>
        /// <returns></returns>
        public List<Lebi_User_Product> History_Product(int count = 10)
        {
            return EX_User.UserProduct(CurrentUser, 143, count);
        }
        public int Count_NewMessage()
        {
            if (CurrentUser.id > 0)
            {
                return B_Lebi_Message.Counts("User_id_to=" + CurrentUser.id + " and IsRead=0");
            }
            else
            {
                return 0;
            }
        }
        public int Count_Message(int IsRead)
        {
            if (CurrentUser.id > 0)
            {
                return B_Lebi_Message.Counts("User_id_to=" + CurrentUser.id + " and IsRead=" + IsRead + "");
            }
            else
            {
                return 0;
            }
        }
        public int Count_NewProductComment()
        {
            if (CurrentUser.id > 0)
            {
                return B_Lebi_Comment.Counts("Parentid = 0 and User_id=" + CurrentUser.id + " and TableName = 'Product' and IsRead=0");
            }
            else
            {
                return 0;
            }
        }
        public int Count_ProductComment(int IsRead)
        {
            if (CurrentUser.id > 0)
            {
                return B_Lebi_Comment.Counts("Parentid = 0 and User_id=" + CurrentUser.id + " and TableName = 'Product' and IsRead=" + IsRead + "");
            }
            else
            {
                return 0;
            }
        }
        public int Count_NewProductAsk()
        {
            if (CurrentUser.id > 0)
            {
                return B_Lebi_Comment.Counts("Parentid = 0 and User_id=" + CurrentUser.id + " and TableName = 'Product_Ask' and IsRead=0");
            }
            else
            {
                return 0;
            }
        }
        public int Count_ProductAsk(int IsRead)
        {
            if (CurrentUser.id > 0)
            {
                return B_Lebi_Comment.Counts("Parentid = 0 and User_id=" + CurrentUser.id + " and TableName = 'Product_Ask' and IsRead=" + IsRead + "");
            }
            else
            {
                return 0;
            }
        }
        public int Count_MewComment()
        {
            if (CurrentUser.id > 0)
            {
                return B_Lebi_Order_Product.Counts("User_id=" + CurrentUser.id + " and IsCommented=0");
            }
            else
            {
                return 0;
            }
        }
        public int Count_Comment(int IsCommented)
        {
            if (CurrentUser.id > 0)
            {
                string sql = " IsCommented=" + IsCommented + "";
                if (IsCommented == 0)
                    sql += " and id in(select id from [Lebi_Order_Product] where Order_id in(select id from [Lebi_Order] where User_id=" + CurrentUser.id + " and IsReceived = 1))";
                return B_Lebi_Order_Product.Counts("User_id=" + CurrentUser.id + " and " + sql + "");
            }
            else
            {
                return 0;
            }
        }
        #endregion
        #region 订单状态
        /// <summary>
        /// 订单状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string OrderStatus(Lebi_Order order)
        {
            string res = "已完成";
            if (order.IsInvalid == 0)
            {
                if (order.IsCompleted == 0)
                {
                    if (order.IsReceived == 0)
                    {
                        if (order.IsShipped == 0)
                        {
                            //res = "已确认";
                            if (order.IsPaid == 0)
                            {
                                res = "等待付款";
                            }
                            else
                            {
                                res = "等待发货";
                            }
                            //if (order.IsVerified == 0)
                            //{
                            //    res = "未确认";
                            //}
                            //else
                            //{

                            //}
                        }
                        else
                        {
                            if (order.IsShipped_All == 0)
                                res = "部分发货";
                            else
                                res = "已发货";
                        }
                    }
                    else
                    {
                        if (order.IsReceived_All == 0)
                            res = "部分收货";
                        else
                            res = "已收货";
                    }
                }
            }
            else
            {
                res = "已取消";
            }
            return Tag(res);
        }
        #endregion
        #region 退货订单状态
        /// <summary>
        /// 退货订单状态
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public string ReturnStatus(Lebi_Order order)
        {
            string res = "已完成";
            if (order.IsInvalid == 0)
            {
                if (order.IsCompleted == 0)
                {
                    if (order.IsReceived == 0)
                    {
                        if (order.IsShipped == 0)
                        {
                            res = "等待发货";
                            //if (order.IsVerified == 0)
                            //{
                            //    res = "未确认";
                            //}
                            //else
                            //{

                            //}
                        }
                        else
                        {
                            if (order.IsShipped_All == 0)
                                res = "部分发货";
                            else
                                res = "已发货";
                        }
                    }
                    else
                    {
                        if (order.IsReceived_All == 0)
                            res = "部分收货";
                        else
                            res = "已收货";
                    }
                }
            }
            else
            {
                res = "已取消";
            }
            return Tag(res);
        }
        #endregion
        #region 通用数据源
        public List<Lebi_Page> TopNews(int pagesize, string order)
        {
            string w = "Node_id=" + Node("News").id;
            List<Lebi_Page> models = B_Lebi_Page.GetList(w, order, pagesize, 1);
            //int f=models.Count;
            return models;

        }
        /// <summary>
        /// 返回子分类
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Lebi_Pro_Type> Cateporys(int id)
        {
            List<Lebi_Pro_Type> models = EX_Product.ShowTypes(id, CurrentSite.id);
            if (models.Count == 0)
            {
                Lebi_Pro_Type m = B_Lebi_Pro_Type.GetModel(id);
                if (m == null)
                {
                    m = new Lebi_Pro_Type();
                }
                if (m.Parentid > 0)
                    return Cateporys(m.Parentid);
            }
            return models;
        }
        /// <summary>
        /// 返回支持的语言
        /// </summary>
        /// <returns></returns>
        public List<Lebi_Language> Languages()
        {
            return Language.SiteLanguages(CurrentSite.id);
        }
        #endregion
        #region 获取当前请求路径及参数的信息 不带域名
        /// <summary>
        /// 获取当前请求路径及参数的信息 不带域名
        /// 形如：/Default.aspx?o=312 
        /// </summary>
        /// <returns></returns>
        public static string GetRequestUrlNonDomain()
        {
            //Request.RawUrl 也可以
            try
            {
                return System.Web.HttpContext.Current.Request.Url.PathAndQuery;
            }
            catch
            {
                return "";
            }
        }
        public static string GetRequestUrlNonDomainToken()
        {
            try
            {
                return EX_User.MD5(ShopCache.GetBaseConfig().InstallCode + System.Web.HttpContext.Current.Request.Url.PathAndQuery);
            }
            catch
            {
                return "";
            }
        }
        #endregion
        #region 获取客服面板软件信息
        public Lebi_ServicePanel_Type GetServicePanelType(int type_id)
        {
            Lebi_ServicePanel_Type type = B_Lebi_ServicePanel_Type.GetModel(type_id);
            if (type == null)
                type = new Lebi_ServicePanel_Type();
            return type;
        }
        #endregion
        #region 处理日期格式yyyy/mm/dd
        public string DateFormat(DateTime datetime)
        {
            return datetime.ToString("yyyy/MM/dd HH:mm:ss");
        }
        #endregion
        #region 处理商品折扣
        public int Discount(decimal price_market, decimal price)
        {
            int ret;
            if (price_market == 0)
            {
                ret = 0;
            }
            else
            {
                ret = Convert.ToInt32((1 - (price / price_market)) * 100);
            }
            return ret;
        }
        #endregion
        public static string GetUrlToken(string url)
        {
            try
            {
                return Shop.Bussiness.EX_User.MD5(ShopCache.GetBaseConfig().InstallCode + url);
            }
            catch
            {
                return "";
            }
        }
        public static int GetDT()
        {
            int DT_id = LB.Tools.RequestTool.RequestInt("dt", 0);
            if (DT_id == 0)
            {
                var nv = LB.Tools.CookieTool.GetCookie("URLPara");
                string dt_id_ = nv.Get("dt");
                int outdt_id = 0;
                int.TryParse(dt_id_, out outdt_id);
                DT_id = Convert.ToInt16(dt_id_);
                if (DT_id > 0)
                {
                    return DT_id;
                }
            }
            if (DT_id == 0)
            {
                string domain = LB.Tools.RequestTool.GetRequestDomain();
                Lebi_DT CurrentDT = B_Lebi_DT.GetModel("Domain = lbsql{'" + domain + "'} and Domain != ''");
                if (CurrentDT != null)
                {
                    return CurrentDT.id;
                }
            }
            return DT_id;
        }
    }

}