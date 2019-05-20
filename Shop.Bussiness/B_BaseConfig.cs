using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Collections.Generic;
using LB.Tools;
using Shop.Model;
using DB.LebiShop;
using System.Collections;

namespace Shop.Bussiness
{
    /// <summary>
    /// BaseConfigXML 的摘要说明
    /// </summary>
    public class B_BaseConfig : Page
    {
        public BaseConfig LoadConfig()
        {
            BaseConfig model = new BaseConfig();
            //model=(BaseConfig)SerializationHelper.Load(model.GetType(), this.xmlpath);
            List<Lebi_Config> models = B_Lebi_Config.GetList("", "");
            Hashtable ht = new Hashtable();
            foreach (Lebi_Config c in models)
            {
                if (ht.Contains(c.Name) == false)
                    ht.Add(c.Name, c.Value);
            }
            model.BillFlag = ht["BillFlag"] == null ? "" : (string)ht["BillFlag"];
            model.ClickFlag = ht["ClickFlag"] == null ? "" : (string)ht["ClickFlag"];
            model.ClickNum1 = ht["ClickNum1"] == null ? "" : (string)ht["ClickNum1"];
            model.ClickNum2 = ht["ClickNum2"] == null ? "" : (string)ht["ClickNum2"];
            model.CommFlag = ht["CommFlag"] == null ? "" : (string)ht["CommFlag"];
            model.Copyright = ht["Copyright"] == null ? "" : (string)ht["Copyright"];
            model.Description = ht["Description"] == null ? "" : (string)ht["Description"];
            model.Domain = ht["Domain"] == null ? "" : (string)ht["Domain"];
            model.Email = ht["Email"] == null ? "" : (string)ht["Email"];
            model.Filter = ht["Filter"] == null ? "" : (string)ht["Filter"];
            model.HtmlFlag = ht["HtmlFlag"] == null ? "" : (string)ht["HtmlFlag"];
            model.Keywords = ht["Keywords"] == null ? "" : (string)ht["Keywords"];
            model.Logoimg = ht["Logoimg"] == null ? "" : (string)ht["Logoimg"];
            model.Loop = ht["Loop"] == null ? "" : (string)ht["Loop"];
            model.MailName = ht["MailName"] == null ? "" : (string)ht["MailName"];
            model.MailSign = ht["MailSign"] == null ? "" : (string)ht["MailSign"];
            model.MailPassWord = ht["MailPassWord"] == null ? "" : (string)ht["MailPassWord"];
            model.MailIsSSL = ht["MailIsSSL"] == null ? "" : (string)ht["MailIsSSL"];
            model.MailPort = ht["MailPort"] == null ? "" : (string)ht["MailPort"];
            model.Name = ht["Name"] == null ? "" : (string)ht["Name"];
            model.Openpwd = ht["Openpwd"] == null ? "" : (string)ht["Openpwd"];
            model.OpenpwdFlag = ht["OpenpwdFlag"] == null ? "" : (string)ht["OpenpwdFlag"];
            model.Phone = ht["Phone"] == null ? "" : (string)ht["Phone"];
            model.QQ = ht["QQ"] == null ? "" : (string)ht["QQ"];
            model.SalesFlag = ht["SalesFlag"] == null ? "" : (string)ht["SalesFlag"];
            model.SalesNum1 = ht["SalesNum1"] == null ? "" : (string)ht["SalesNum1"];
            model.SalesNum2 = ht["SalesNum2"] == null ? "" : (string)ht["SalesNum2"];
            model.ServiceP = ht["ServiceP"] == null ? "" : (string)ht["ServiceP"];
            model.SmtpAddress = ht["SmtpAddress"] == null ? "" : (string)ht["SmtpAddress"];
            model.Tieshi = ht["Tieshi"] == null ? "" : (string)ht["Tieshi"];
            model.Title = ht["Title"] == null ? "" : (string)ht["Title"];
            model.UrlFlag = ht["UrlFlag"] == null ? "" : (string)ht["UrlFlag"];
            model.VisitTime = ht["VisitTime"] == null ? "" : (string)ht["VisitTime"];
            model.VisitTimeFlag = ht["VisitTimeFlag"] == null ? "" : (string)ht["VisitTimeFlag"];
            model.WeiHuFlag = ht["WeiHuFlag"] == null ? "" : (string)ht["WeiHuFlag"];
            model.Wornings = ht["Wornings"] == null ? "" : (string)ht["Wornings"];
            model.ServicePanel = ht["ServicePanel"] == null ? "" : (string)ht["ServicePanel"];
            model.KuaiDi100 = ht["KuaiDi100"] == null ? "" : (string)ht["KuaiDi100"];
            model.KuaiDi100Status = ht["KuaiDi100Status"] == null ? "" : (string)ht["KuaiDi100Status"];
            model.EmailTPL_getpwd = ht["EmailTPL_getpwd"] == null ? "" : (string)ht["EmailTPL_getpwd"];
            model.EmailTPL_newuser = ht["EmailTPL_newuser"] == null ? "" : (string)ht["EmailTPL_newuser"];
            model.EmailTPL_ordershipping = ht["EmailTPL_ordershipping"] == null ? "" : (string)ht["EmailTPL_ordershipping"];
            model.EmailTPL_ordersubmit = ht["EmailTPL_ordersubmit"] == null ? "" : (string)ht["EmailTPL_ordersubmit"];
            model.SMSTPL_orderpickup = ht["SMSTPL_orderpickup"] == null ? "" : (string)ht["SMSTPL_orderpickup"];
            model.EmailTPL_getpwd_title = ht["EmailTPL_getpwd_title"] == null ? "" : (string)ht["EmailTPL_getpwd_title"];
            model.EmailTPL_newuser_title = ht["EmailTPL_newuser_title"] == null ? "" : (string)ht["EmailTPL_newuser_title"];
            model.EmailTPL_ordershipping_title = ht["EmailTPL_ordershipping_title"] == null ? "" : (string)ht["EmailTPL_ordershipping_title"];
            model.EmailTPL_ordersubmit_title = ht["EmailTPL_ordersubmit_title"] == null ? "" : (string)ht["EmailTPL_ordersubmit_title"];
            model.AdminMailAddress = ht["AdminMailAddress"] == null ? "" : (string)ht["AdminMailAddress"];
            model.AdminMailSign = ht["AdminMailSign"] == null ? "" : (string)ht["AdminMailSign"];
            model.EmailTPL_Admin_newuser = ht["EmailTPL_Admin_newuser"] == null ? "" : (string)ht["EmailTPL_Admin_newuser"];
            model.EmailTPL_Admin_ordersubmit = ht["EmailTPL_Admin_ordersubmit"] == null ? "" : (string)ht["EmailTPL_Admin_ordersubmit"];
            model.EmailTPL_Admin_ordercomment = ht["EmailTPL_Admin_ordercomment"] == null ? "" : (string)ht["EmailTPL_Admin_ordercomment"];
            model.EmailTPL_Admin_inquiry = ht["EmailTPL_Admin_inquiry"] == null ? "" : (string)ht["EmailTPL_Admin_inquiry"];
            model.EmailTPL_Admin_comment = ht["EmailTPL_Admin_comment"] == null ? "" : (string)ht["EmailTPL_Admin_comment"];
            model.EmailTPL_Admin_ask = ht["EmailTPL_Admin_ask"] == null ? "" : (string)ht["EmailTPL_Admin_ask"];
            model.EmailTPL_Admin_message = ht["EmailTPL_Admin_message"] == null ? "" : (string)ht["EmailTPL_Admin_message"];
            model.EmailTPL_Admin_newuser_title = ht["EmailTPL_Admin_newuser_title"] == null ? "" : (string)ht["EmailTPL_Admin_newuser_title"];
            model.EmailTPL_Admin_ordersubmit_title = ht["EmailTPL_Admin_ordersubmit_title"] == null ? "" : (string)ht["EmailTPL_Admin_ordersubmit_title"];
            model.EmailTPL_Admin_ordercomment_title = ht["EmailTPL_Admin_ordercomment_title"] == null ? "" : (string)ht["EmailTPL_Admin_ordercomment_title"];
            model.EmailTPL_Admin_inquiry_title = ht["EmailTPL_Admin_inquiry_title"] == null ? "" : (string)ht["EmailTPL_Admin_inquiry_title"];
            model.EmailTPL_Admin_comment_title = ht["EmailTPL_Admin_comment_title"] == null ? "" : (string)ht["EmailTPL_Admin_comment_title"];
            model.EmailTPL_Admin_ask_title = ht["EmailTPL_Admin_ask_title"] == null ? "" : (string)ht["EmailTPL_Admin_ask_title"];
            model.EmailTPL_Admin_message_title = ht["EmailTPL_Admin_message_title"] == null ? "" : (string)ht["EmailTPL_Admin_message_title"];
            model.EmailTPL_changgouqingdan = ht["EmailTPL_changgouqingdan"] == null ? "" : (string)ht["EmailTPL_changgouqingdan"];
            model.EmailTPL_changgouqingdan_title = ht["EmailTPL_changgouqingdan_title"] == null ? "" : (string)ht["EmailTPL_changgouqingdan_title"];
            model.EmailTPL_checkcode = ht["EmailTPL_checkcode"] == null ? "" : (string)ht["EmailTPL_checkcode"];
            model.EmailTPL_checkcode_title = ht["EmailTPL_checkcode_title"] == null ? "" : (string)ht["EmailTPL_checkcode_title"];
            model.EmailTPL_sendfriend = ht["EmailTPL_sendfriend"] == null ? "" : (string)ht["EmailTPL_sendfriend"];
            model.EmailTPL_sendfriend_title = ht["EmailTPL_sendfriend_title"] == null ? "" : (string)ht["EmailTPL_sendfriend_title"];
            model.EmailTPL_reserveok = ht["EmailTPL_reserveok"] == null ? "" : (string)ht["EmailTPL_reserveok"];
            model.EmailTPL_reserveok_title = ht["EmailTPL_reserveok_title"] == null ? "" : (string)ht["EmailTPL_reserveok_title"];
            model.SMSTPL_Admin_orderpaid = ht["SMSTPL_Admin_orderpaid"] == null ? "" : (string)ht["SMSTPL_Admin_orderpaid"];
            model.MailAddress = ht["MailAddress"] == null ? "" : (string)ht["MailAddress"];
            model.MailDisplayName = ht["MailDisplayName"] == null ? "" : (string)ht["MailDisplayName"];
            model.DataBase_BackPath = ht["DataBase_BackPath"] == null ? "" : (string)ht["DataBase_BackPath"];
            model.DataBase_BackName = ht["DataBase_BackName"] == null ? "" : (string)ht["DataBase_BackName"];
            model.Mail_SendTop = ht["Mail_SendTop"] == null ? "" : (string)ht["Mail_SendTop"];
            model.Mail_SendTime = ht["Mail_SendTime"] == null ? "" : (string)ht["Mail_SendTime"];
            model.DataBase_BackUpTime = ht["DataBase_BackUpTime"] == null ? "" : (string)ht["DataBase_BackUpTime"];
            model.CommentPoint = ht["CommentPoint"] == null ? "" : (string)ht["CommentPoint"];
            model.ImageBigHeight = 0;
            model.ImageBigWidth = 800;
            model.ImageMediumHeight = 320;
            model.ImageMediumWidth = 320;
            model.ImageSmallWidth = 100;
            model.ImageSmallHeight = 100;
            model.IsBasketAction = ht["IsBasketAction"] == null ? "0" : (string)ht["IsBasketAction"];
            model.ProductNumberPrefix = ht["ProductNumberPrefix"] == null ? "" : (string)ht["ProductNumberPrefix"];
            model.ProductNumberLength = ht["ProductNumberLength"] == null ? "8" : (string)ht["ProductNumberLength"];
            model.NewEventTimes = ht["NewEventTimes"] == null ? "60000" : (string)ht["NewEventTimes"];
            model.NewEventPlayAudio = ht["NewEventPlayAudio"] == null ? "1" : (string)ht["NewEventPlayAudio"];
            //授权
            model.LicenseMD5 = (string)ht["LicenseMD5"];
            model.LicensePWD = (string)ht["LicensePWD"];
            model.LicenseDomain = (string)ht["LicenseDomain"];
            model.LicenseString = (string)ht["LicenseString"];
            model.LicensePackage = (string)ht["LicensePackage"];
            model.LicenseUserName = (string)ht["LicenseUserName"];
            model.Version = (string)ht["Version"];
            model.Version_Son = (string)ht["Version_Son"];
            model.UpLoadLimit = ht["UpLoadLimit"] == null ? "2" : (string)ht["UpLoadLimit"];//默认2M
            model.UpLoadPath = ht["UpLoadPath"] == null ? "/upload" : (string)ht["UpLoadPath"];//默认/upload
            model.UpLoadSaveName = ht["UpLoadSaveName"] == null ? "0" : (string)ht["UpLoadSaveName"];
            model.UpLoadRName = (string)ht["UpLoadRName"];
            model.UpLoadMode = ht["UpLoadMode"] == null ? "0" : (string)ht["UpLoadMode"];
            model.IPLock = ht["IPLock"] == null ? "" : (string)ht["IPLock"];
            model.InstallCode = ht["InstallCode"] == null ? "" : (string)ht["InstallCode"];
            model.SpreadCode = ht["SpreadCode"] == null ? "" : (string)ht["SpreadCode"];
            //第三方登录
            model.platform_login = ht["platform_login"] == null ? "" : (string)ht["platform_login"];
            model.platform_qq_id = ht["platform_qq_id"] == null ? "" : (string)ht["platform_qq_id"];
            model.platform_qq_key = ht["platform_qq_key"] == null ? "" : (string)ht["platform_qq_key"];
            model.platform_qq_image = ht["platform_qq_image"] == null ? "" : (string)ht["platform_qq_image"];
            model.platform_qq_adduser = ht["platform_qq_adduser"] == null ? "1" : (string)ht["platform_qq_adduser"];
            model.platform_weibo_id = ht["platform_weibo_id"] == null ? "" : (string)ht["platform_weibo_id"];
            model.platform_weibo_key = ht["platform_weibo_key"] == null ? "" : (string)ht["platform_weibo_key"];
            model.platform_weibo_image = ht["platform_weibo_image"] == null ? "" : (string)ht["platform_weibo_image"];
            model.platform_weibo_adduser = ht["platform_weibo_adduser"] == null ? "1" : (string)ht["platform_weibo_adduser"];
            model.platform_taobao_image = ht["platform_taobao_image"] == null ? "" : (string)ht["platform_taobao_image"];
            model.platform_taobao_key = ht["platform_taobao_key"] == null ? "" : (string)ht["platform_taobao_key"];
            model.platform_taobao_secret = ht["platform_taobao_secret"] == null ? "" : (string)ht["platform_taobao_secret"];
            model.platform_taobao_sessionkey = ht["platform_taobao_sessionkey"] == null ? "" : (string)ht["platform_taobao_sessionkey"];
            model.platform_taobao_shopnick = ht["platform_taobao_shopnick"] == null ? "" : (string)ht["platform_taobao_shopnick"];
            model.platform_taobao_adduser = ht["platform_taobao_adduser"] == null ? "1" : (string)ht["platform_taobao_adduser"];
            model.platform_facebook_secret = ht["platform_facebook_secret"] == null ? "" : (string)ht["platform_facebook_secret"];
            model.platform_facebook_id = ht["platform_facebook_id"] == null ? "" : (string)ht["platform_facebook_id"];
            model.platform_facebook_image = ht["platform_facebook_image"] == null ? "" : (string)ht["platform_facebook_image"];
            model.platform_facebook_adduser = ht["platform_facebook_adduser"] == null ? "1" : (string)ht["platform_facebook_adduser"];
            model.platform_twitter_image = ht["platform_twitter_image"] == null ? "" : (string)ht["platform_twitter_image"];
            model.platform_twitter_key = ht["platform_twitter_key"] == null ? "" : (string)ht["platform_twitter_key"];
            model.platform_twitter_secret = ht["platform_twitter_secret"] == null ? "" : (string)ht["platform_twitter_secret"];
            model.platform_twitter_adduser = ht["platform_twitter_adduser"] == null ? "1" : (string)ht["platform_twitter_adduser"];
            model.platform_weixin_number = ht["platform_weixin_number"] == null ? "" : (string)ht["platform_weixin_number"];
            model.platform_weixin_id = ht["platform_weixin_id"] == null ? "" : (string)ht["platform_weixin_id"];
            model.platform_weixin_image = ht["platform_weixin_image"] == null ? "" : (string)ht["platform_weixin_image"];
            model.platform_weixin_image_qrcode = ht["platform_weixin_image_qrcode"] == null ? "" : (string)ht["platform_weixin_image_qrcode"];
            model.platform_weixin_secret = ht["platform_weixin_secret"] == null ? "" : (string)ht["platform_weixin_secret"];
            model.platform_weixin_custemtoken = ht["platform_weixin_custemtoken"] == null ? "" : (string)ht["platform_weixin_custemtoken"];
            model.platform_weixin_subscribe_automsg = ht["platform_weixin_subscribe_automsg"] == null ? "" : (string)ht["platform_weixin_subscribe_automsg"];
            model.platform_weixin_adduser = ht["platform_weixin_adduser"] == null ? "1" : (string)ht["platform_weixin_adduser"];
            model.TakeMoneyLimit = ht["TakeMoneyLimit"] == null ? "100" : (string)ht["TakeMoneyLimit"];
            model.WithdrawalFeeRate = ht["WithdrawalFeeRate"] == null ? "0" : (string)ht["WithdrawalFeeRate"];
            model.AdminLanguages = ht["AdminLanguages"] == null ? "" : (string)ht["AdminLanguages"];
            model.OrderReceivedDays = ht["OrderReceivedDays"] == null ? "0" : (string)ht["OrderReceivedDays"];
            model.OrderCompleteDays = ht["OrderCompleteDays"] == null ? "0" : (string)ht["OrderCompleteDays"];
            model.APIPassWord = ht["APIPassWord"] == null ? "" : (string)ht["APIPassWord"];
            model.LebiAPI = ht["LebiAPI"] == null ? "" : (string)ht["LebiAPI"];
            model.HTTPServer = ht["HTTPServer"] == null ? "http" : (string)ht["HTTPServer"];
            model.TaxRate = ht["TaxRate"] == null ? "0" : (string)ht["TaxRate"];
            //开关
            model.IsAnonymousUser = ht["IsAnonymousUser"] == null ? "0" : (string)ht["IsAnonymousUser"];
            model.TopAreaid = ht["TopAreaid"] == null ? "0" : (string)ht["TopAreaid"];
            model.IsOpenPaidOrderConfirm = ht["IsOpenPaidOrderConfirm"] == null ? "0" : (string)ht["IsOpenPaidOrderConfirm"];
            model.IsReduceBasketStep = ht["IsReduceBasketStep"] == null ? "0" : (string)ht["IsReduceBasketStep"];
            model.TopAreaid = model.TopAreaid == "" ? "0" : model.TopAreaid;
            model.IsNullStockDown = ht["IsNullStockDown"] == null ? "0" : (string)ht["IsNullStockDown"];
            model.IsNullStockSale = ht["IsNullStockSale"] == null ? "0" : (string)ht["IsNullStockSale"];
            model.IsPointToMoney = ht["IsPointToMoney"] == null ? "0" : (string)ht["IsPointToMoney"];
            model.PluginUsed = ht["PluginUsed"] == null ? "" : (string)ht["PluginUsed"];
            model.IsClosetuihuo = ht["IsClosetuihuo"] == null ? "0" : (string)ht["IsClosetuihuo"];
            model.IsSupplierCash = ht["IsSupplierCash"] == null ? "0" : (string)ht["IsSupplierCash"];
            model.IsOpenUserEnd = ht["IsOpenUserEnd"] == null ? "0" : (string)ht["IsOpenUserEnd"];
            model.UserRegCheckedType = ht["UserRegCheckedType"] == null ? "" : (string)ht["UserRegCheckedType"];
            model.DefaultUserEndDays = ht["DefaultUserEndDays"] == null ? "0" : (string)ht["DefaultUserEndDays"];
            model.IsMutiCurrencyShow = ht["IsMutiCurrencyShow"] == null ? "0" : (string)ht["IsMutiCurrencyShow"];
            model.ProductStockFreezeTime = ht["ProductStockFreezeTime"] == null ? "orderconfirm" : (string)ht["ProductStockFreezeTime"];
            model.IsAllowOutSideAjax = ht["IsAllowOutSideAjax"] == null ? "0" : (string)ht["IsAllowOutSideAjax"];
            model.SafeIPs = ht["SafeIPs"] == null ? "" : (string)ht["SafeIPs"];
            model.ProductLimitType = ht["ProductLimitType"] == null ? "0" : (string)ht["ProductLimitType"];
            model.IntOrderMoney = ht["IntOrderMoney"] == null ? "0" : (string)ht["IntOrderMoney"];
            //代理
            model.Angent_Commission = ht["Angent_Commission"] == null ? "" : (string)ht["Angent_Commission"];
            model.Angent_Commission_require = ht["Angent_Commission_require"] == null ? "" : (string)ht["Angent_Commission_require"];
            model.Angent1_Commission = ht["Angent1_Commission"] == null ? "" : (string)ht["Angent1_Commission"];
            model.Angent2_Commission = ht["Angent2_Commission"] == null ? "" : (string)ht["Angent2_Commission"];
            model.Angent3_Commission = ht["Angent3_Commission"] == null ? "" : (string)ht["Angent3_Commission"];
            model.IsUsedAgent = ht["IsUsedAgent"] == null ? "0" : (string)ht["IsUsedAgent"];
            model.IsUsedAgent_Area = ht["IsUsedAgent_Area"] == null ? "0" : (string)ht["IsUsedAgent_Area"];
            model.IsUsedAgent_Product = ht["IsUsedAgent_Product"] == null ? "0" : (string)ht["IsUsedAgent_Product"];
            model.CommissionMoneyDays = ht["CommissionMoneyDays"] == null ? "0" : (string)ht["CommissionMoneyDays"];
            model.AgentEndDays = ht["AgentEndDays"] == null ? "0" : (string)ht["AgentEndDays"];
            //手机短信
            model.SMSTPL_newuser = ht["SMSTPL_newuser"] == null ? "" : (string)ht["SMSTPL_newuser"];
            model.SMSTPL_ordersubmit = ht["SMSTPL_ordersubmit"] == null ? "" : (string)ht["SMSTPL_ordersubmit"];
            model.SMSTPL_ordershipping = ht["SMSTPL_ordershipping"] == null ? "" : (string)ht["SMSTPL_ordershipping"];
            model.SMSTPL_balance = ht["SMSTPL_balance"] == null ? "" : (string)ht["SMSTPL_balance"];
            model.SMSTPL_getpwd = ht["SMSTPL_getpwd"] == null ? "" : (string)ht["SMSTPL_getpwd"];
            model.SMSTPL_getnewpwd = ht["SMSTPL_getnewpwd"] == null ? "" : (string)ht["SMSTPL_getnewpwd"];
            model.SMSTPL_comment = ht["SMSTPL_comment"] == null ? "" : (string)ht["SMSTPL_comment"];
            model.SMSTPL_ask = ht["SMSTPL_ask"] == null ? "" : (string)ht["SMSTPL_ask"];
            model.SMSTPL_message = ht["SMSTPL_message"] == null ? "" : (string)ht["SMSTPL_message"];
            model.SMSTPL_checkcode = ht["SMSTPL_checkcode"] == null ? "" : (string)ht["SMSTPL_checkcode"];
            model.SMSTPL_Admin_newuser = ht["SMSTPL_Admin_newuser"] == null ? "" : (string)ht["SMSTPL_Admin_newuser"];
            model.SMSTPL_Admin_ordersubmit = ht[""] == null ? "" : (string)ht["SMSTPL_Admin_ordersubmit"];
            model.SMSTPL_Admin_orderrecive = ht["SMSTPL_Admin_orderrecive"] == null ? "" : (string)ht["SMSTPL_Admin_orderrecive"];
            model.SMSTPL_Admin_ordercomment = ht["SMSTPL_Admin_ordercomment"] == null ? "" : (string)ht["SMSTPL_Admin_ordercomment"];
            model.SMSTPL_Admin_ordersubmit = ht["SMSTPL_Admin_ordersubmit"] == null ? "" : (string)ht["SMSTPL_Admin_ordersubmit"];
            model.SMSTPL_Admin_inquiry = ht["SMSTPL_Admin_inquiry"] == null ? "" : (string)ht["SMSTPL_Admin_inquiry"];
            model.SMSTPL_Admin_comment = ht["SMSTPL_Admin_comment"] == null ? "" : (string)ht["SMSTPL_Admin_comment"];
            model.SMSTPL_Admin_ask = ht["SMSTPL_Admin_ask"] == null ? "" : (string)ht["SMSTPL_Admin_ask"];
            model.SMSTPL_Admin_message = ht["SMSTPL_Admin_message"] == null ? "" : (string)ht["SMSTPL_Admin_message"];
            model.SMS_user = ht["SMS_user"] == null ? "" : (string)ht["SMS_user"];
            model.SMS_password = ht["SMS_password"] == null ? "" : (string)ht["SMS_password"];
            model.SMS_server = ht["SMS_server"] == null ? "0" : (string)ht["SMS_server"];
            model.SMS_state = ht["SMS_state"] == null ? "0" : (string)ht["SMS_state"];
            model.SMS_apitype = ht["SMS_apitype"] == null ? "3" : (string)ht["SMS_apitype"];
            model.SMS_sendmode = ht["SMS_sendmode"] == null ? "" : (string)ht["SMS_sendmode"];
            model.SMS_reciveno = ht["SMS_reciveno"] == null ? "" : (string)ht["SMS_reciveno"];
            model.SMS_serverport = ht["SMS_serverport"] == null ? "0" : (string)ht["SMS_serverport"];
            model.SMS_maxlen = ht["SMS_maxlen"] == null ? "" : (string)ht["SMS_maxlen"];
            model.SMS_lastmsg = ht["SMS_lastmsg"] == null ? "" : (string)ht["SMS_lastmsg"];
            model.SMS_httpapi = ht["SMS_httpapi"] == null ? "" : (string)ht["SMS_httpapi"];
            model.IsMobilePhoneMutiReg = ht["IsMobilePhoneMutiReg"] == null ? "0" : (string)ht["IsMobilePhoneMutiReg"];
            //退税
            model.Refund_StepR = ht["Refund_StepR"] == null ? "" : (string)ht["Refund_StepR"];
            model.Refund_MinMoney = ht["Refund_MinMoney"] == null ? "0" : (string)ht["Refund_MinMoney"];
            model.Refund_VAT = ht["Refund_VAT"] == null ? "0" : (string)ht["Refund_VAT"];
            //新事件
            model.NewEvent_Order_IsVerified = ht["NewEvent_Order_IsVerified"] == null ? "" : (string)ht["NewEvent_Order_IsVerified"];
            model.NewEvent_Order_IsPaid = ht["NewEvent_Order_IsPaid"] == null ? "" : (string)ht["NewEvent_Order_IsPaid"];
            model.NewEvent_Order_IsShipped = ht["NewEvent_Order_IsShipped"] == null ? "" : (string)ht["NewEvent_Order_IsShipped"];
            //验证码
            model.Verifycode_UserRegister = ht["Verifycode_UserRegister"] == null ? "0" : (string)ht["Verifycode_UserRegister"];
            model.Verifycode_UserLogin = ht["Verifycode_UserLogin"] == null ? "0" : (string)ht["Verifycode_UserLogin"];
            model.Verifycode_ForgetPassword = ht["Verifycode_ForgetPassword"] == null ? "0" : (string)ht["Verifycode_ForgetPassword"];
            model.Verifycode_SupplierRegister = ht["Verifycode_SupplierRegister"] == null ? "0" : (string)ht["Verifycode_SupplierRegister"];
            model.Verifycode_SupplierLogin = ht["Verifycode_SupplierLogin"] == null ? "0" : (string)ht["Verifycode_SupplierLogin"];
            model.Verifycode_AdminLogin = ht["Verifycode_AdminLogin"] == null ? "0" : (string)ht["Verifycode_AdminLogin"];
            //APP推送
            model.APPPush_state = ht["APPPush_state"] == null ? "0" : (string)ht["APPPush_state"];
            model.APPPush_sendmode = ht["APPPush_sendmode"] == null ? "" : (string)ht["APPPush_sendmode"];
            //APP分享配置
            model.app_share = ht["app_share"] == null ? "" : (string)ht["app_share"];
            model.app_share_wechat_key = ht["app_share_wechat_key"] == null ? "" : (string)ht["app_share_wechat_key"];
            model.app_share_wechat_secret = ht["app_share_wechat_secret"] == null ? "" : (string)ht["app_share_wechat_secret"];
            model.app_share_qq_key = ht["app_share_qq_key"] == null ? "" : (string)ht["app_share_qq_key"];
            model.app_share_qq_secret = ht["app_share_qq_secret"] == null ? "" : (string)ht["app_share_qq_secret"];
            model.app_share_dingtalk_key = ht["app_share_dingtalk_key"] == null ? "" : (string)ht["app_share_dingtalk_key"];
            model.app_share_dingtalk_secret = ht["app_share_dingtalk_secret"] == null ? "" : (string)ht["app_share_dingtalk_secret"];
            //面板
            model.system_layout_logo = ht["system_layout_logo"] == null ? "/theme/system/systempage/admin2/assets/images/logo.gif" : (string)ht["system_layout_logo"];
            model.system_login_logo = ht["system_layout_logo"] == null ? "/theme/system/systempage/admin2/assets/images/lebi.gif" : (string)ht["system_login_logo"];
            model.system_login_background = ht["system_layout_logo"] == null ? "/theme/system/systempage/admin2/assets/images/background/login.jpg" : (string)ht["system_login_background"];
            return model;
        }
        public bool SaveConfig(BaseConfig model)
        {
            //return SerializationHelper.Save(model, this.xmlpath);
            Type type = model.GetType();
            Lebi_Config cf;
            foreach (System.Reflection.PropertyInfo p in type.GetProperties())
            {
                if (p.GetValue(model, null) == null)
                    continue;
                cf = B_Lebi_Config.GetModel("Name='" + p.Name + "'");
                if (cf == null)
                {
                    cf = new Lebi_Config();
                    cf.Name = p.Name;
                    cf.Value = p.GetValue(model, null).ToString();
                    B_Lebi_Config.Add(cf);
                }
                else
                {
                    cf.Name = p.Name;
                    cf.Value = p.GetValue(model, null).ToString();
                    B_Lebi_Config.Update(cf);

                }

            }
            ShopCache.SetBaseConfig();//更新缓存
            return true;
        }

