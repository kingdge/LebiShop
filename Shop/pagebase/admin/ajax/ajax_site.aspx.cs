using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.Text;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
using System.Reflection;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.IO;
namespace Shop.Admin.Ajax
{
    public partial class Ajax_site : AdminAjaxBase //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = LB.Tools.RequestTool.RequestString("__Action");
            Type type = this.GetType();
            MethodInfo methodInfo = type.GetMethod(action);
            if (methodInfo != null)
                methodInfo.Invoke(this, null);
        }
        /// <summary>
        /// 保存配置信息
        /// </summary>
        /// <returns></returns>
        public void BaseConfig_Edit()
        {
            if (!EX_Admin.Power("baseconfig_edit", "基本设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.BillFlag = RequestTool.RequestString("BillFlag");
            model.ClickNum1 = RequestTool.RequestString("ClickNum1");
            model.ClickNum2 = RequestTool.RequestString("ClickNum2");
            model.ClickFlag = RequestTool.RequestString("ClickFlag");
            model.CommFlag = RequestTool.RequestString("CommFlag");
            model.Copyright = Language.RequestString("Copyright");
            model.Description = Language.RequestString("Description");
            model.Domain = RequestTool.RequestString("Domain");
            model.Email = Language.RequestString("Email");
            model.Fax = Language.RequestString("Fax");
            model.Filter = RequestTool.RequestString("Filter");
            model.HtmlFlag = RequestTool.RequestString("HtmlFlag");
            model.Keywords = Language.RequestString("Keywords");
            model.Logoimg = Language.RequestString("Logoimg");
            model.Loop = RequestTool.RequestString("Loop");
            model.Openpwd = RequestTool.RequestString("Openpwd");
            model.OpenpwdFlag = RequestTool.RequestString("OpenpwdFlag");
            model.Phone = Language.RequestString("Phone");
            model.QQ = Language.RequestString("QQ");
            model.SalesFlag = RequestTool.RequestString("SalesFlag");
            model.SalesNum1 = RequestTool.RequestString("SalesNum1");
            model.SalesNum2 = RequestTool.RequestString("SalesNum2");
            model.ServiceP = Language.RequestString("ServiceP");
            model.Tieshi = Language.RequestString("Tieshi");
            model.Name = Language.RequestString("Name");
            model.Title = Language.RequestString("Title");
            model.UrlFlag = RequestTool.RequestString("UrlFlag");
            model.UpLoadLimit = RequestTool.RequestString("UpLoadLimit");
            model.UpLoadSaveName = RequestTool.RequestString("UpLoadSaveName");
            model.UpLoadRName = RequestTool.RequestString("UpLoadRName");
            model.UpLoadMode = RequestTool.RequestString("UpLoadMode");
            model.TakeMoneyLimit = RequestTool.RequestString("TakeMoneyLimit");
            model.IsOpenPaidOrderConfirm = RequestTool.RequestString("IsOpenPaidOrderConfirm");
            model.IsReduceBasketStep = RequestTool.RequestString("IsReduceBasketStep");
            model.IsNullStockDown = RequestTool.RequestString("IsNullStockDown");
            model.IsNullStockSale = RequestTool.RequestString("IsNullStockSale");
            model.ProductStockFreezeTime = RequestTool.RequestString("ProductStockFreezeTime");
            model.IsPointToMoney = RequestTool.RequestString("IsPointToMoney");
            model.AdminLanguages = RequestTool.RequestString("AdminLanguages");
            model.OrderReceivedDays = RequestTool.RequestString("OrderReceivedDays");
            model.OrderCompleteDays = RequestTool.RequestString("OrderCompleteDays");
            model.CommentPoint = RequestTool.RequestString("CommentPoint");
            model.APIPassWord = RequestTool.RequestString("APIPassWord");
            model.IsClosetuihuo = RequestTool.RequestString("IsClosetuihuo");
            model.IsSupplierCash = RequestTool.RequestString("IsSupplierCash");
            model.IsOpenUserEnd = RequestTool.RequestString("IsOpenUserEnd");
            model.UserRegCheckedType = RequestTool.RequestString("UserRegCheckedType");
            model.DefaultUserEndDays = RequestTool.RequestString("DefaultUserEndDays");
            model.WithdrawalFeeRate = RequestTool.RequestString("WithdrawalFeeRate");
            model.IsMutiCurrencyShow = RequestTool.RequestString("IsMutiCurrencyShow");
            model.IsMobilePhoneMutiReg = RequestTool.RequestString("IsMobilePhoneMutiReg");
            model.IsAllowOutSideAjax = RequestTool.RequestString("IsAllowOutSideAjax");
            model.SafeIPs = RequestTool.RequestString("SafeIPs");
            model.ProductLimitType = RequestTool.RequestString("ProductLimitType");
            string sp = RequestTool.RequestString("UpLoadPath");
            sp = ThemeUrl.CheckURL("/" + sp).TrimEnd('/');
            model.UpLoadPath = sp;

            //model.IsProductCode = RequestTool.RequestString("IsProductCode");
            model.TopAreaid = RequestTool.RequestString("TopAreaid");
            model.IsAnonymousUser = RequestTool.RequestString("IsAnonymousUser");
            model.ProductNumberPrefix = RequestTool.RequestString("ProductNumberPrefix");
            model.ProductNumberLength = RequestTool.RequestString("ProductNumberLength");
            //退税阶梯函数率BaseConfigStepR
            string Refund_Step_S = RequestTool.RequestString("Refund_Step_S");
            string Refund_Step_R = RequestTool.RequestString("Refund_Step_R");
            List<BaseConfigStepR> steprs = new List<BaseConfigStepR>();
            string[] Step_Ss = Refund_Step_S.Split(',');
            string[] Step_Rs = Refund_Step_R.Split(',');
            for (int i = 0; i < Step_Ss.Length; i++)
            {
                decimal s_Step_S = 0;
                decimal s_Step_R = 0;
                decimal.TryParse(Step_Ss[i], out s_Step_S);
                decimal.TryParse(Step_Rs[i], out s_Step_R);
                BaseConfigStepR stepr = new BaseConfigStepR();
                stepr.S = s_Step_S;
                stepr.R = s_Step_R;
                steprs.Add(stepr);

            }
            if (steprs.Count > 0)
            {
                steprs = steprs.OrderByDescending(a => a.S).ToList();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                model.Refund_StepR = jss.Serialize(steprs);
            }
            model.Refund_MinMoney = RequestTool.RequestString("Refund_MinMoney");
            model.Refund_VAT = RequestTool.RequestString("Refund_VAT");
            model.NewEventTimes = RequestTool.RequestString("NewEventTimes");
            model.NewEventPlayAudio = RequestTool.RequestString("NewEventPlayAudio");
            model.Verifycode_UserRegister = RequestTool.RequestString("Verifycode_UserRegister");
            model.Verifycode_UserLogin = RequestTool.RequestString("Verifycode_UserLogin");
            model.Verifycode_ForgetPassword = RequestTool.RequestString("Verifycode_ForgetPassword");
            model.Verifycode_SupplierRegister = RequestTool.RequestString("Verifycode_SupplierRegister");
            model.Verifycode_SupplierLogin = RequestTool.RequestString("Verifycode_SupplierLogin");
            model.Verifycode_AdminLogin = RequestTool.RequestString("Verifycode_AdminLogin");
            model.HTTPServer = RequestTool.RequestString("HTTPServer");
            model.TaxRate = RequestTool.RequestString("TaxRate");
            model.IntOrderMoney = RequestTool.RequestInt("IntOrderMoney",0).ToString();
            dob.SaveConfig(model);
            ImageHelper.LebiImagesUsed(model.Logoimg, "config", 1);
            Log.Add("编辑基本设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void EmailConfig_Edit()
        {
            if (!EX_Admin.Power("emailconfig_edit", "编辑邮件设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.Email = Language.RequestString("Email");
            model.MailName = RequestTool.RequestString("MailName");
            model.MailSign = RequestTool.RequestString("MailSign");
            string pwd = RequestTool.RequestString("MailPassWord");
            if (pwd != "******")
                model.MailPassWord = pwd;
            model.SmtpAddress = RequestTool.RequestString("SmtpAddress");
            model.MailAddress = RequestTool.RequestString("MailAddress");
            model.MailDisplayName = RequestTool.RequestString("MailDisplayName");
            model.Mail_SendTop = RequestTool.RequestString("Mail_SendTop", "0");
            model.Mail_SendTime = RequestTool.RequestString("Mail_SendTime", "1");
            if (Convert.ToInt32(model.Mail_SendTime) < 1)
                model.Mail_SendTime = "1";
            model.AdminMailAddress = RequestTool.RequestString("AdminMailAddress");
            model.AdminMailSign = RequestTool.RequestString("AdminMailSign");
            model.MailPort = RequestTool.RequestString("MailPort");
            model.MailIsSSL = RequestTool.RequestInt("MailIsSSL").ToString();
            dob.SaveConfig(model);
            //更新队列时间
            TimeWork tw = new TimeWork();
            tw.work_email_restart();
            Log.Add("编辑邮件设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 测试邮件配置
        /// </summary>
        public void EmailTestSend()
        {
            Email.SendEmail_test();
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void PlatformConfig_Edit()
        {
            if (!EX_Admin.Power("platformconfig_edit", "第三方平台设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.platform_login = RequestTool.RequestString("platform_login");
            model.platform_qq_id = RequestTool.RequestString("platform_qq_id");
            model.platform_qq_image = RequestTool.RequestString("platform_qq_image");
            model.platform_qq_key = RequestTool.RequestString("platform_qq_key");
            model.platform_qq_adduser = RequestTool.RequestString("platform_qq_adduser");
            model.platform_weibo_id = RequestTool.RequestString("platform_weibo_id");
            model.platform_weibo_image = RequestTool.RequestString("platform_weibo_image");
            model.platform_weibo_key = RequestTool.RequestString("platform_weibo_key");
            model.platform_weibo_adduser = RequestTool.RequestString("platform_weibo_adduser");
            model.platform_taobao_secret = RequestTool.RequestString("platform_taobao_secret");
            model.platform_taobao_key = RequestTool.RequestString("platform_taobao_key");
            model.platform_taobao_image = RequestTool.RequestString("platform_taobao_image");
            model.platform_taobao_adduser = RequestTool.RequestString("platform_taobao_adduser");
            model.platform_facebook_image = RequestTool.RequestString("platform_facebook_image");
            model.platform_facebook_id = RequestTool.RequestString("platform_facebook_id");
            model.platform_facebook_secret = RequestTool.RequestString("platform_facebook_secret");
            model.platform_facebook_adduser = RequestTool.RequestString("platform_facebook_adduser");
            model.platform_twitter_secret = RequestTool.RequestString("platform_twitter_secret");
            model.platform_twitter_key = RequestTool.RequestString("platform_twitter_key");
            model.platform_twitter_image = RequestTool.RequestString("platform_twitter_image");
            model.platform_twitter_adduser = RequestTool.RequestString("platform_twitter_adduser");
            model.platform_weixin_id = RequestTool.RequestString("platform_weixin_id");
            model.platform_weixin_secret = RequestTool.RequestString("platform_weixin_secret");
            model.platform_weixin_custemtoken = RequestTool.RequestString("platform_weixin_custemtoken");
            model.platform_weixin_image = RequestTool.RequestString("platform_weixin_image");
            model.platform_weixin_image_qrcode = RequestTool.RequestString("platform_weixin_image_qrcode");
            model.platform_weixin_number = RequestTool.RequestString("platform_weixin_number");
            model.platform_weixin_subscribe_automsg = RequestTool.RequestString("platform_weixin_subscribe_automsg");
            model.platform_weixin_adduser = RequestTool.RequestString("platform_weixin_adduser");
            dob.SaveConfig(model);
            ImageHelper.LebiImagesUsed(model.platform_qq_image + "@" + model.platform_weibo_image + "@" + model.platform_taobao_image + "@" + model.platform_facebook_image + "@" + model.platform_twitter_image, "config", 1);
            Log.Add("第三方平台设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑邮件模板
        /// </summary>
        public void EmailTPL_Edit()
        {
            if (!EX_Admin.Power("theme_email_edit", "编辑邮件模板"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            string type = RequestTool.RequestString("type");
            string content = Language.RequestString("Content");
            string title = Language.RequestString("Title");
            switch (type)
            {
                case "EmailTPL_getpwd":
                    model.EmailTPL_getpwd = content;
                    model.EmailTPL_getpwd_title = title;
                    break;
                case "EmailTPL_newuser":
                    model.EmailTPL_newuser = content;
                    model.EmailTPL_newuser_title = title;
                    break;
                case "EmailTPL_ordershipping":
                    model.EmailTPL_ordershipping = content;
                    model.EmailTPL_ordershipping_title = title;
                    break;
                case "EmailTPL_ordersubmit":
                    model.EmailTPL_ordersubmit = content;
                    model.EmailTPL_ordersubmit_title = title;
                    break;
                case "EmailTPL_orderpaid":
                    model.EmailTPL_orderpaid = content;
                    model.EmailTPL_orderpaid_title = title;
                    break;
                case "EmailTPL_Admin_newuser":
                    model.EmailTPL_Admin_newuser = content;
                    model.EmailTPL_Admin_newuser_title = title;
                    break;
                case "EmailTPL_Admin_ordersubmit":
                    model.EmailTPL_Admin_ordersubmit = content;
                    model.EmailTPL_Admin_ordersubmit_title = title;
                    break;
                case "EmailTPL_Admin_orderpaid":
                    model.EmailTPL_Admin_orderpaid = content;
                    model.EmailTPL_Admin_orderpaid_title = title;
                    break;
                case "EmailTPL_Admin_ordercomment":
                    model.EmailTPL_Admin_ordercomment = content;
                    model.EmailTPL_Admin_ordercomment_title = title;
                    break;
                case "EmailTPL_Admin_inquiry":
                    model.EmailTPL_Admin_inquiry = content;
                    model.EmailTPL_Admin_inquiry_title = title;
                    break;
                case "EmailTPL_Admin_comment":
                    model.EmailTPL_Admin_comment = content;
                    model.EmailTPL_Admin_comment_title = title;
                    break;
                case "EmailTPL_Admin_ask":
                    model.EmailTPL_Admin_ask = content;
                    model.EmailTPL_Admin_ask_title = title;
                    break;
                case "EmailTPL_Admin_message":
                    model.EmailTPL_Admin_message = content;
                    model.EmailTPL_Admin_message_title = title;
                    break;
                case "EmailTPL_changgouqingdan":
                    model.EmailTPL_changgouqingdan = content;
                    model.EmailTPL_changgouqingdan_title = title;
                    break;
                case "EmailTPL_checkcode":
                    model.EmailTPL_checkcode = content;
                    model.EmailTPL_checkcode_title = title;
                    break;
                case "EmailTPL_sendfriend":
                    model.EmailTPL_sendfriend = content;
                    model.EmailTPL_sendfriend_title = title;
                    break;
                case "EmailTPL_reserveok":
                    model.EmailTPL_reserveok = content;
                    model.EmailTPL_reserveok_title = title;
                    break;
                default:
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
            }
            dob.SaveConfig(model);
            Log.Add("编辑邮件模板", "Config", "", CurrentAdmin, type);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 保存网站维护信息
        /// </summary>
        /// <returns></returns>
        public void Defend_Edit()
        {
            if (!EX_Admin.Power("defend_edit", "网站维护"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.WeiHuFlag = RequestTool.RequestString("WeiHuFlag");
            model.VisitTime = RequestTool.RequestString("VisitTime");
            model.VisitTimeFlag = RequestTool.RequestString("VisitTimeFlag");
            model.Wornings = RequestTool.RequestString("Wornings");
            dob.SaveConfig(model);
            Log.Add("编辑网站维护", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 快递100API配置
        /// </summary>
        /// <returns></returns>
        public void Kuaidi100_Edit()
        {
            if (!EX_Admin.Power("transport_kuaidi100_edit", "快递100配置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.KuaiDi100 = RequestTool.RequestString("KuaiDi100");
            model.KuaiDi100Status = RequestTool.RequestString("KuaiDi100Status");
            dob.SaveConfig(model);
            Log.Add("编辑快递100", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 保存水印配置信息
        /// </summary>
        /// <returns></returns>
        public void WaterConfig_Edit()
        {
            if (!EX_Admin.Power("watermark_edit", "水印设置"))
            {
                AjaxNoPower();
                return;
            }
            B_WaterConfig dob = new B_WaterConfig();

            WaterConfig model = new WaterConfig();
            model.OnAndOff = RequestTool.RequestString("OnAndOff");

            model.WM_Font = RequestTool.RequestString("WM_Font");
            model.WM_FontColor = RequestTool.RequestString("WM_FontColor");
            model.WM_FontForm = RequestTool.RequestString("WM_FontForm");
            model.WM_FontSize = RequestTool.RequestString("WM_FontSize");

            model.PicAndText = RequestTool.RequestString("PicAndText");
            model.WM_PicPath = RequestTool.RequestString("WM_PicPath");

            model.WM_PlaceX = RequestTool.RequestString("WM_PlaceX");
            model.WM_PlaceY = RequestTool.RequestString("WM_PlaceY");



            model.WM_Text = RequestTool.RequestString("WM_Text");
            model.WM_Transparence = RequestTool.RequestString("WM_Transparence");
            model.WM_Width = RequestTool.RequestString("WM_Width");
            model.WM_Height = RequestTool.RequestString("WM_Height");
            model.WM_Location = RequestTool.RequestString("WM_Location");
            dob.SaveConfig(model);
            Log.Add("编辑水印设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 保存导航内容
        /// </summary>
        public void Tab_Edit()
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Tab model = B_Lebi_Tab.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Tab();
            }
            model = B_Lebi_Tab.BindForm(model);
            model.Tname = Language.RequestString("Tname");
            model.Title = Language.RequestString("Title");
            model.Tkey = Language.RequestString("Tkey");
            model.Tdes = Language.RequestString("Tdes");
            model.Url = Language.RequestString("Url");
            model.Description = Language.RequestString("Description");
            if (addflag)
            {
                B_Lebi_Tab.Add(model);
                id = B_Lebi_Tab.GetMaxId();
                string description = Shop.Bussiness.Language.Content(Language.RequestString("TName"), "CN");
                Log.Add("添加导航", "Tab", id.ToString(), CurrentAdmin, description);
            }
            else
            {
                B_Lebi_Tab.Update(model);
                string description = Shop.Bussiness.Language.Content(Language.RequestString("TName"), "CN");
                Log.Add("编辑导航", "Tab", id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\", \"id\":\"" + id + "\"}");

        }
        /// <summary>
        /// 删除导航内容
        /// </summary>
        public void Tab_Del()
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Tab.Delete("id in (lbsql{" + id + "})");
            string description = id;
            Log.Add("删除导航", "Tab", id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 添加类别的商品分类
        /// </summary>
        public void Tab_Child_Add()
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                AjaxNoPower();
                return;
            }
            int tabid = RequestTool.RequestInt("tabid", 0);
            string ids = RequestTool.RequestString("ids");
            Lebi_Tab tab = B_Lebi_Tab.GetModel(tabid);
            if (tab == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            Lebi_TabChild child;
            List<Lebi_Pro_Type> models = B_Lebi_Pro_Type.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Pro_Type model in models)
            {
                child = B_Lebi_TabChild.GetModel("protypeid=" + model.id + " and tabid=" + tab.id + "");
                if (child == null)
                {
                    child = new Lebi_TabChild();
                    child.protypeid = model.id;
                    child.tabid = tab.id;
                    B_Lebi_TabChild.Add(child);
                }
            }
            string description = ids;
            Log.Add("添加导航类别", "TabChild", ids.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑类别的商品分类
        /// </summary>
        public void Tab_Child_Edit()
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                AjaxNoPower();
                return;
            }
            int tabid = RequestTool.RequestInt("tabid", 0);
            Lebi_Tab tab = B_Lebi_Tab.GetModel(tabid);
            if (tab == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            List<Lebi_TabChild> models = B_Lebi_TabChild.GetList("tabid=" + tab.id + "", "");
            foreach (Lebi_TabChild model in models)
            {
                model.sort = RequestTool.RequestInt("txt_sort_" + model.id + "", 0);
                model.num = RequestTool.RequestInt("txt_num_" + model.id + "", 0);
                B_Lebi_TabChild.Update(model);
            }
            string description = tabid.ToString();
            Log.Add("编辑导航类别", "TabChild", tabid.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除类别的商品分类
        /// </summary>
        public void Tab_Child_Del()
        {
            if (!EX_Admin.Power("tab_edit", "导航设置"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("typeid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            B_Lebi_TabChild.Delete("id in (lbsql{" + id + "})");
            string description = id.ToString();
            Log.Add("删除导航类别", "TabChild", id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑友情链接
        /// </summary>
        public void FriendLink_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_FriendLink model = B_Lebi_FriendLink.GetModel(id);

            if (model == null)
            {
                addflag = true;
                model = new Lebi_FriendLink();
            }
            model = B_Lebi_FriendLink.BindForm(model);
            model.Language = Language.LanuageidsToCodes(model.Language_ids);
            if (addflag)
            {
                if (!EX_Admin.Power("friendlink_add", "添加友情链接"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_FriendLink.Add(model);
                id = B_Lebi_FriendLink.GetMaxId();
                Log.Add("添加友情链接", "FriendLink", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("friendlink_edit", "编辑友情链接"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_FriendLink.Update(model);
                Log.Add("编辑友情链接", "FriendLink", id.ToString(), CurrentAdmin, model.Name);
            }
            //====================================
            //处理静态页
            ImageHelper.LebiImagesUsed(model.Logo, "friendlink", id);
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_FriendLink'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_OnePage(themepage);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量编辑友情链接
        /// </summary>
        public void FriendLinks_Edit()
        {
            if (!EX_Admin.Power("friendlink_edit", "编辑友情链接"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestString("IDS");
            List<Lebi_FriendLink> models = B_Lebi_FriendLink.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_FriendLink model in models)
            {
                model.Name = RequestTool.RequestString("Name" + model.id);
                model.Url = RequestTool.RequestString("Url" + model.id);
                model.Sort = RequestTool.RequestInt("Sort" + model.id, 0);
                model.IsPic = RequestTool.RequestInt("IsPic" + model.id, 0);
                model.IsShow = RequestTool.RequestInt("IsShow" + model.id, 0);
                B_Lebi_FriendLink.Update(model);
            }
            string description = ids.ToString();
            //处理静态页
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_FriendLink'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_OnePage(themepage);
            Log.Add("编辑友情链接", "FriendLink", ids.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量删除友情链接
        /// </summary>
        public void FriendLink_Del()
        {
            if (!EX_Admin.Power("friendlink_del", "删除友情链接"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_FriendLink.Delete("id in (lbsql{" + id + "})");
            string description = id.ToString();
            //处理图片
            ImageHelper.LebiImagesDelete("friendlink", id);
            //处理静态页
            Lebi_Theme_Page themepage = B_Lebi_Theme_Page.GetModel("Code='P_FriendLink'");
            if (themepage.Type_id_PublishType == 122)
                PageStatic.Greate_OnePage(themepage);
            Log.Add("删除友情链接", "FriendLink", id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑关键词
        /// </summary>
        public void SearchKey_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Searchkey model = B_Lebi_Searchkey.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Searchkey();
            }
            model = B_Lebi_Searchkey.BindForm(model);
            model.Name = Language.RequestString("Name");

            if (addflag)
            {
                if (!EX_Admin.Power("searchkey_add", "添加关键词"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Searchkey.Add(model);
                id = B_Lebi_Searchkey.GetMaxId();
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add("添加关键词", "Searchkey", id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("searchkey_edit", "编辑关键词"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Searchkey.Update(model);
                string description = Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN");
                Log.Add("编辑关键词", "Searchkey", id.ToString(), CurrentAdmin, description);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新关键词排序
        /// </summary>
        public void SearchKey_Update()
        {
            if (!EX_Admin.Power("searchkey_edit", "编辑关键词"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Uid");
            List<Lebi_Searchkey> models = B_Lebi_Searchkey.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Searchkey model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                B_Lebi_Searchkey.Update(model);
            }
            string description = id.ToString();
            Log.Add("编辑关键词", "Searchkey", id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量删除关键词
        /// </summary>
        public void SearchKey_Del()
        {
            if (!EX_Admin.Power("searchkey_del", "删除关键词"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Searchkey.Delete("id in (lbsql{" + id + "})");
            string description = id.ToString();
            Log.Add("删除关键词", "Searchkey", id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑IP锁定
        /// </summary>
        public void IpBlock_Edit()
        {
            if (!EX_Admin.Power("ipblock_edit", "IP锁定"))
            {
                AjaxNoPower();
                return;
            }
            string ip = RequestTool.RequestString("ip");
            ip = ip.Replace("\r\n", "\n");
            ip = ip.Replace("\n", ",");
            BaseConfig bc = new BaseConfig();
            bc.IPLock = ip;
            B_BaseConfig dob = new B_BaseConfig();
            dob.SaveConfig(bc);
            Log.Add("编辑IP锁定", "IpBlock", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑站点信息
        /// </summary>
        public void Site_Edit()
        {
            if (!EX_Admin.Power("language_edit", "编辑网站语言"))
            {
                AjaxNoPower();
                return;
            }
            int SiteCount = B_Lebi_Site.Counts("1=1");
            int sitenum = Shop.Bussiness.Site.Instance.SiteCount;
            if (SiteCount > sitenum)
            {
                Response.Write("{\"msg\":\"" + Tag("站点数量异常") + "\"}");
                return;
            }
            Lebi_Site model = new Lebi_Site();
            int id = RequestTool.RequestInt("id", 0);
            if (id > 0)
            {
                string where = "";
                if (domain3admin && CurrentAdmin.Site_ids != "")
                {
                    where = " and id in (" + CurrentAdmin.Site_ids + ")";
                }
                model = B_Lebi_Site.GetModel("id = " + id + "" + where + "");
                if (model == null)
                {
                    Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                    return;
                }
            }
            List<Lebi_Language_Code> langcodes = B_Lebi_Language_Code.GetList("Code in (select Code from Lebi_Language where Site_id=" + model.id + ")", "Code asc");
            model = B_Lebi_Site.BindForm(model);
            model.Copyright = Language.RequestString("Copyright", langcodes);
            model.Description = Language.RequestString("Description", langcodes);
            model.Domain = RequestTool.RequestString("Domain");
            model.Email = Language.RequestString("Email", langcodes);
            model.Fax = Language.RequestString("Fax", langcodes);
            model.Keywords = Language.RequestString("Keywords", langcodes);
            model.Logoimg = Language.RequestString("Logoimg", langcodes);
            model.Phone = Language.RequestString("Phone", langcodes);
            model.QQ = Language.RequestString("QQ", langcodes);
            model.ServiceP = Language.RequestString("ServiceP", langcodes);
            model.Name = Language.RequestString("Name", langcodes);
            model.Title = Language.RequestString("Title", langcodes);
            model.FootHtml = Language.RequestString("FootHtml", langcodes);
            model.Sort = RequestTool.RequestInt("Sort");
            model.Path = "/" + RequestTool.RequestString("Path").Trim('/');
            model.IsMobile = RequestTool.RequestInt("IsMobile", 0);
            model.SubName = RequestTool.RequestString("SubName");
            if (id == 0)
            {
                B_Lebi_Site.Add(model);
                id = B_Lebi_Site.GetMaxId();
            }
            else
            {
                B_Lebi_Site.Update(model);
            }
            string description = model.Name;
            List<Lebi_Language> langs = B_Lebi_Language.GetList("Site_id=" + id + "", "");
            foreach (Lebi_Language lang in langs)
            {
                lang.Path = "/" + RequestTool.RequestString("Path" + lang.id).Trim('/');
                lang.Name = RequestTool.RequestString("Name" + lang.id);
                lang.Theme_id = RequestTool.RequestInt("Theme_id" + lang.id);
                lang.ImageUrl = RequestTool.RequestString("ImageUrl" + lang.id);
                //lang.Path = ThemeUrl.CheckURL(lang.Path).TrimEnd('/');
                lang.Currency_id = RequestTool.RequestInt("Currency_id" + lang.id, 0);
                lang.TopAreaid = RequestTool.RequestInt("TopAreaid" + lang.id);
                lang.IsLazyLoad = RequestTool.RequestInt("IsLazyLoad" + lang.id, 0);
                B_Lebi_Language.Update(lang);
                ImageHelper.LebiImagesUsed(lang.ImageUrl, "config", id);
            }
            if (domain3admin)
            {
                if (model.Path != "" && model.Path != "/" && model.Domain == "" && ShopCache.GetMainSite().id != id)
                {
                    //未使用域名的附属站点删除web.config
                    string config = HttpContext.Current.Server.MapPath(@"~/" + model.Path + "/web.config");
                    if (File.Exists(config))
                    {
                        File.Delete(config);
                    }
                }
                if (model.Path != "" && model.Path != "/" && model.Domain.Length > 3 && model.Domain.Contains(".") && ShopCache.GetMainSite().id != id)
                {
                    //附属站点使用了域名处理web.config和bin
                    copyfiles("bin", model.Path);
                    copyfiles("ajax", model.Path);
                    copyfiles("inc", model.Path);
                    copyfiles("agent", model.Path);
                    copyfiles("supplier", model.Path);
                    copyfiles("api", model.Path);
                    copyfiles("onlinepay", model.Path);
                    copyfile("web.config", model.Path);
                    copyfile("code.aspx", model.Path);
                }
            }
            Log.Add("编辑网站语言", "Language", id.ToString(), CurrentAdmin, description);

            ShopCache.SetMainSite();//更新主站点缓存
            //检查主站的路径
            if (ShopCache.GetMainSite().Path == "" || ShopCache.GetMainSite().Path == "/")
            {
                Response.Write("{\"msg\":\"OK\"}");
            }
            else
            {
                Response.Write("{\"msg\":\"主站路径必须为 / \"}");
            }
        }
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="foldername"></param>
        private void copyfiles(string foldername, string topath)
        {
            string varFromDirectory = HttpContext.Current.Server.MapPath(@"~/" + foldername);
            string varToDirectory = HttpContext.Current.Server.MapPath(@"~/" + topath + "/" + foldername);
            FileTool.CopyFiles(varFromDirectory, varToDirectory, true);
        }
        private void copyfile(string name, string topath)
        {
            string formconfig = HttpContext.Current.Server.MapPath(@"~/" + name);
            string toconfig = HttpContext.Current.Server.MapPath(@"~/" + topath + "/" + name);
            FileTool.CopyFile(formconfig, toconfig, true);
        }
        /// <summary>
        /// 删除站点语言
        /// </summary>
        public void SiteLanguage_Del()
        {
            if (!EX_Admin.Power("language_del", "删除网站语言"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Language model = B_Lebi_Language.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            string description = model.Name;
            Log.Add("删除网站语言", "Language", id.ToString(), CurrentAdmin, description);
            B_Lebi_Language.Delete(id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除站点
        /// </summary>
        public void Site_Del()
        {
            if (!EX_Admin.Power("site_del", "删除站点"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Site model = B_Lebi_Site.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            string description = model.Name;
            Log.Add("删除站点", "Site", id.ToString(), CurrentAdmin, description);
            B_Lebi_Site.Delete(id);
            B_Lebi_Language.Delete("Site_id=" + id + "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 添加一个站点
        /// </summary>
        public void AddSite()
        {
            if (!EX_Admin.Power("site_add", "添加站点"))
            {
                AjaxNoPower();
                return;
            }
            int SiteCount = B_Lebi_Site.Counts("1=1");
            int sitenum = Shop.Bussiness.Site.Instance.SiteCount;
            if (SiteCount > sitenum)
            {
                Response.Write("{\"msg\":\"" + Tag("站点数量异常") + "\"}");
                return;
            }
            Lebi_Site model = new Lebi_Site();
            model.SubName = Tag("新站点");
            B_Lebi_Site.Add(model);
            model.id = B_Lebi_Site.GetMaxId();
            //if (CurrentAdmin.Site_ids == "")
            //    CurrentAdmin.Site_ids = model.id.ToString();
            //else
            //    CurrentAdmin.Site_ids = CurrentAdmin.Site_ids + "," + model.id;
            //B_Lebi_Administrator.Update(CurrentAdmin);
            //EX_Admin.SetSession(CurrentAdmin);
            Log.Add("添加站点", "Site", model.id.ToString(), CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + model.id + "\"}");
        }
        /// <summary>
        /// 添加一个语种
        /// </summary>
        public void SiteLanguage_Add()
        {
            if (!EX_Admin.Power("language_add", "添加网站语言"))
            {
                AjaxNoPower();
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Site model = B_Lebi_Site.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            string languagecode = RequestTool.RequestString("languagecode");
            Lebi_Language_Code code = B_Lebi_Language_Code.GetModel("Code=lbsql{'" + languagecode + "'}");
            if (code == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            //Lebi_Language lang = B_Lebi_Language.GetModel("Code=lbsql{'" + languagecode + "'} and Site_id=" + id + "");
            Lebi_Language lang = new Lebi_Language();
            //if (lang == null)
            //{
            lang = new Lebi_Language();
            lang.Code = code.Code;
            lang.Name = code.Name;
            lang.Site_id = id;
            B_Lebi_Language.Add(lang);
            Response.Write("{\"msg\":\"OK\"}");
            //}
            //else
            //{
            //    Response.Write("{\"msg\":\"" + Tag("不能重复添加") + "\"}");
            //}
        }
        /// <summary>
        /// 批量删除地名
        /// </summary>
        public void Area_Del()
        {
            if (!EX_Admin.Power("area_del", "删除地名"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Area.Delete("id in (lbsql{" + id + "})");
            string description = id.ToString();
            Log.Add("删除地名", "Area", id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑地名
        /// </summary>
        public void Area_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Area model = B_Lebi_Area.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Area();
            }
            model = B_Lebi_Area.BindForm(model);
            if (addflag)
            {
                if (!EX_Admin.Power("area_add", "添加地名"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Area.Add(model);
                id = B_Lebi_Area.GetMaxId();
                string description = model.Name;
                Log.Add("添加地名", "Area", id.ToString(), CurrentAdmin, description);
            }
            else
            {
                if (!EX_Admin.Power("area_edit", "编辑地名"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Area.Update(model);
                Log.Add("编辑地名", "Area", id.ToString(), CurrentAdmin, model.Name);
            }
            //string result = "{error:'OK', id:'" + id + "'}";
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新地名排序
        /// </summary>
        public void Area_Update()
        {
            if (!EX_Admin.Power("area_edit", "编辑地名"))
            {
                AjaxNoPower();
                return;
            }
            int pid = RequestTool.RequestInt("pid", 0);
            string id = RequestTool.RequestString("IDS");
            List<Lebi_Area> models = B_Lebi_Area.GetList("id in (lbsql{" + id + "}) and Parentid=" + pid, "");
            foreach (Lebi_Area model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                B_Lebi_Area.Update(model);
            }
            Log.Add("更新地名排序", "Area", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除配送方式
        /// </summary>
        public void Transport_Del()
        {
            if (!EX_Admin.Power("transport_del", "删除配送方式"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Transport.Delete("id in (lbsql{" + id + "})");
            B_Lebi_Transport_Price.Delete("Transport_id in (lbsql{" + id + "})");
            Log.Add("删除配送方式", "Transport", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑运输公司
        /// </summary>
        public void Transport_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Transport model = B_Lebi_Transport.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Transport();
            }
            model = B_Lebi_Transport.BindForm(model);
            if (model.Type_id_TransportType == 332)
            {
                int count = B_Lebi_Transport.Counts("id!=" + model.id + " and Type_id_TransportType=332");
                if (count > 0)
                {
                    Response.Write("{\"msg\":\"" + Tag("自提已经存在") + "\"}");
                    return;
                }
            }
            if (addflag)
            {
                if (!EX_Admin.Power("transport_add", "添加配送方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Transport.Add(model);
                id = B_Lebi_Transport.GetMaxId();
                Log.Add("添加配送方式", "Transport", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("transport_edit", "编辑配送方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Transport.Update(model);
                Log.Add("编辑配送方式", "Transport", id.ToString(), CurrentAdmin, model.Name);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑配送区域价格
        /// </summary>
        public void Transport_Price_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            int tid = RequestTool.RequestInt("tid", 0);
            Lebi_Transport tmodel = B_Lebi_Transport.GetModel(tid);
            Lebi_Transport_Price model = B_Lebi_Transport_Price.GetModel(id);
            string ContainerPrice = "";
            if (tmodel.Type_id_TransportType == 331)//货柜
            {
                string cids = RequestTool.RequestString("Containerid");
                if (cids != "")
                {
                    List<Lebi_Transport_Container> tcs = B_Lebi_Transport_Container.GetList("id in (lbsql{" + cids + "})", "");
                    List<KeyValue> kvs = new List<KeyValue>();
                    foreach (Lebi_Transport_Container tc in tcs)
                    {
                        KeyValue kv = new KeyValue();
                        kv.K = tc.id.ToString();
                        kv.V = RequestTool.RequestDecimal("ContainerPrice" + tc.id, 0).ToString();
                        kvs.Add(kv);
                    }
                    JavaScriptSerializer jss = new JavaScriptSerializer();
                    ContainerPrice = jss.Serialize(kvs);
                }
            }
            if (model != null)
            {
                if (!EX_Admin.Power("transport_price_edit", "编辑配送区域"))
                {
                    AjaxNoPower();
                    return;
                }
                int Area_id = model.Area_id;
                B_Lebi_Transport_Price.BindForm(model);
                model.Transport_id = tmodel.id;
                model.Area_id = Area_id;
                model.Container = ContainerPrice;
                B_Lebi_Transport_Price.Update(model);
                Log.Add("编辑配送区域", "Transport_Price", id.ToString(), CurrentAdmin, tmodel.Name);
            }
            else
            {
                if (!EX_Admin.Power("transport_price_add", "添加配送区域价格"))
                {
                    AjaxNoPower();
                    return;
                }
                string aids = RequestTool.RequestString("Area_ids");
                if (aids == "")
                    aids = "0";
                List<Lebi_Area> areas = B_Lebi_Area.GetList("id in (lbsql{" + aids + "})", "");
                model = new Lebi_Transport_Price();
                foreach (Lebi_Area area in areas)
                {
                    //避免重复添加
                    int count = B_Lebi_Transport_Price.Counts("Transport_id=" + tmodel.id + " and Area_id=" + area.id + " and Supplier_id = 0");
                    if (count > 0)
                        continue;
                    B_Lebi_Transport_Price.BindForm(model);
                    model.Area_id = area.id;
                    model.Transport_id = tmodel.id;
                    model.Container = ContainerPrice;
                    B_Lebi_Transport_Price.Add(model);
                }
                Log.Add("添加配送区域", "Transport_Price", id.ToString(), CurrentAdmin, tmodel.Name);
            }
            Response.Write("{\"msg\":\"OK\"}");

        }
        /// <summary>
        /// 批量更新配送区域价格
        /// </summary>
        public void Transport_Price_Update()
        {
            if (!EX_Admin.Power("transport_price_edit", "编辑配送区域"))
            {
                AjaxNoPower();
                return;
            }
            int tid = RequestTool.RequestInt("tid", 0);
            string id = RequestTool.RequestString("Uid");
            Lebi_Transport tmodel = B_Lebi_Transport.GetModel(tid);
            List<Lebi_Transport_Price> models = B_Lebi_Transport_Price.GetList("id in (lbsql{" + id + "}) and Transport_id=" + tid, "");
            foreach (Lebi_Transport_Price model in models)
            {
                model.Price = RequestTool.GetFormDecimal("Price" + model.id + "", 0);
                model.Weight_Start = RequestTool.GetFormDecimal("Weight_Start" + model.id + "", 0);
                model.Weight_Step = RequestTool.GetFormDecimal("Weight_Step" + model.id + "", 0);
                model.Price_Step = RequestTool.GetFormDecimal("Price_Step" + model.id + "", 0);
                B_Lebi_Transport_Price.Update(model);
            }
            Log.Add("编辑配送区域", "Transport_Price", id.ToString(), CurrentAdmin, tmodel.Name);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除区域配送定价
        /// </summary>
        public void Transport_Price_Del()
        {
            if (!EX_Admin.Power("transport_price_del", "删除配送区域价格"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Transport_Price.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除配送区域", "Transport_Price", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除配送方式-货柜
        /// </summary>
        public void Transport_Container_Del()
        {
            if (!EX_Admin.Power("transport_del", "删除配送方式"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Transport_Container.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除货柜", "Transport_Container", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑货柜
        /// </summary>
        public void Transport_Container_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Transport_Container model = B_Lebi_Transport_Container.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Transport_Container();
            }
            model = B_Lebi_Transport_Container.BindForm(model);
            if (addflag)
            {
                if (!EX_Admin.Power("transport_add", "添加配送方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Transport_Container.Add(model);
                id = B_Lebi_Transport_Container.GetMaxId();
                Log.Add("添加货柜", "Transport_Container", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("transport_edit", "编辑配送方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Transport_Container.Update(model);
                Log.Add("编辑货柜", "Transport_Container", id.ToString(), CurrentAdmin, model.Name);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// SQL命令执行
        /// </summary>
        public void SQL_Edit()
        {
            if (!EX_Admin.Power("sql_edit", "命令执行"))
            {
                AjaxNoPower();
                return;
            }
            string sql = Request["sql"].Trim().ToLower();
            if (sql == null)
                sql = "";
            sql = sql.Replace("delete", "").Replace("exec", "").Replace("drop", "").Replace("create", "").Replace("truncate", "");
            Log.Add("命令执行", "", "", CurrentAdmin, sql);
            try
            {
                if (sql.IndexOf("lebi_log") > -1 || sql.IndexOf("lebi_administrator") > -1)
                {
                    return;
                }
                if (sql.IndexOf("select") == 0)
                {
                    StringBuilder sb = new StringBuilder();
                    DataSet ds;

                    ds = Bussiness.Common.GetDataSet(sql);

                    DataTable dt = ds.Tables[0];
                    sb.Append(Tag("操作成功，受影响的行数：") + dt.Rows.Count);
                    sb.Append("<table  class=\"data\"><tr>");
                    foreach (DataColumn col in dt.Columns)
                    {
                        sb.Append("<td>" + col.ColumnName + "</td>");
                    }
                    sb.Append("</tr><tr>");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr>");
                        foreach (DataColumn col in dt.Columns)
                        {
                            sb.Append("<td>" + dt.Rows[i][col.ColumnName] + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    Response.Write(sb.ToString());
                }
                else
                {
                    int res = Bussiness.Common.ExecuteSql(sql);
                    Response.Write(Tag("操作成功，受影响的行数：") + res + "}");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return;
            }
        }
        /// <summary>
        /// CNZZ统计配置
        /// </summary>
        public void CnzzConfig_Edit()
        {
            if (!EX_Admin.Power("cnzz_edit", "CNZZ统计"))
            {
                AjaxNoPower();
                return;
            }
            int r1 = RequestTool.RequestInt("r1", 0);
            int r2 = RequestTool.RequestInt("r2", 0);
            Lebi_Cnzz model = B_Lebi_Cnzz.GetModel(1);
            if (model == null)
            {
                model = new Lebi_Cnzz();
                model.state = r1;
                B_Lebi_Cnzz.Add(model);
                Log.Add("CNZZ统计开通", "Cnzz", "", CurrentAdmin, "");
            }
            else
            {
                if (r2 == 1)
                {
                    string domain = HttpContext.Current.Request.Url.Host;
                    string key = "Jsit7Kd3";
                    string cms = "56770";
                    key = this.GetAspMd5(domain + key, 32);
                    string str = "http://intf.cnzz.com/user/companion/56770.php?domain=" + domain + "&key=" + key + "&cms=" + cms;
                    string userpwd = getStr(str);
                    if (userpwd.Length > 3)
                    {
                        model.Ccontent = userpwd;
                        model.state = 0;
                        B_Lebi_Cnzz.Update(model);
                        //bool b = bll.update(model);
                        //if (b)
                        //{
                        Log.Add("CNZZ统计开通", "Cnzz", "", CurrentAdmin, "重新开通");
                        Response.Write("{\"msg\":\"OK\"}");
                        //}
                        //else
                        //{
                        //    Response.Write("重新开通失败");
                        //    return;
                        //}
                    }
                }
                else
                {
                    model.state = r1;
                    B_Lebi_Cnzz.Update(model);
                    Log.Add("CNZZ统计配置", "Cnzz", "", CurrentAdmin, "");
                    Response.Write("{\"msg\":\"OK\"}");
                }
            }
        }
        public string GetAspMd5(string md5str, int type)
        {
            if (type == 16)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5str, "MD5").Substring(8, 16).ToLower();
            }
            else if (type == 32)
            {
                return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(md5str, "MD5").ToLower();
            }
            return "";
        }
        public string getStr(string url)
        {
            //
            // TODO: 在此处添加代码以启动应用程序
            //
            WebClient wc = new WebClient();
            byte[] response = wc.DownloadData(url);
            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("gb2312");
            string cc = encoding.GetString(response);
            return cc;
        }
        /// <summary>
        /// 编辑在线支付信息
        /// </summary>
        public void OnlinePay_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            int FreeFeeRate = RequestTool.RequestInt("FreeFeeRate");
            bool addflag = false;
            Lebi_OnlinePay model = B_Lebi_OnlinePay.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_OnlinePay();
            }
            model = B_Lebi_OnlinePay.BindForm(model);
            model.Name = Language.RequestString("Name");
            model.Description = Language.RequestString("Description");
            int Currency_id = RequestTool.RequestInt("Currency_id");
            Lebi_Currency currency = B_Lebi_Currency.GetModel(Currency_id);
            if (currency == null)
                currency = new Lebi_Currency();
            model.Currency_id = currency.id;
            model.Currency_Name = currency.Name;
            model.Currency_Code = currency.Code;
            model.showtype = RequestTool.RequestString("showtype");
            model.Language_ids = RequestTool.RequestString("Language_ids");
            model.FreeFeeRate = FreeFeeRate;
            if (addflag)
            {
                if (!EX_Admin.Power("OnlinePay", "添加在线支付"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_OnlinePay.Add(model);
                id = B_Lebi_OnlinePay.GetMaxId();
                Log.Add("添加在线支付", "OnlinePay", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            else
            {
                if (!EX_Admin.Power("onlinepay_edit", "编辑在线支付"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_OnlinePay.Update(model);
                Log.Add("编辑在线支付", "OnlinePay", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            ImageHelper.LebiImagesUsed(model.Logo, "onlinepay", id);
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除在线支付信息
        /// </summary>
        public void OnlinePay_Del()
        {
            if (!EX_Admin.Power("onlinepay_del", "删除在线支付"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Delid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_OnlinePay.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除在线支付", "OnlinePay", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 复制在线支付信息
        /// </summary>
        public void OnlinePay_Copy()
        {
            if (!EX_Admin.Power("OnlinePay", "添加在线支付"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_OnlinePay oldmodel = B_Lebi_OnlinePay.GetModel(id);
            Lebi_OnlinePay model = new Lebi_OnlinePay();
            model.Code = oldmodel.Code;
            model.Currency_Code = oldmodel.Currency_Code;
            model.Currency_id = oldmodel.Currency_id;
            model.Currency_Name = oldmodel.Currency_Name;
            model.Description = oldmodel.Description;
            model.Email = oldmodel.Email;
            model.ExchangeRate = oldmodel.ExchangeRate;
            model.FeeRate = oldmodel.FeeRate;
            model.FreeFeeRate = oldmodel.FreeFeeRate;
            model.Language_ids = "";
            model.Logo = oldmodel.Logo;
            model.Name = oldmodel.Name;
            model.parentid = 0;
            model.showtype = oldmodel.showtype;
            model.Supplier_id = 0;
            model.terminal = oldmodel.terminal;
            model.TypeName = oldmodel.TypeName;
            model.Url = oldmodel.Url;
            model.UserKey = oldmodel.UserKey;
            model.UserName = oldmodel.UserName;
            model.UserRealName = oldmodel.UserRealName;
            model.Remark = "复制";
            B_Lebi_OnlinePay.Add(model);
            id = B_Lebi_OnlinePay.GetMaxId();
            Log.Add("添加在线支付", "OnlinePay", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑支付信息
        /// </summary>
        public void Pay_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Pay model = B_Lebi_Pay.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Pay();
            }
            model = B_Lebi_Pay.BindForm(model);
            model.Name = Language.RequestString("Name");
            model.Description = Language.RequestString("Description");
            int c = B_Lebi_Pay.Counts("Code='" + model.Code + "' and id!=" + model.id + "");
            if (c > 0)
            {
                Response.Write("{\"msg\":\"" + Tag("系统代码不可重复") + "\"}");
                return;
            }
            if (addflag)
            {
                if (!EX_Admin.Power("pay_add", "添加付款方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Pay.Add(model);
                id = B_Lebi_Pay.GetMaxId();
                Log.Add("添加付款方式", "Pay", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            else
            {
                if (!EX_Admin.Power("pay_edit", "编辑付款方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_Pay.Update(model);
                Log.Add("编辑付款方式", "Pay", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量更新支付信息
        /// </summary>
        public void Pay_Update()
        {
            if (!EX_Admin.Power("pay_edit", "编辑付款方式"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("IDS");
            List<Lebi_Pay> models = B_Lebi_Pay.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_Pay model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                B_Lebi_Pay.Update(model);
            }
            Log.Add("编辑付款方式", "Pay", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除支付信息
        /// </summary>
        public void Pay_Del()
        {
            if (!EX_Admin.Power("pay_del", "删除付款方式"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Pay.Delete("id in (lbsql{" + id + "}) and Code=''");
            Log.Add("删除付款方式", "Pay", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑发票信息
        /// </summary>
        public void BillType_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_BillType model = B_Lebi_BillType.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_BillType();
            }
            model = B_Lebi_BillType.BindForm(model);
            model.Name = Language.RequestString("Name");
            model.Description = Language.RequestString("Description");
            if (addflag)
            {
                if (!EX_Admin.Power("billtype_add", "添加发票类型"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_BillType.Add(model);
                id = B_Lebi_BillType.GetMaxId();
                Log.Add("添加发票类型", "BillType", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            else
            {
                if (!EX_Admin.Power("billtype_edit", "编辑发票类型"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_BillType.Update(model);
                Log.Add("编辑发票类型", "BillType", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Name"), "CN"));
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除支付信息
        /// </summary>
        public void BillType_Del()
        {
            if (!EX_Admin.Power("billtype_del", "删除发票类型"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_BillType.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除发票类型", "BillType", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑客服面板成员信息
        /// </summary>
        public void ServicePanel_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ServicePanel model = B_Lebi_ServicePanel.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ServicePanel();
            }
            model = B_Lebi_ServicePanel.BindForm(model);
            model.Name = RequestTool.RequestString("Name");
            model.Account = RequestTool.RequestString("Account");
            model.Language_ids = RequestTool.RequestSafeString("Language_ids");
            model.ServicePanel_Type_id = RequestTool.RequestInt("ServicePanel_Type_id", 0);
            model.ServicePanel_Group_id = RequestTool.RequestInt("ServicePanel_Group_id", 0);
            model.Sort = RequestTool.RequestInt("Sort", 0);
            if (addflag)
            {
                if (!EX_Admin.Power("servicepanel_add", "添加客服面板成员"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ServicePanel.Add(model);
                id = B_Lebi_ServicePanel.GetMaxId();
                Log.Add("添加客服成员", "ServicePanel", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("servicepanel_edit", "编辑客服面板成员"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ServicePanel.Update(model);
                Log.Add("编辑客服成员", "ServicePanel", id.ToString(), CurrentAdmin, model.Name);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量更新客服面板成员信息
        /// </summary>
        public void ServicePanel_Update()
        {
            if (!EX_Admin.Power("servicepanel_edit", "编辑客服面板成员"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Uid");
            List<Lebi_ServicePanel> models = B_Lebi_ServicePanel.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_ServicePanel model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                model.Name = RequestTool.RequestString("Name" + model.id);
                model.Account = RequestTool.RequestString("Account" + model.id);
                B_Lebi_ServicePanel.Update(model);
            }
            Log.Add("编辑客服成员", "ServicePanel", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除客服面板成员信息
        /// </summary>
        public void ServicePanel_Del()
        {
            if (!EX_Admin.Power("servicepanel_del", "删除客服面板成员"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_ServicePanel.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除客服成员", "ServicePanel", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑客服面板部门信息
        /// </summary>
        public void ServicePanel_Group_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ServicePanel_Group model = B_Lebi_ServicePanel_Group.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ServicePanel_Group();
            }
            model = B_Lebi_ServicePanel_Group.BindForm(model);
            model.Name = RequestTool.RequestString("Name");
            model.Language_ids = RequestTool.RequestSafeString("Language_ids");
            model.Sort = RequestTool.RequestInt("Sort", 0);
            if (addflag)
            {
                if (!EX_Admin.Power("servicepanel_group_add", "添加客服面板部门"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ServicePanel_Group.Add(model);
                id = B_Lebi_ServicePanel_Group.GetMaxId();
                Log.Add("添加客服部门", "ServicePanel_Group", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("servicepanel_group_edit", "编辑客服面板部门"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ServicePanel_Group.Update(model);
                Log.Add("编辑客服部门", "ServicePanel_Group", id.ToString(), CurrentAdmin, model.Name);
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 批量更新客服面板部门信息
        /// </summary>
        public void ServicePanel_Group_Update()
        {
            if (!EX_Admin.Power("servicepanel_group_edit", "编辑客服面板部门"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Uid");
            List<Lebi_ServicePanel_Group> models = B_Lebi_ServicePanel_Group.GetList("id in (lbsql{" + id + "})", "");
            foreach (Lebi_ServicePanel_Group model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id + "", 0);
                model.Name = RequestTool.RequestString("Name" + model.id);
                B_Lebi_ServicePanel_Group.Update(model);
            }
            Log.Add("编辑客服部门", "ServicePanel_Group", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除客服面板部门信息
        /// </summary>
        public void ServicePanel_Group_Del()
        {
            if (!EX_Admin.Power("servicepanel_group_del", "删除客服面板部门"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_ServicePanel_Group.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除客服部门", "ServicePanel_Group", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑客服面板软件信息
        /// </summary>
        public void ServicePanel_Type_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_ServicePanel_Type model = B_Lebi_ServicePanel_Type.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_ServicePanel_Type();
            }
            model = B_Lebi_ServicePanel_Type.BindForm(model);
            model.Name = RequestTool.RequestString("Name");
            model.Url = RequestTool.RequestString("Url");
            model.Text = RequestTool.RequestString("Text");
            model.Face = RequestTool.RequestString("Face");
            model.Code = RequestTool.RequestString("Code");
            model.IsOnline = RequestTool.RequestInt("IsOnline", 0);
            model.Sort = RequestTool.RequestInt("Sort", 0);
            if (addflag)
            {
                if (!EX_Admin.Power("servicepanel_type_add", "添加客服面板软件"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ServicePanel_Type.Add(model);
                id = B_Lebi_ServicePanel_Type.GetMaxId();
                Log.Add("添加客服软件", "ServicePanel_Type", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("servicepanel_type_edit", "编辑客服面板软件"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_ServicePanel_Type.Update(model);
                Log.Add("编辑客服软件", "ServicePanel_Type", id.ToString(), CurrentAdmin, model.Name);
            }
            ImageHelper.LebiImagesUsed(model.Face, "config", id);
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除客服面板软件信息
        /// </summary>
        public void ServicePanel_Type_Del()
        {
            if (!EX_Admin.Power("servicepanel_type_del", "删除客服面板软件"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("ids");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_ServicePanel_Type.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除客服软件", "ServicePanel_Type", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 客服面板设置信息
        /// </summary>
        public void ServicePanel_Config()
        {
            if (!EX_Admin.Power("servicepanel_config", "编辑客服面板配置"))
            {
                AjaxNoPower();
                return;
            }
            Model.ServicePanel model = new Model.ServicePanel();
            model.X = RequestTool.RequestString("X");
            model.Y = RequestTool.RequestString("Y");
            model.Theme = RequestTool.RequestString("Theme");
            model.Status = RequestTool.RequestString("Status");
            model.IsFloat = RequestTool.RequestString("IsFloat");
            model.Style = RequestTool.RequestString("Style");
            string con = B_ServicePanel.ToJson(model);
            //BaseConfig bconf = ShopCache.GetBaseConfig();
            Lebi_Config conf = B_Lebi_Config.GetModel("[Name]='ServicePanel'");
            if (conf == null)
            {
                conf = new Lebi_Config();
                conf.Name = "ServicePanel";
                conf.Value = con;
                B_Lebi_Config.Add(conf);
            }
            else
            {
                conf.Value = con;
                B_Lebi_Config.Update(conf);
            }
            ShopCache.SetBaseConfig();//更新缓存
            string temp = "";
            temp += "var startX = document.body.clientWidth-" + RequestTool.RequestString("X") + ";\n";
            if (RequestTool.RequestString("IsFloat") == "1" && RequestTool.RequestString("Style") == "1")
            {
                temp += "var startY = " + RequestTool.RequestString("Y") + ";\n";
            }
            else if (RequestTool.RequestString("IsFloat") == "0" && RequestTool.RequestString("Style") == "1")
            {
                temp += "var startY = " + RequestTool.RequestString("Y") + ";\n";
            }
            if (RequestTool.RequestString("IsFloat") == "1" && RequestTool.RequestString("Style") == "1")
            {
                temp += "var verticalpos='fromtop';\n";
            }
            else if (RequestTool.RequestString("IsFloat") == "0" && RequestTool.RequestString("Style") == "1")
            {
                temp += "var verticalpos='frombottom';\n";
            }
            temp += "var winWidth = 0;\n";
            temp += "function iecompattest(){\n";
            temp += "   return (document.compatMode && document.compatMode!='BackCompat')? document.documentElement : document.body\n";
            temp += "}\n";

            temp += "function staticbar(){\n";
            temp += "	if (window.innerWidth)\n";
            temp += "	winWidth = window.innerWidth;\n";
            temp += "	else if ((document.body) && (document.body.clientWidth))\n";
            temp += "	winWidth = document.body.clientWidth;\n";
            temp += "	if (document.documentElement && document.documentElement.clientWidth){winWidth = document.documentElement.clientWidth;}\n";
            temp += "	startX = winWidth-" + RequestTool.RequestString("X") + ";\n";
            temp += "	barheight=document.getElementById('divStayTopLeft').offsetHeight\n";
            temp += "	var ns = (navigator.appName.indexOf('Netscape') != -1) || window.opera;\n";
            temp += "	var d = document;\n";
            temp += "	function ml(id){\n";
            temp += "		var el=d.getElementById(id);\n";
            temp += "		el.style.visibility='visible'\n";
            temp += "		if(d.layers)el.style=el;\n";
            temp += "		el.sP=function(x,y){this.style.left=x+'px';this.style.top=y+'px';};\n";
            temp += "		el.x = startX;\n";
            temp += "		if (verticalpos=='fromtop')\n";
            temp += "		el.y = startY;\n";
            temp += "		else{\n";
            temp += "		el.y = ns ? pageYOffset + innerHeight : iecompattest().scrollTop + iecompattest().clientHeight;\n";
            temp += "		el.y -= startY;\n";
            temp += "		}\n";
            temp += "		return el;\n";
            temp += "	}\n";
            temp += "	window.stayTopLeft=function(){\n";
            temp += "		if (verticalpos=='fromtop'){\n";
            temp += "		var pY = ns ? pageYOffset : iecompattest().scrollTop;\n";
            temp += "		ftlObj.y += (pY + startY - ftlObj.y)/8;\n";
            temp += "		}\n";
            temp += "		else{\n";
            temp += "		var pY = ns ? pageYOffset + innerHeight - barheight: iecompattest().scrollTop + iecompattest().clientHeight - barheight;\n";
            temp += "		ftlObj.y += (pY - startY - ftlObj.y)/8;\n";
            temp += "		}\n";
            temp += "		ftlObj.sP(ftlObj.x, ftlObj.y);\n";
            temp += "		setTimeout('stayTopLeft()', 10);\n";
            temp += "	}\n";
            temp += "	ftlObj = ml('divStayTopLeft');\n";
            temp += "	stayTopLeft();\n";
            temp += "}\n";
            temp += "if(typeof(HTMLElement)!='undefined'){\n";
            temp += "  HTMLElement.prototype.contains=function (obj)\n";
            temp += "  {\n";
            temp += "	  while(obj!=null&&typeof(obj.tagName)!='undefind'){\n";
            temp += "　 　 if(obj==this) return true;\n";
            temp += "　　	　obj=obj.parentNode;\n";
            temp += "　	  }\n";
            temp += "	  return false;\n";
            temp += "  }\n";
            temp += "}\n";
            temp += "if (window.addEventListener){\n";
            temp += "window.addEventListener('load', staticbar, false)\n";
            temp += "}else if (window.attachEvent){\n";
            temp += "window.attachEvent('onload', staticbar)\n";
            temp += "}else if (document.getElementById){\n";
            temp += "window.onload=staticbar;}\n";
            temp += "window.onresize=staticbar;\n";
            temp += "function servicepannelOver(){\n";
            temp += "   document.getElementById('divMenu').style.display = 'none';\n";
            temp += "   document.getElementById('divOnline').style.display = 'block';\n";
            temp += "   document.getElementById('divStayTopLeft').style.width = '170px';\n";
            temp += "}\n";
            temp += "function servicepannelOut(){\n";
            temp += "   document.getElementById('divMenu').style.display = 'block';\n";
            temp += "   document.getElementById('divOnline').style.display = 'none';\n";
            temp += "}\n";
            temp += "if(typeof(HTMLElement)!='undefined') \n";
            temp += "{\n";
            temp += "   HTMLElement.prototype.contains=function(obj)\n";
            temp += "   {\n";
            temp += "       while(obj!=null&&typeof(obj.tagName)!='undefind'){\n";
            temp += "   　　　 if(obj==this) return true;\n";
            temp += "   　　　 obj=obj.parentNode;\n";
            temp += "   　　}\n";
            temp += "          return false;\n";
            temp += "   };\n";
            temp += "}\n";
            temp += "function Showservicepannel(theEvent){\n";
            temp += "　 if (theEvent){\n";
            temp += "　    var browser=navigator.userAgent;\n";
            temp += "　	if (browser.indexOf('Firefox')>0){\n";
            temp += "　　     if (document.getElementById('divOnline').contains(theEvent.relatedTarget)) {\n";
            temp += "　　         return;\n";
            temp += "         }\n";
            temp += "      }\n";
            temp += "	    if (browser.indexOf('MSIE')>0 || browser.indexOf('Safari')>0 || browser.indexOf('.NET') > 0){ //IE Safari\n";
            temp += "          if (document.getElementById('divOnline').contains(event.toElement)) {\n";
            temp += "	          return;\n";
            temp += "          }\n";
            temp += "      }\n";
            temp += "   }\n";
            temp += "   document.getElementById('divMenu').style.display = 'block';\n";
            temp += "   document.getElementById('divOnline').style.display = 'none';\n";
            temp += "}";
            HtmlEngine save = new HtmlEngine();
            save.CreateFile("theme/system/js/ServicePanel.js", temp);
            Log.Add("编辑客服面板配置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量编辑语言标签
        /// </summary>
        public void LanguageTag_Edits()
        {
            if (!EX_Admin.Power("language_tag_edit", "编辑语言标签"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id == "")
                Response.Write("{\"msg\":\"OK\"}");
            List<Lebi_Language_Tag> models = B_Lebi_Language_Tag.GetList("id in (lbsql{" + id + "})", "");
            List<LBLanguage> lbs = new List<LBLanguage>();
            List<Lebi_Language_Code> langs = Language.Languages();
            LBLanguage lb;
            string value;
            string newvalue;
            foreach (Lebi_Language_Tag model in models)
            {
                Type type = model.GetType();
                bool editflag = false;
                foreach (Lebi_Language_Code lang in langs)
                {
                    System.Reflection.PropertyInfo p = type.GetProperty(lang.Code);
                    value = p.GetValue(model, null).ToString();
                    newvalue = RequestTool.RequestString(lang.Code + model.id);
                    if (value != newvalue)
                    {
                        p.SetValue(model, Convert.ChangeType(newvalue, p.PropertyType, CultureInfo.CurrentCulture), null);
                        if (newvalue != "")
                        {
                            lb = new LBLanguage();
                            lb.Lang = lang.Code;
                            lb.Tag = model.tag;
                            lb.Content = newvalue;
                            lbs.Add(lb);
                        }
                        editflag = true;
                    }
                }
                if (editflag)
                    B_Lebi_Language_Tag.Update(model);
            }
            Shop.LebiAPI.Service.Instanse.UpdateLanguageTagByThread(lbs);
            Log.Add("编辑语言标签", "Language_Tag", "", CurrentAdmin, id.ToString());
            ShopCache.SetLanguageTag();
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除语言标签
        /// </summary>
        public void LanguageTag_Del()
        {
            if (!EX_Admin.Power("language_tag_del", "删除语言标签"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Language_Tag.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除语言标签", "Language_Tag", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 发送任务中的一个邮件
        /// </summary>
        public void Email_send()
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            int Mail_SendTop = 0;
            int.TryParse(conf.Mail_SendTop, out Mail_SendTop);
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Email model = B_Lebi_Email.GetModel(id);
            Lebi_User user = B_Lebi_User.GetModel(model.User_id);
            if (user == null)
            {
                user = new Lebi_User();
            }
            if (model.Count_send >= Mail_SendTop)
            {
                model.Count_send = 1;
            }
            else
            {
                model.Count_send++;
            }
            model.Type_id_EmailStatus = 270;
            B_Lebi_Email.Update(model);
            Email.Send(model.Email, model.Title, model.Content, user, model.id);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除任务中的一个邮件
        /// </summary>
        public void Email_del()
        {
            if (!EX_Admin.Power("email_del", "删除邮件任务"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的数据") + "\"}");
                return;
            }
            B_Lebi_Email.Delete("id in (lbsql{" + id + "})");
            string description = id.ToString();
            Log.Add("删除邮件任务", "Email", "", CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 数据备份设置
        /// </summary>
        public void BackUPConfig_Edit()
        {
            if (!EX_Admin.Power("backup_config", "数据备份配置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.DataBase_BackUpTime = RequestTool.RequestString("DataBase_BackUpTime", "0");
            model.DataBase_BackPath = RequestTool.RequestString("DataBase_BackPath");
            model.DataBase_BackName = RequestTool.RequestString("DataBase_BackName");

            dob.SaveConfig(model);
            TimeWork tw = new TimeWork();
            tw.work_databackup_restart();
            Log.Add("数据备份设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑币种
        /// </summary>
        public void Currency_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Currency model = B_Lebi_Currency.GetModel(id);
            bool addflag = false;
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Currency();
            }
            if (addflag)
            {
                if (!EX_Admin.Power("currency_add", "添加币种"))
                {
                    AjaxNoPower();
                    return;
                }
            }
            else
            {
                if (!EX_Admin.Power("currency_edit", "编辑币种"))
                {
                    AjaxNoPower();
                    return;
                }
            }
            model = B_Lebi_Currency.BindForm(model);
            if (model.IsDefault == 1 && model.ExchangeRate != 1)
            {
                Response.Write("{\"msg\":\"" + Tag("默认币种汇率必须为1") + "\"}");
                return;
            }
            if (addflag)
            {
                B_Lebi_Currency.Add(model);
                id = B_Lebi_Language.GetMaxId();
                string description = model.Name;
                Log.Add("添加币种", "Currency", id.ToString(), CurrentAdmin, description);
            }
            else
            {

                B_Lebi_Currency.Update(model);
                string description = model.Name;
                Log.Add("编辑币种", "Currency", id.ToString(), CurrentAdmin, description);
            }
            if (model.IsDefault == 1)
            {
                List<Lebi_Currency> ls = B_Lebi_Currency.GetList("id!=" + id, "");
                foreach (Lebi_Currency l in ls)
                {
                    l.IsDefault = 0;
                    B_Lebi_Currency.Update(l);
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 删除币种
        /// </summary>
        public void Currency_Del()
        {
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Currency model = B_Lebi_Currency.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
            }

            if (!EX_Admin.Power("currency_del", "删除币种"))
            {
                AjaxNoPower();
                return;
            }

            B_Lebi_Currency.Delete(id);
            string description = model.Name;
            Log.Add("删除币种", "Currency", id.ToString(), CurrentAdmin, description);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 生成网站地图XML
        /// </summary>
        public void Sitemap_Edit()
        {
            if (!EX_Admin.Power("sitemap_edit", "网站地图"))
            {
                AjaxNoPower();
                return;
            }
            //HtmlEngine save = new HtmlEngine();
            //save.CreateFile("../../Sitemap.xml", Shop.Bussiness.SiteMap.GenerateSiteMapString());

            Shop.Bussiness.SiteMap.SaveSiteMap("/Sitemap.xml");
            Log.Add("生成网站地图", "Sitemap", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 生成图片规格
        /// </summary>
        public void CreatImageBySize()
        {
            if (!EX_Admin.Power("CreatImageBySize", "生成图片规格"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id");
            Lebi_ImageSize m = B_Lebi_ImageSize.GetModel(id);
            if (m != null)
            {
                string where = "TableName='Product' and Size not like '%" + m.Width + "$" + m.Height + "%'";
                List<Lebi_Image> models = B_Lebi_Image.GetList(where, "");
                string imagename = "";
                string savepath = "";
                int l = 0;

                foreach (Lebi_Image model in models)
                {
                    l = model.Image.LastIndexOf("/");

                    imagename = model.Image;
                    imagename = imagename.Substring(l + 1, (imagename.Length - 1 - l));
                    if (imagename.Contains("_w$h_"))
                        imagename = model.Image.Replace("_w$h_", "");
                    imagename = imagename.Replace(".", "_" + m.Width + "$" + m.Height + "_.");
                    savepath = model.Image.Substring(0, l);
                    ImageHelper.UPLoad(model.Image, savepath, imagename, m.Width, m.Height);
                    model.Size += "," + m.Width + "$" + m.Height;
                    model.Time_Update = System.DateTime.Now;
                    B_Lebi_Image.Update(model);
                }
            }
            Log.Add("生成图片规格", "ImageSize", "", CurrentAdmin, m.Width + "*" + m.Height);
            Response.Write("{\"msg\":\"OK\"}");
        }

        /// <summary>
        /// 编辑文件
        /// </summary>
        public void File_Edit()
        {
            if (!EX_Admin.Power("fileedit", "文件编辑"))
            {
                AjaxNoPower();
                return;
            }
            if (RequestTool.GetConfigKey("OnlineFileEdit").Trim() != "1")
            {
                AjaxNoPower();
                return;
            }
            string filename = RequestTool.RequestString("file");
            if (filename.ToLower().Contains("web.config"))
            {
                Response.Write("{\"msg\":\"OK\"}");
                return;
            }

            Log.Add("文件编辑", "FieEdit", filename);

            //生成页面
            string SkinContent = Request["SkinContent"];
            string SkinPath = "/" + filename;

            HtmlEngine.Instance.CreateFile(SkinPath, SkinContent);
            string result = "{\"msg\":\"OK\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 编辑安全问题
        /// </summary>
        public void Question_Edit()
        {
            if (!EX_Admin.Power("question_list", "安全问题"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_User_Question model = B_Lebi_User_Question.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_User_Question();
            }
            model = B_Lebi_User_Question.BindForm(model);
            model.Name = Language.RequestString("Name");
            if (addflag)
            {
                B_Lebi_User_Question.Add(model);
                id = B_Lebi_User_Question.GetMaxId();
                Log.Add("添加安全问题", "User_Question", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            else
            {
                B_Lebi_User_Question.Update(model);
                Log.Add("编辑安全问题", "User_Question", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(model.Name, CurrentLanguage.Code));
            }
            Response.Write("{\"msg\":\"OK\",\"id\":\"" + id + "\"}");

        }
        /// <summary>
        /// 删除安全问题
        /// </summary>
        public void Question_Del()
        {
            if (!EX_Admin.Power("question_list", "安全问题"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_User_Question.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除安全问题", "User_Question", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除留言反馈
        /// </summary>
        public void Inquiry_Del()
        {
            if (!EX_Admin.Power("inquiry_list", "留言反馈"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            Log.Add("删除留言反馈", "Inquiry", id, CurrentAdmin);
            B_Lebi_Inquiry.Delete("id in (lbsql{" + id + "})");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 处理留言反馈
        /// </summary>
        public void Inquiry_Edit()
        {
            if (!EX_Admin.Power("inquiry_list", "留言反馈"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            Lebi_Inquiry model = B_Lebi_Inquiry.GetModel(id);
            if (model == null)
            {
                Response.Write("{\"msg" + Tag("参数错误") + "\"}");
                return;
            }
            int t = RequestTool.RequestInt("t", 0);
            model.Type_id_InquiryStatus = t;
            model.Time_Update = System.DateTime.Now;
            B_Lebi_Inquiry.Update(model);
            Log.Add("编辑留言反馈", "Inquiry", id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量修改留言反馈状态
        /// </summary>
        public void Inquiry_Update()
        {
            if (!EX_Admin.Power("inquiry_list", "留言反馈"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("id");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("参数错误") + "\"}");
                return;
            }
            List<Lebi_Inquiry> models = B_Lebi_Inquiry.GetList("id in (lbsql{" + id + "}) and Type_id_InquiryStatus != 412", "");
            foreach (Lebi_Inquiry model in models)
            {
                model.Time_Update = System.DateTime.Now;
                model.Type_id_InquiryStatus = 412;
                B_Lebi_Inquiry.Update(model);
            }
            Log.Add("编辑留言反馈", "Inquiry", id.ToString(), CurrentAdmin);
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量更新语言设置
        /// </summary>
        public void SetLanguage_Update()
        {
            string table = RequestTool.RequestString("table");
            string ids = RequestTool.RequestString("ids");
            string id = RequestTool.RequestString("id");
            string Language_ids = RequestTool.RequestString("Language_ids");
            int DataType = RequestTool.RequestInt("DataType", 0);
            int UpdateType = RequestTool.RequestInt("UpdateType", 0);
            if (DataType == 0)
            {
                if (ids == "")
                {
                    Response.Write("{\"msg\":\"" + Tag("没有选择任何数据") + "\"}");
                    return;
                }
            }
            if (table == "Advert")
            {
                if (!EX_Admin.Power("advertimage_edit", "编辑广告"))
                {
                    AjaxNoPower();
                }
                string where = "Theme_Advert_id in (lbsql{" + ids + "})";
                if (DataType == 1)
                {
                    where = "Theme_id = lbsql{" + id + "}";
                }
                List<Lebi_Advert> models = B_Lebi_Advert.GetList(where, "");
                foreach (Lebi_Advert model in models)
                {
                    if (UpdateType == 0)
                    {
                        model.Language_ids = Language_ids;
                    }
                    else
                    {
                        model.Language_ids += "," + Language_ids;
                    }
                    model.Language_Codes = Language.LanuageidsToCodes(model.Language_ids);
                    B_Lebi_Advert.Update(model);
                }
                Log.Add("编辑广告", "AdvertImage", id.ToString(), CurrentAdmin, ids.ToString());
            }
            if (table == "Page")
            {
                if (!EX_Admin.Power("page_edit", "编辑结点内容"))
                {
                    AjaxNoPower();
                }
                string where = "id in (lbsql{" + ids + "})";
                if (DataType == 1)
                {
                    where = "Node_id = lbsql{" + id + "}";
                }
                List<Lebi_Page> models = B_Lebi_Page.GetList(where, "");
                foreach (Lebi_Page model in models)
                {
                    if (UpdateType == 0)
                    {
                        model.Language_ids = Language_ids;
                    }
                    else
                    {
                        model.Language_ids += "," + Language_ids;
                    }
                    model.Language = Language.LanuageidsToCodes(model.Language_ids);
                    B_Lebi_Page.Update(model);
                }
                Log.Add("编辑结点内容", "Page", id.ToString(), CurrentAdmin, ids.ToString());
            }
            if (table == "Node")
            {
                if (!EX_Admin.Power("usernode_edit", "编辑自定义分类"))
                {
                    AjaxNoPower();
                }
                string where = "id in (lbsql{" + ids + "})";
                if (DataType == 1)
                {
                    where = "Code = lbsql{'" + id + "'}";
                }
                List<Lebi_Node> models = B_Lebi_Node.GetList(where, "");
                foreach (Lebi_Node model in models)
                {
                    if (UpdateType == 0)
                    {
                        model.Language_ids = Language_ids;
                    }
                    else
                    {
                        model.Language_ids += "," + Language_ids;
                    }
                    model.Language = Language.LanuageidsToCodes(model.Language_ids);
                    B_Lebi_Node.Update(model);
                    List<Lebi_Page> pmodels = B_Lebi_Page.GetList("Node_id = "+ model.id, "");
                    foreach (Lebi_Page pmodel in pmodels)
                    {
                        if (UpdateType == 0)
                        {
                            pmodel.Language_ids = Language_ids;
                        }
                        else
                        {
                            pmodel.Language_ids += "," + Language_ids;
                        }
                        pmodel.Language = Language.LanuageidsToCodes(pmodel.Language_ids);
                        B_Lebi_Page.Update(pmodel);
                    }
                }
                Log.Add("编辑结点内容", "Node", ids.ToString(), CurrentAdmin, ids.ToString());
            }
            if (table == "FriendLink")
            {
                if (!EX_Admin.Power("friendlink_edit", "编辑友情链接"))
                {
                    AjaxNoPower();
                }
                string where = "id in (lbsql{" + ids + "})";
                if (DataType == 1)
                {
                    where = "1=1";
                }
                List<Lebi_FriendLink> models = B_Lebi_FriendLink.GetList(where, "");
                foreach (Lebi_FriendLink model in models)
                {
                    if (UpdateType == 0)
                    {
                        model.Language_ids = Language_ids;
                    }
                    else
                    {
                        model.Language_ids += "," + Language_ids;
                    }
                    model.Language = Language.LanuageidsToCodes(model.Language_ids);
                    B_Lebi_FriendLink.Update(model);
                }
                Log.Add("编辑友情链接", "FriendLink", id.ToString(), CurrentAdmin, ids.ToString());
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑短信模板
        /// </summary>
        public void SMSTPL_Edit()
        {
            if (!EX_Admin.Power("theme_sms_edit", "编辑短信模板"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            string type = RequestTool.RequestString("type");
            switch (type)
            {
                case "user":
                    model.SMSTPL_newuser = Language.RequestString("SMSTPL_newuser");
                    model.SMSTPL_ordersubmit = Language.RequestString("SMSTPL_ordersubmit");
                    model.SMSTPL_ordershipping = Language.RequestString("SMSTPL_ordershipping");
                    model.SMSTPL_balance = Language.RequestString("SMSTPL_balance");
                    model.SMSTPL_getpwd = Language.RequestString("SMSTPL_getpwd");
                    model.SMSTPL_getnewpwd = Language.RequestString("SMSTPL_getnewpwd");
                    model.SMSTPL_comment = Language.RequestString("SMSTPL_comment");
                    model.SMSTPL_ask = Language.RequestString("SMSTPL_ask");
                    model.SMSTPL_message = Language.RequestString("SMSTPL_message");
                    model.SMSTPL_checkcode = Language.RequestString("SMSTPL_checkcode");
                    model.SMSTPL_reserveok = Language.RequestString("SMSTPL_reserveok");
                    break;
                case "admin":
                    model.SMSTPL_Admin_newuser = Language.RequestString("SMSTPL_Admin_newuser");
                    model.SMSTPL_Admin_ordersubmit = Language.RequestString("SMSTPL_Admin_ordersubmit");
                    model.SMSTPL_Admin_orderrecive = Language.RequestString("SMSTPL_Admin_orderrecive");
                    model.SMSTPL_Admin_ordercomment = Language.RequestString("SMSTPL_Admin_ordercomment");
                    model.SMSTPL_Admin_inquiry = Language.RequestString("SMSTPL_Admin_inquiry");
                    model.SMSTPL_Admin_comment = Language.RequestString("SMSTPL_Admin_comment");
                    model.SMSTPL_Admin_ask = Language.RequestString("SMSTPL_Admin_ask");
                    model.SMSTPL_Admin_message = Language.RequestString("SMSTPL_Admin_message");
                    model.SMSTPL_Admin_orderpaid = Language.RequestString("SMSTPL_Admin_orderpaid");
                    break;
            }
            dob.SaveConfig(model);
            Log.Add("编辑短信模板", "Config", "", CurrentAdmin, type);
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void SMSConfig_Edit()
        {
            if (!EX_Admin.Power("smsconfig_edit", "手机短信设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.SMS_user = RequestTool.RequestString("SMS_user");
            string pwd = RequestTool.RequestString("SMS_password");
            if (pwd != "******")
                model.SMS_password = pwd;
            model.SMS_server = RequestTool.RequestString("SMS_server");
            model.SMS_state = RequestTool.RequestString("SMS_state", "0");
            model.SMS_apitype = RequestTool.RequestString("SMS_apitype");
            model.SMS_sendmode = RequestTool.RequestString("SMS_sendmode");
            model.SMS_reciveno = RequestTool.RequestString("SMS_reciveno");
            model.SMS_serverport = RequestTool.RequestString("SMS_serverport", "20002");
            model.SMS_maxlen = RequestTool.RequestString("SMS_maxlen", "0");
            model.SMS_lastmsg = RequestTool.RequestString("SMS_lastmsg");
            model.SMS_httpapi = RequestTool.RequestString("SMS_httpapi");
            dob.SaveConfig(model);
            Log.Add("手机短信设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 刷新后台文件
        /// </summary>
        public virtual void UpdateAdmin()
        {
            //Shop.Bussiness.SystemTheme.CreateAdmin("admin", "");
            //Shop.Bussiness.SystemTheme.CreateAdmin("supplier", "");
            //Shop.Bussiness.SystemTheme.CreateAdmin("inc", "");
            Shop.Bussiness.SystemTheme.CreateSystemPage();
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 删除自提点
        /// </summary>
        public void pickup_Del()
        {
            if (!EX_Admin.Power("pickup_del", "删除自提点"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_PickUp.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除自提点", "PickUp", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑自提点
        /// </summary>
        public void pickup_Edit()
        {
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_PickUp model = B_Lebi_PickUp.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_PickUp();
            }
            model = B_Lebi_PickUp.BindForm(model);
            if (addflag)
            {
                if (!EX_Admin.Power("pickup_add", "添加配送方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_PickUp.Add(model);
                id = B_Lebi_PickUp.GetMaxId();
                Log.Add("添加配送方式", "PickUp", id.ToString(), CurrentAdmin, model.Name);
            }
            else
            {
                if (!EX_Admin.Power("pickup_edit", "编辑配送方式"))
                {
                    AjaxNoPower();
                    return;
                }
                B_Lebi_PickUp.Update(model);
                Log.Add("编辑配送方式", "PickUp", id.ToString(), CurrentAdmin, model.Name);
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑每日箴言
        /// </summary>
        public void Tips_Edit()
        {
            if (!EX_Admin.Power("tips_list", "每日箴言"))
            {
                AjaxNoPower();
                return;
            }
            int id = RequestTool.RequestInt("id", 0);
            bool addflag = false;
            Lebi_Tips model = B_Lebi_Tips.GetModel(id);
            if (model == null)
            {
                addflag = true;
                model = new Lebi_Tips();
            }
            model = B_Lebi_Tips.BindForm(model);
            model.Content = Language.RequestString("Content");
            if (addflag)
            {
                B_Lebi_Tips.Add(model);
                id = B_Lebi_Tips.GetMaxId();
                Log.Add("添加每日箴言", "Tips", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Content"), "CN"));
            }
            else
            {
                B_Lebi_Tips.Update(model);
                Log.Add("编辑每日箴言", "Tips", id.ToString(), CurrentAdmin, Shop.Bussiness.Language.Content(Language.RequestString("Content"), "CN"));
            }
            string result = "{\"msg\":\"OK\", \"id\":\"" + id + "\"}";
            Response.Write(result);
        }
        /// <summary>
        /// 删除每日箴言
        /// </summary>
        public void Tips_Del()
        {
            if (!EX_Admin.Power("tips_list", "每日箴言"))
            {
                AjaxNoPower();
                return;
            }
            string id = RequestTool.RequestString("Fid");
            if (id == "")
            {
                Response.Write("{\"msg\":\"" + Tag("请选择要删除的信息") + "\"}");
                return;
            }
            B_Lebi_Tips.Delete("id in (lbsql{" + id + "})");
            Log.Add("删除每日箴言", "Tips", id.ToString(), CurrentAdmin, id.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 编辑APP菜单
        /// </summary>
        public void APPMenu_Edit()
        {
            if (!EX_Admin.Power("appmenu", "编辑APP菜单"))
            {
                AjaxNoPower();
                return;
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<LanguageContent> list = new List<LanguageContent>();
            LanguageContent con = new LanguageContent();
            foreach (Lebi_Language_Code lang in Shop.Bussiness.Language.Languages())
            {
                string menu_sort = RequestTool.RequestString("menu_sort" + lang.Code);
                string menu_name = RequestTool.RequestString("menu_name" + lang.Code);
                string menu_icon = RequestTool.RequestString("menu_icon" + lang.Code);
                string menu_url = RequestTool.RequestString("menu_url" + lang.Code);

                string mi = RequestTool.RequestString("mi" + lang.Code);
                List<BaseConfigAppMenu> models = new List<BaseConfigAppMenu>();
                string[] sorts = menu_sort.Split(',');
                string[] names = menu_name.Split(',');
                string[] icons = menu_icon.Split(',');
                string[] urls = menu_url.Split(',');
                string[] mis = mi.Split(',');
                for (int i = 0; i < sorts.Length; i++)
                {
                    if (sorts[i] == "")
                        continue;
                    int sort = 0;
                    int.TryParse(sorts[i], out sort);
                    BaseConfigAppMenu model = new BaseConfigAppMenu();
                    model.Icon = icons[i];
                    model.Name = names[i];
                    model.Sort = sort;
                    model.URL = urls[i];
                    List<BaseConfigAppMenuSon> sons = new List<BaseConfigAppMenuSon>();
                    try
                    {
                        string son_sort = RequestTool.RequestString(mis[i] + "menu_sort" + lang.Code);
                        if (son_sort != "")
                        {
                            string son_name = RequestTool.RequestString(mis[i] + "menu_name" + lang.Code);
                            string son_url = RequestTool.RequestString(mis[i] + "menu_url" + lang.Code);
                            string[] sonsorts = son_sort.Split(',');
                            string[] sonnames = son_name.Split(',');
                            string[] sonurls = son_url.Split(',');
                            for (int j = 0; j < sonsorts.Length; j++)
                            {
                                int ssort = 0;
                                int.TryParse(sonsorts[j], out ssort);
                                BaseConfigAppMenuSon son = new BaseConfigAppMenuSon();
                                son.Name = sonnames[j];
                                son.Sort = ssort;
                                son.URL = sonurls[j];
                                sons.Add(son);
                            }
                            if (sons.Count > 0)
                            {
                                sons = sons.OrderByDescending(a => a.Sort).ToList();
                            }
                        }
                    }
                    catch
                    {
                    }
                    model.Son = sons;
                    models.Add(model);
                }
                string appmenu = "";
                if (models.Count > 0)
                {
                    models = models.OrderByDescending(a => a.Sort).ToList();
                    appmenu = jss.Serialize(models);

                }

                con = new LanguageContent();
                con.L = lang.Code;
                con.C = appmenu;
                list.Add(con);
            }
            string json = Language.ToJson(list);
            B_BaseConfig.Set("app_menu", json);
            string app_name = Language.RequestString("app_name");
            string app_lefticon = Language.RequestString("app_lefticon");
            string app_lefturl = Language.RequestString("app_lefturl");
            string app_righticon = Language.RequestString("app_righticon");
            string app_righturl = Language.RequestString("app_righturl");
            string app_toplogo = Language.RequestString("app_toplogo");
            string app_toplogourl = Language.RequestString("app_toplogourl");
            string app_topbackground = Language.RequestString("app_topbackground");
            string app_topcolor = Language.RequestString("app_topcolor");
            string app_topline = Language.RequestString("app_topline");
            string app_bottombackground = Language.RequestString("app_bottombackground");
            string app_bottomcolor = Language.RequestString("app_bottomcolor");
            string app_bottomline = Language.RequestString("app_bottomline");
            string app_bottomcount = Language.RequestString("app_bottomcount");
            string app_startimage = Language.RequestString("app_startimage");
            string app_starturl = Language.RequestString("app_starturl");
            string app_waittimes = Language.RequestString("app_waittimes");
            string app_version = Language.RequestString("app_version");
            string app_downloadurl = Language.RequestString("app_downloadurl");
            B_BaseConfig.Set("app_name", app_name);
            B_BaseConfig.Set("app_lefticon", app_lefticon);
            B_BaseConfig.Set("app_lefturl", app_lefturl);
            B_BaseConfig.Set("app_righticon", app_righticon);
            B_BaseConfig.Set("app_righturl", app_righturl);
            B_BaseConfig.Set("app_toplogo", app_toplogo);
            B_BaseConfig.Set("app_toplogourl", app_toplogourl);
            B_BaseConfig.Set("app_topbackground", app_topbackground);
            B_BaseConfig.Set("app_topcolor", app_topcolor);
            B_BaseConfig.Set("app_topline", app_topline);
            B_BaseConfig.Set("app_bottombackground", app_bottombackground);
            B_BaseConfig.Set("app_bottomcolor", app_bottomcolor);
            B_BaseConfig.Set("app_bottomline", app_bottomline);
            B_BaseConfig.Set("app_bottomcount", app_bottomcount);
            B_BaseConfig.Set("app_startimage", app_startimage);
            B_BaseConfig.Set("app_starturl", app_starturl);
            B_BaseConfig.Set("app_waittimes", app_waittimes);
            B_BaseConfig.Set("app_updatetime", System.DateTime.Now.ToString());
            B_BaseConfig.Set("app_version", app_version);
            B_BaseConfig.Set("app_downloadurl", app_downloadurl);
            Log.Add("接口设置", "APP", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// APP推送配置
        /// </summary>
        public void APPPush_Edit()
        {
            if (!EX_Admin.Power("appmenu", "编辑APP菜单"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.APPPush_state = RequestTool.RequestInt("APPPush_state", 0).ToString();
            model.APPPush_sendmode = RequestTool.RequestString("APPPush_sendmode");
            model.app_share = RequestTool.RequestString("app_share");
            model.app_share_wechat_key = RequestTool.RequestString("app_share_wechat_key");
            model.app_share_wechat_secret = RequestTool.RequestString("app_share_wechat_secret");
            model.app_share_qq_key = RequestTool.RequestString("app_share_qq_key");
            model.app_share_qq_secret = RequestTool.RequestString("app_share_qq_secret");
            model.app_share_dingtalk_key = RequestTool.RequestString("app_share_dingtalk_key");
            model.app_share_dingtalk_secret = RequestTool.RequestString("app_share_dingtalk_secret");
            dob.SaveConfig(model);
            Log.Add("推送配置", "APP", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 更新子站文件
        /// </summary>
        public void UpdateSonSite()
        {
            List<Lebi_Site> models = B_Lebi_Site.GetList("Domain!='' and Path !='' and Path !='/'", "Sort desc");
            foreach (Lebi_Site model in models)
            {
                if (model.Domain.Length > 3 && model.Domain.Contains("."))
                {
                    //附属站点使用了域名处理web.config和bin
                    copyfiles("bin", model.Path);
                    copyfiles("ajax", model.Path);
                    copyfiles("inc", model.Path);
                    copyfiles("agent", model.Path);
                    copyfiles("supplier", model.Path);
                    copyfiles("api", model.Path);
                    copyfiles("platform", model.Path);
                    copyfiles("onlinepay", model.Path);
                    copyfiles("config", model.Path);
                    copyfiles("theme/system/js", model.Path);
                    copyfiles("theme/system/css", model.Path);
                    copyfiles("theme/system/images", model.Path);
                    copyfiles("theme/system/systempage", model.Path);
                    copyfile("web.config", model.Path);
                    copyfile("code.aspx", model.Path);
                    copyfile("pic.aspx", model.Path);
                }
            }
            Response.Write("{\"msg\":\"OK\"}");
        }
        /// <summary>
        /// 批量编辑类型
        /// </summary>
        public void Type_Update()
        {
            if (!EX_Admin.Power("type_list", "类型管理"))
            {
                AjaxNoPower();
                return;
            }
            string ids = RequestTool.RequestSafeString("ids");
            List<Lebi_Type> models = B_Lebi_Type.GetList("id in (lbsql{" + ids + "})", "");
            foreach (Lebi_Type model in models)
            {
                model.Sort = RequestTool.RequestInt("Sort" + model.id, 0);
                model.Color = RequestTool.RequestString("Color" + model.id);
                B_Lebi_Type.Update(model);
            }
            Log.Add("更新类型", "Type", ids.ToString(), CurrentAdmin, ids.ToString());
            Response.Write("{\"msg\":\"OK\"}");
        }
        public void Layout_Edit()
        {
            if (!EX_Admin.Power("baseconfig_edit", "基本设置"))
            {
                AjaxNoPower();
                return;
            }
            B_BaseConfig dob = new B_BaseConfig();
            BaseConfig model = new BaseConfig();
            model.system_layout_logo = RequestTool.RequestString("system_layout_logo");
            model.system_login_logo = RequestTool.RequestString("system_login_logo");
            model.system_login_background = RequestTool.RequestString("system_login_background");
            dob.SaveConfig(model);
            Log.Add("界面设置", "Config", "", CurrentAdmin, "");
            Response.Write("{\"msg\":\"OK\"}");
        }
    }
}