using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;
using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.Web;
using System.Security.Cryptography.X509Certificates;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using System.Security.Authentication;
using System.Net;
using System.Configuration;
using System.Threading;
namespace Shop.Bussiness
{
    public class APP
    {
        public static void Push(string system, string token, string msg)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(msg))
            {
                return;
            }
            Thread thread = new Thread(() => ThreadPush(system, token, msg));
            thread.IsBackground = true;
            thread.Start();
        }
        public static void ThreadPush(string system, string token, string msg)
        {
            BaseConfig model = ShopCache.GetBaseConfig();
            if (model.APPPush_state == "1")
            {
                if (system == "ios")
                {
                    ApplePush(token, msg);
                }
                else
                {
                    AndroidPush(token, msg);
                }
            }
        }
        public static void UserLastUse()
        {
            Shop.LebiAPI.Service.Instanse.UserLastUse();
        }
        public static string ApplePush(string token, string msg)
        {
            //SystemLog.Add("ApplePush: token=" + token + "&msg=" + msg + "");
            string url = RequestTool.GetConfigKey("ApplePushAddress");
            url = url + "?token=" + token + "&msg=" + msg;
            string res = HtmlEngine.Get(url);
            return res;

        }
        public static string AndroidPush(string token, string msg)
        {
            //SystemLog.Add("AndroidPush: user_id=" + token + "&msg=" + msg +"");
            string url = RequestTool.GetConfigKey("AndroidPushAddress");
            url = url + "?user_id=" + token + "&msg=" + msg;
            string res = HtmlEngine.Get(url);
            return res;

        }
        /// <summary>
        /// 预定商品到货
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="pro"></param>
        public static void Push_reserveok(Lebi_User user, Lebi_Order order, Lebi_Order_Product pro)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.APPPush_sendmode.Contains("reserveok"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_reserveok, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$OrderNO}", order.Code);
                //content = content.Replace("{$OrderNO}", order.Code);
                Push(user.Device_system, user.Device_id, content);
            }
        }
        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="torder"></param>
        public static void Push_ordershipping(Lebi_User user, Lebi_Order order, Lebi_Transport_Order torder)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.APPPush_sendmode.Contains("ordershipping"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string list = "";
                List<TransportProduct> tps = new List<TransportProduct>();
                JavaScriptSerializer jss = new JavaScriptSerializer();
                try
                {
                    tps = jss.Deserialize<List<TransportProduct>>(torder.Product);
                }
                catch (Exception)
                {
                    tps = new List<TransportProduct>();
                }
                foreach (TransportProduct pro in tps)
                {
                    list += "" + Language.Tag("商品编号", user.Language) + "：" + pro.Product_Number + "；" + Language.Tag("商品", user.Language) + "：" + Language.Content(pro.Product_Name, user.Language) + "；" + Language.Tag("发货数量", user.Language) + "：" + pro.Count + "；";
                }
                string content = Language.Content(conf.SMSTPL_ordershipping, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$ExpressCompany}", torder.Transport_Name);
                content = content.Replace("{$ExpressNumber}", torder.Code);
                content = content.Replace("{$Order}", list);
                Push(user.Device_system, user.Device_id, content);
            }
        }
        /// <summary>
        /// 余额提醒
        /// </summary>
        /// <param name="user"></param>
        public static void Push_balance(Lebi_User user)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.APPPush_sendmode.Contains("balance"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_balance, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Money}", Math.Round(user.Money, 2).ToString());
                Push(user.Device_system, user.Device_id, content);
            }
        }
        /// <summary>
        /// 商品评论管理员回复
        /// </summary>
        /// <param name="model"></param>
        public static void Push_commentreply(Lebi_Comment model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.APPPush_sendmode.Contains("comment"))
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_comment, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Content}", model.Content);
                Push(user.Device_system, user.Device_id, content);
            }
        }
        /// <summary>
        /// 商品咨询管理员回复
        /// </summary>
        /// <param name="model"></param>
        public static void Push_askreply(Lebi_Comment model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.APPPush_sendmode.Contains("ask"))
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_ask, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Content}", model.Content);
                Push(user.Device_system, user.Device_id, content);
            }
        }
        /// <summary>
        /// 站内信管理员回复
        /// </summary>
        /// <param name="user"></param>
        public static void Push_messagereply(Lebi_Message model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.APPPush_sendmode.Contains("message"))
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id_To);
                if (user == null)
                {
                    user.Language = Language.Languages().FirstOrDefault().Code;
                }
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_message, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Title}", model.Title);
                content = content.Replace("{$Content}", model.Content);
                Push(user.Device_system, user.Device_id, content);
            }
        }
        /// <summary>
        /// 自定义内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="Device_system"></param>
        /// <param name="Device_id"></param>
        public static void Push_custom(string content, string Device_system, string Device_id)
        {
            Push(Device_system, Device_id, content);
        }
        public static string ReplaceSMSTag(string instr, Lebi_User user, BaseConfig conf)
        {
            string language = Language.DefaultLanguage().Code;

            instr = instr.Replace("{$Domain}", conf.Domain);
            instr = instr.Replace("{$Time}", DateTime.Now.ToString());
            if (user != null)
            {
                instr = instr.Replace("{$UserName}", user.UserName);
                instr = instr.Replace("{$RealName}", user.RealName);
                instr = instr.Replace("{$UserID}", user.id.ToString());
                instr = instr.Replace("{$UserNumber}", user.UserNumber);
                instr = instr.Replace("{$NickName}", user.NickName);
                language = user.Language;
                if (language == "")
                    user.Language = "CN";
            }
            else
            {
                user = new Lebi_User();
                user.Language = "CN";
            }
            string Phone = "";
            string Email = "";
            string QQ = "";
            string Fax = "";
            string Name = "";
            string Domain = "";
            Lebi_Site site = B_Lebi_Site.GetModel(user.Site_id);
            if (site == null)
            {
                site = B_Lebi_Site.GetList("", "Sort desc").FirstOrDefault();
                Phone = Language.Content(site.Phone, user.Language);
                Email = Language.Content(site.Email, user.Language);
                QQ = Language.Content(site.QQ, user.Language);
                Fax = Language.Content(site.Fax, user.Language);
                Name = Language.Content(site.Name, user.Language);
                Domain = Language.Content(site.Domain, user.Language);

            }
            else
            {
                Phone = Language.Content(site.Phone, user.Language);
                Email = Language.Content(site.Email, user.Language);
                QQ = Language.Content(site.QQ, user.Language);
                Fax = Language.Content(site.Fax, user.Language);
                Name = Language.Content(site.Name, user.Language);
                Domain = Language.Content(site.Domain, user.Language);
                Lebi_Site mainsite = B_Lebi_Site.GetList("", "Sort desc").FirstOrDefault();
                if (Phone == "")
                    Phone = Language.Content(mainsite.Phone, user.Language);
                if (Email == "")
                    Email = Language.Content(mainsite.Email, user.Language);
                if (QQ == "")
                    QQ = Language.Content(mainsite.QQ, user.Language);
                if (Fax == "")
                    Fax = Language.Content(mainsite.Fax, user.Language);
                if (Name == "")
                    Name = Language.Content(mainsite.Name, user.Language);
                if (Domain == "")
                    Domain = Language.Content(mainsite.Domain, user.Language);
            }
            instr = instr.Replace("{$UserName}", user.UserName);
            instr = instr.Replace("{$UserID}", user.id.ToString());

            //instr = instr.Replace("{$Phone}", Language.Content(conf.Phone, user.Language));
            //instr = instr.Replace("{$Email}", Language.Content(conf.Email, user.Language));
            //instr = instr.Replace("{$QQ}", Language.Content(conf.QQ, user.Language));
            //instr = instr.Replace("{$Fax}", Language.Content(conf.Fax, user.Language));
            //instr = instr.Replace("{$SiteName}", Language.Content(conf.Name, user.Language));
            //instr = instr.Replace("{$Domain}", conf.Domain);
            instr = instr.Replace("{$Phone}", Phone);
            instr = instr.Replace("{$Email}", Email);
            instr = instr.Replace("{$QQ}", QQ);
            instr = instr.Replace("{$Fax}", Fax);
            instr = instr.Replace("{$SiteName}", Name);
            instr = instr.Replace("{$Domain}", Domain);
            return instr;
        }

    }

}

