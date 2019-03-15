using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using com.todaynic.ScpClient;
using cn.eibei.xml;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Net;
using System.Threading;
namespace Shop.Bussiness
{
    public class SMS
    {
        //protected SMSClient smsClient;
        public static void Send(string to, string body)
        {
            Thread thread = new Thread(() => ThreadSend(to, body));
            thread.IsBackground = true;
            thread.Start();
        }
        public static bool ThreadSend(string to, string body)
        {
            BaseConfig model = ShopCache.GetBaseConfig();
            string MobilePhone = to;
            string Msg = body;
            bool flag;
            if (model.SMS_state == "0")
                return false;
            if (MobilePhone == "")
                return false;
            //if (MobilePhone.Length != 11)
            //    return false;
            body = body + model.SMS_lastmsg;
            try
            {
                if (model.SMS_server == "0")
                {
                    //时代互联通道
                    SMSClient smsClient = new SMSClient("sms.todaynic.com", Convert.ToInt32(model.SMS_serverport), model.SMS_user, model.SMS_password);
                    flag = smsClient.sendSMS(MobilePhone, Msg, model.SMS_apitype);
                    if (flag)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (model.SMS_server == "1")
                {
                    //yunsms.cn通道
                    string pwd = MD5(model.SMS_password);	//密码MD5
                    string uid = model.SMS_user;		//用户名
                    StringBuilder sbTemp = new StringBuilder();
                    sbTemp.Append("uid=" + uid + "&pwd=" + pwd + "&mobile=" + to + "&content=" + body);
                    byte[] bTemp = System.Text.Encoding.GetEncoding("GBK").GetBytes(sbTemp.ToString());
                    string postReturn = SMS.doPostRequest("http://http.yunsms.cn/tx/", bTemp);
                    if (postReturn == "100")
                        return true;
                    return false;
                }
                else if (model.SMS_server == "999")
                {
                    //通用接口
                    //"http://sms.soe.wang:8009/sys_port/gateway/?id=" + uid + "&pwd=" + pwd + "&to=" + mob + "&content=" + msg + "&time=";
                    string url = model.SMS_httpapi.ToLower().Replace("{$username}", model.SMS_user);
                    url = url.Replace("{$password}", model.SMS_password);
                    url = url.Replace("{$phonenumber}", MobilePhone);
                    url = url.Replace("{$content}", System.Web.HttpUtility.UrlEncode(Msg, System.Text.Encoding.GetEncoding("GB2312")));
                    url = url.Replace("{$content_utf-8}", System.Web.HttpUtility.UrlEncode(Msg, System.Text.Encoding.GetEncoding("UTF-8")));
                    string res = doGetRequest(url);
                    return true;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #region yunsms.cn方法
        private static String doPostRequest(string url, byte[] bData)
        {
            System.Net.HttpWebRequest hwRequest;
            System.Net.HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "POST";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
                hwRequest.ContentLength = bData.Length;

                System.IO.Stream smWrite = hwRequest.GetRequestStream();
                smWrite.Write(bData, 0, bData.Length);
                smWrite.Close();
            }
            catch (System.Exception err)
            {
                //WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                //WriteErrLog(err.ToString());
            }

            return strResult;
        }
        //GET方式发送得结果
        private static String doGetRequest(string url)
        {
            HttpWebRequest hwRequest;
            HttpWebResponse hwResponse;

            string strResult = string.Empty;
            try
            {
                hwRequest = (System.Net.HttpWebRequest)WebRequest.Create(url);
                hwRequest.Timeout = 5000;
                hwRequest.Method = "GET";
                hwRequest.ContentType = "application/x-www-form-urlencoded";
            }
            catch (System.Exception err)
            {
                //WriteErrLog(err.ToString());
                return strResult;
            }

            //get response
            try
            {
                hwResponse = (HttpWebResponse)hwRequest.GetResponse();
                StreamReader srReader = new StreamReader(hwResponse.GetResponseStream(), Encoding.ASCII);
                strResult = srReader.ReadToEnd();
                srReader.Close();
                hwResponse.Close();
            }
            catch (System.Exception err)
            {
                //WriteErrLog(err.ToString());
            }

            return strResult;
        }
        private static string MD5(string pwd)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.Default.GetBytes(pwd);
            byte[] md5data = md5.ComputeHash(data);
            md5.Clear();
            string str = "";
            for (int i = 0; i < md5data.Length; i++)
            {
                str += md5data[i].ToString("x").PadLeft(2, '0');

            }
            return str;
        }

        #endregion

        #region 发送内容
        /// <summary>
        /// 添加邮件发送记录
        /// </summary>
        public static void SMSJob(string Emailto, string Content, bool sendstatus, Lebi_User user, BaseConfig conf)
        {

            Lebi_Email log = new Lebi_Email();
            int Type_id_EmailStatus = 270;
            if (sendstatus)
                Type_id_EmailStatus = 271;
            else
            {
                int Mail_SendTop = 0;
                int.TryParse(conf.Mail_SendTop, out Mail_SendTop);
                if (Mail_SendTop < 1)
                {
                    Type_id_EmailStatus = 272;
                }
            }
            log.Content = Content;
            log.Count_send = 1;
            log.Email = Emailto;
            log.Type_id_EmailStatus = Type_id_EmailStatus;
            log.User_id = user.id;
            log.User_Name = user.UserName;
            B_Lebi_Email.Add(log);

        }
        /// <summary>
        /// 新会员注册
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_newuser(Lebi_User user)
        {
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_newuser"))
            {
                string content = Language.Content(conf.SMSTPL_newuser, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                Send(user.MobilePhone, content);
                //SMSJob(user.MobilePhone, content, flag, user, conf);
            }
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_newuser"))
            {
                string Admin_content = Language.Content(conf.SMSTPL_Admin_newuser, user.Language);
                Admin_content = ReplaceSMSTag(Admin_content, user, conf);
                Send(conf.SMS_reciveno, Admin_content);
                //SMSJob(conf.SMS_reciveno, Admin_content, Admin_flag, user, conf);
            }
        }
        /// <summary>
        /// 订单提交
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        public static void SendSMS_ordersubmit(Lebi_User user, Lebi_Order order)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            int IsUserSms = 0;
            int IsUserSms2 = 0;
            int IsAdminSms = 0;
            int IsSupplierSms = 0;
            if (conf.SMS_sendmode.Contains("SMSTPL_ordersubmit"))
            {
                IsUserSms = 1;
            }
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_ordersubmit"))
            {
                IsAdminSms = 1;
            }
            if (conf.SMS_sendmode.Contains("SMSTPL_orderpickup"))
            {
                IsUserSms2 = 1;
            }
            if (order.Supplier_id > 0)
            {
                if (ShopCache.GetBaseConfig_Supplier(order.Supplier_id).SMS_sendmode.Contains("SMSTPL_Admin_ordersubmit"))
                {
                    IsSupplierSms = 1;
                }
            }
            if (IsUserSms == 0 && IsUserSms2 == 0 && IsAdminSms == 0 && IsSupplierSms == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string list = "";
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
            foreach (Lebi_Order_Product pro in pros)
            {
                list += "" + Language.Tag("商品编号", user.Language) + "：" + pro.Product_Number + "；" + Language.Tag("商品", user.Language) + "：" + Language.Content(pro.Product_Name, user.Language) + "；" + Language.Tag("数量", user.Language) + "：" + pro.Count + "；";
            }
            if (IsUserSms == 1)
            {
                string content = Language.Content(conf.SMSTPL_ordersubmit, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$Money}", Language.FormatMoney(order.Money_Order, order.Currency_Code));
                content = content.Replace("{$Order}", list);
                Send(order.T_MobilePhone, content);
            }
            if (IsAdminSms == 1 || IsSupplierSms == 1)
            {
                string Admin_content = Language.Content(conf.SMSTPL_Admin_ordersubmit, user.Language);
                Admin_content = ReplaceSMSTag(Admin_content, user, conf);
                Admin_content = Admin_content.Replace("{$OrderNO}", order.Code);
                Admin_content = Admin_content.Replace("{$Money}", Language.FormatMoney(order.Money_Order, order.Currency_Code));
                Admin_content = Admin_content.Replace("{$Order}", list);
                string transport = "order.Transport_Name";
                if (order.PickUp_Name != "")
                    transport += "|" + order.PickUp_Name + "|" + order.PickUp_Date.ToString("yyyy-MM-dd");
                Admin_content = Admin_content.Replace("{$Transport}", transport);
                if (IsAdminSms == 1)
                {
                    Send(conf.SMS_reciveno, Admin_content);
                }
                if (IsSupplierSms == 1)
                {
                    BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(order.Supplier_id);
                    Send(config_supplier.SMS_reciveno, Admin_content);
                }
            }
            if (IsUserSms2 == 1)
            {
                string content = Language.Content(conf.SMSTPL_orderpickup, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$Money}", Language.FormatMoney(order.Money_Order, order.Currency_Code));
                content = content.Replace("{$Order}", list);
                content = content.Replace("{$PickUpStation}", order.PickUp_Name);
                content = content.Replace("{$PickUpTime}", order.PickUp_Date.ToString("yyyy-MM-dd"));
                Send(order.T_MobilePhone, content);
            }
        }
        /// <summary>
        /// 订单付款
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        public static void SendSMS_orderpaid(Lebi_User user, Lebi_Order order)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            int IsAdminSms = 0;
            int IsSupplierSms = 0;
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_orderpaid"))
            {
                IsAdminSms = 1;
            }
            if (order.Supplier_id > 0)
            {
                if (ShopCache.GetBaseConfig_Supplier(order.Supplier_id).SMS_sendmode.Contains("SMSTPL_Admin_orderpaid"))
                {
                    IsSupplierSms = 1;
                }
            }
            if (IsAdminSms == 0 && IsSupplierSms == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string Admin_content = Language.Content(conf.SMSTPL_Admin_orderpaid, user.Language);
            Admin_content = ReplaceSMSTag(Admin_content, user, conf);
            Admin_content = Admin_content.Replace("{$OrderNO}", order.Code);
            if (IsAdminSms == 1)
            {
                Send(conf.SMS_reciveno, Admin_content);
            }
            if (IsSupplierSms == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(order.Supplier_id);
                Send(config_supplier.SMS_reciveno, Admin_content);
            }
        }
        /// <summary>
        /// 订单收货
        /// </summary>
        /// <param name="user"></param>
        /// <param name="torder"></param>
        public static void SendSMS_orderrecive(Lebi_User user, Lebi_Transport_Order torder)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            int IsAdminSms = 0;
            int IsSupplierSms = 0;
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_orderrecive"))
            {
                IsAdminSms = 1;
            }
            Lebi_Order order = B_Lebi_Order.GetModel("id=" + torder.Order_id + "");
            if (order.Supplier_id > 0)
            {
                if (ShopCache.GetBaseConfig_Supplier(order.Supplier_id).SMS_sendmode.Contains("SMSTPL_Admin_orderrecive"))
                {
                    IsSupplierSms = 1;
                }
            }
            if (IsAdminSms == 0 && IsSupplierSms == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string Admin_content = Language.Content(conf.SMSTPL_Admin_orderrecive, user.Language);
            Admin_content = ReplaceSMSTag(Admin_content, user, conf);
            Admin_content = Admin_content.Replace("{$OrderNO}", order.Code);
            if (IsAdminSms == 1)
            {
                Send(conf.SMS_reciveno, Admin_content);
            }
            if (IsSupplierSms == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(order.Supplier_id);
                Send(config_supplier.SMS_reciveno, Admin_content);
            }
        }
        /// <summary>
        /// 预定商品到货
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="pro"></param>
        public static void SendSMS_reserveok(Lebi_User user, Lebi_Order order, Lebi_Order_Product pro)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_reserveok"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_reserveok, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$OrderNO}", order.Code);
                //content = content.Replace("{$OrderNO}", order.Code);
                Send(user.MobilePhone, content);
            }
        }
        /// <summary>
        /// 订单发货
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="torder"></param>
        public static void SendSMS_ordershipping(Lebi_User user, Lebi_Order order, Lebi_Transport_Order torder)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_ordershipping"))
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
                Send(order.T_MobilePhone, content);
            }
        }
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_forgetpwd(Lebi_User user)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_getpwd"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_getpwd, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                Send(user.MobilePhone, content);
            }
        }
        /// <summary>
        /// 获取新密码
        /// </summary>
        /// <param name="user"></param>
        /// <param name="Password"></param>
        public static void SendSMS_newpwd(Lebi_User user, string Password)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_getnewpwd"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_getnewpwd, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Password}", Password);
                Send(user.MobilePhone, content);
            }
        }
        /// <summary>
        /// 余额提醒
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_balance(Lebi_User user)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_balance"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_balance, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Money}", Math.Round(user.Money, 2).ToString());
                Send(user.MobilePhone, content);
            }
        }
        /// <summary>
        /// 留言反馈
        /// </summary>
        /// <param name="model"></param>
        public static void SendSMS_inquiry(Lebi_Inquiry model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_inquiry"))
            {
                Lebi_User user = B_Lebi_User.GetModel(0);
                if (user == null)
                    user = new Lebi_User();
                string list = "" + Language.Tag("昵称", model.Language) + "：" + model.UserName + "<br />" + Language.Tag("电话", model.Language) + "：" + model.Phone + "<br />" + Language.Tag("EMAIL", model.Language) + "：" + model.Email + "<br />" + Language.Tag("标题", model.Language) + "：" + model.Subject + "<br />" + Language.Tag("内容", model.Language) + "：" + model.Content + "<br />" + Language.Tag("时间", model.Language) + "：" + model.Time_Add + "";
                string Admin_content = Language.Content(conf.SMSTPL_Admin_inquiry, model.Language);
                Admin_content = ReplaceSMSTag(Admin_content, user, conf);
                Admin_content = Admin_content.Replace("{$Title}", model.Subject);
                Admin_content = Admin_content.Replace("{$Content}", list);
                Send(conf.SMS_reciveno, Admin_content);
            }
        }
        /// <summary>
        /// 商品评论
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void SendSMS_comment(Lebi_User user, Lebi_Comment model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            int IsAdminSms = 0;
            int IsSupplierSms = 0;
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_comment"))
            {
                IsAdminSms = 1;
            }
            Lebi_Product product = B_Lebi_Product.GetModel(model.Product_id);
            if (product == null)
                product = new Lebi_Product();
            if (product.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(product.Supplier_id);
                if (supplier.IsSupplierTransport == 0)  //商家发货
                {
                    if (ShopCache.GetBaseConfig_Supplier(product.Supplier_id).SMS_sendmode.Contains("SMSTPL_Admin_comment"))
                    {
                        IsSupplierSms = 1;
                    }
                }
            }
            if (IsAdminSms == 0 && IsSupplierSms == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string list = "" + Language.Tag("商品名称", user.Language) + "：" + Language.Content(product.Name, user.Language) + "；" + Language.Tag("评分", user.Language) + "：" + model.Star + "；" + Language.Tag("内容", user.Language) + "：" + model.Content + "；";
            string Admin_content = Language.Content(conf.SMSTPL_Admin_comment, user.Language);
            Admin_content = ReplaceSMSTag(Admin_content, user, conf);
            Admin_content = Admin_content.Replace("{$Content}", list);
            if (IsAdminSms == 1)
            {
                Send(conf.SMS_reciveno, Admin_content);
            }
            if (IsSupplierSms == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(product.Supplier_id);
                Send(config_supplier.SMS_reciveno, Admin_content);
            }
        }
        /// <summary>
        /// 商品评论管理员回复
        /// </summary>
        /// <param name="model"></param>
        public static void SendSMS_commentreply(Lebi_Comment model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_comment"))
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_comment, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Content}", model.Content);
                Send(user.MobilePhone, content);
            }
        }
        /// <summary>
        /// 商品咨询
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void SendSMS_ask(Lebi_User user, Lebi_Comment model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            int IsAdminSms = 0;
            int IsSupplierSms = 0;
            if (conf.SMS_sendmode.Contains("SMSTPL_ask"))
            {
                IsAdminSms = 1;
            }
            Lebi_Product product = B_Lebi_Product.GetModel(model.Product_id);
            if (product == null)
                product = new Lebi_Product();
            if (product.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(product.Supplier_id);
                if (supplier.IsSupplierTransport == 0)  //商家发货
                {
                    if (ShopCache.GetBaseConfig_Supplier(product.Supplier_id).SMS_sendmode.Contains("SMSTPL_ask"))
                    {
                        IsSupplierSms = 1;
                    }
                }
            }
            if (IsAdminSms == 0 && IsSupplierSms == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string list = "" + Language.Tag("商品名称", user.Language) + "：" + Language.Content(product.Name, user.Language) + "；" + Language.Tag("内容", user.Language) + "：" + model.Content + "；";
            string Admin_content = Language.Content(conf.SMSTPL_Admin_ask, user.Language);
            Admin_content = ReplaceSMSTag(Admin_content, user, conf);
            Admin_content = Admin_content.Replace("{$Content}", list);
            if (IsAdminSms == 1)
            {
                Send(conf.SMS_reciveno, Admin_content);
            }
            if (IsSupplierSms == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(product.Supplier_id);
                Send(config_supplier.SMS_reciveno, Admin_content);
            }
        }
        /// <summary>
        /// 商品咨询管理员回复
        /// </summary>
        /// <param name="model"></param>
        public static void SendSMS_askreply( Lebi_Comment model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_ask"))
            {
                Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string content = Language.Content(conf.SMSTPL_ask, user.Language);
                content = ReplaceSMSTag(content, user, conf);
                content = content.Replace("{$Content}", model.Content);
                Send(user.MobilePhone, content);
            }
        }
        /// <summary>
        /// 站内信
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_message(Lebi_User user, Lebi_Message model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_message"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                Lebi_Message_Type type = B_Lebi_Message_Type.GetModel(model.Message_Type_id);
                if (type == null)
                    type = new Lebi_Message_Type();
                string list = "" + Language.Tag("类型", user.Language) + "：" + Language.Content(type.Name, user.Language) + "；" + Language.Tag("标题", user.Language) + "：" + model.Title + "；" + Language.Tag("内容", user.Language) + "：" + model.Content + "；";

                string Admin_content = Language.Content(conf.SMSTPL_Admin_message, user.Language);
                Admin_content = ReplaceSMSTag(Admin_content, user, conf);
                Admin_content = Admin_content.Replace("{$Title}", model.Title);
                Admin_content = Admin_content.Replace("{$Content}", list);
                Send(conf.SMS_reciveno, Admin_content);
            }
        }
        /// <summary>
        /// 站内信管理员回复
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_messagereply(Lebi_Message model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.SMS_sendmode.Contains("SMSTPL_message"))
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
                Send(user.MobilePhone, content);
            }
        }
        /// <summary>
        /// 订单留言
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_ordercomment(Lebi_User user, Lebi_Comment model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            int IsAdminSms = 0;
            int IsSupplierSms = 0;
            if (conf.SMS_sendmode.Contains("SMSTPL_Admin_ordercomment"))
            {
                IsAdminSms = 1;
            }
            Lebi_Order order = B_Lebi_Order.GetModel(model.Keyid);
            if (order == null)
                order = new Lebi_Order();
            if (order.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
                if (supplier.IsSupplierTransport == 0)  //商家发货
                {
                    if (ShopCache.GetBaseConfig_Supplier(order.Supplier_id).SMS_sendmode.Contains("SMSTPL_Admin_ordercomment"))
                    {
                        IsSupplierSms = 1;
                    }
                }
            }
            if (IsAdminSms == 0 && IsSupplierSms == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string Admin_content = Language.Content(conf.SMSTPL_Admin_ordercomment, user.Language);
            Admin_content = ReplaceSMSTag(Admin_content, user, conf);
            Admin_content = Admin_content.Replace("{$Content}", model.Content);
            Admin_content = Admin_content.Replace("{$OrderNO}", order.Code);
            if (IsAdminSms == 1)
            {
                Send(conf.SMS_reciveno, Admin_content);
            }
            if (IsSupplierSms == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(order.Supplier_id);
                Send(config_supplier.SMS_reciveno, Admin_content);
            }
        }
        /// <summary>
        /// 发送手机验证码
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_checkcode(Lebi_User user)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            string content = Language.Content(conf.SMSTPL_checkcode, user.Language);
            string code = Common.GetRnd(6, true, false, false, false, "");
            HttpContext.Current.Session["phonecheckcode"] = user.MobilePhone + code;
            content = ReplaceSMSTag(content, user, conf);
            content = content.Replace("{$CheckCode}", code);
            Send(user.MobilePhone, content);
        }
        /// <summary>
        /// 自定义内容
        /// </summary>
        /// <param name="user"></param>
        public static void SendSMS_custom(string SMScontent, string SMSphone)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            string content = SMScontent;
            content = ReplaceSMSTag(content, null, conf);
            Send(SMSphone, content);
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
            }else
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

        #endregion
    }

}