        /// <summary>
        /// 获取一个设置值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key)
        {
            Lebi_Config conf = B_Lebi_Config.GetModel("Name='" + key + "'");
            if (conf == null)
                return "";
            return conf.Value;
        }
        /// <summary>
        /// 设置一个设置值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set(string key, string value)
        {
            Lebi_Config conf = B_Lebi_Config.GetModel("Name='" + key + "'");
            if (conf == null)
            {
                conf = new Lebi_Config();
                conf.Name = key;
                conf.Value = value;
                B_Lebi_Config.Add(conf);
            }
            else
            {
                conf.Value = value;
                B_Lebi_Config.Update(conf);
            }
        }
        /// <summary>
        /// 反序列化退税函数率
        /// </summary>
        /// <param name="pricestr"></param>
        /// <returns></returns>
        public static List<BaseConfigStepR> StepR(string pricestr)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<BaseConfigStepR> models = jss.Deserialize<List<BaseConfigStepR>>(pricestr);
            return models;
        }

        /// <summary>
        /// 反序列化app菜单
        /// </summary>
        /// <param name="pricestr"></param>
        /// <returns></returns>
        public static List<BaseConfigAppMenu> AppMenu(string str)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            List<BaseConfigAppMenu> models = jss.Deserialize<List<BaseConfigAppMenu>>(str);
            return models;
        }
    }
}