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
using System.Web.Script.Serialization;
using System.Web;
using System.Threading;
namespace Shop.Bussiness
{
    public class Email
    {
        public static bool Send(string to, string title, string body, Lebi_User user, int Taskid = 0)
        {
            Thread thread = new Thread(() => ThreadSend(to, title, body, user, Taskid));
            thread.IsBackground = true;
            thread.Start();
            return true;
        }
        public static bool Send(string to, string title, string body, Lebi_User user, string TableName, int Keyid, int Taskid = 0)
        {
            Thread thread = new Thread(() => ThreadSend(to, title, body, user, TableName, Keyid, Taskid));
            thread.IsBackground = true;
            thread.Start();
            return true;
        }
        public static bool ThreadSend(string to, string title, string body, Lebi_User user, int Taskid = 0)
        {
            BaseConfig model = ShopCache.GetBaseConfig();
            try
            {
                string EmailTo = to;
                string EmailFrom = model.MailAddress;
                string Title = title;
                string Content = body;
                string UserName = model.MailName;
                string PassWord = model.MailPassWord;
                string Smtp = model.SmtpAddress;
                int port = 25;
                int.TryParse(model.MailPort, out port);
                if (UserName == "" || PassWord == "" || Smtp == "" || EmailFrom == "" || EmailTo == "")
                {
                    EmailJob(to, title, body, false, user, model, "", 0, Taskid);
                    return false;
                }
                //System.Net.Mail.SmtpClient sc = new System.Net.Mail.SmtpClient();//服务器发件箱的地址、端口
                //System.Net.NetworkCredential nc = new System.Net.NetworkCredential();//验证的用户名和密码
                //sc.Credentials = nc;
                //nc.UserName = UserName;
                //nc.Password = PassWord;
                //sc.Host = Smtp;
                //if (port > 0)
                //{
                //    sc.Port = port;
                //}
                //if (model.MailIsSSL == "1")
                //    sc.EnableSsl = true;
                //System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage(new System.Net.Mail.MailAddress(EmailFrom, model.MailDisplayName), new System.Net.Mail.MailAddress(EmailTo));
                //mm.Subject = Title;
                //mm.Body = Content;
                //mm.IsBodyHtml = true;
                //mm.BodyEncoding = System.Text.Encoding.UTF8;
                //sc.Send(mm);
                //client.UseDefaultCredentials = true;
                MailAddress from_ = new MailAddress(EmailFrom, model.MailDisplayName);
                MailAddress to_ = new MailAddress(EmailTo);
                MailMessage message = new MailMessage(from_, to_);
                message.Subject = Title;        //设置邮件主题 
                message.IsBodyHtml = true;      //设置邮件正文为html格式 
                message.Body = Content;         //设置邮件内容 
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Priority = MailPriority.High;    //优先级 
                //message.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");   
                //message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");               
                SmtpClient client = new SmtpClient(Smtp);
                if (port > 0)
                    client = new SmtpClient(Smtp, port);
                //设置发送邮件身份验证方式 
                //注意如果发件人地址是abc@def.com，则用户名是abc而不是abc@def.com 
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                if (model.MailIsSSL == "1")
                    client.EnableSsl = true;
                client.Send(message);
                message.Dispose();
                EmailJob(to, title, body, true, user, model, "", 0, Taskid);
                return true;
            }
            catch (Exception ex)
            {
                SystemLog.Add(ex.ToString());
                EmailJob(to, title, body, false, user, model, "", 0, Taskid);
                return false;
            }
        }
        public static bool ThreadSend(string to, string title, string body, Lebi_User user, string TableName, int Keyid, int Taskid = 0)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            try
            {
                string EmailTo = to;
                string Title = title;
                string Content = body;
                string EmailFrom = "";
                string UserName = "";
                string PassWord = "";
                string Smtp = "";
                string MailDisplayName = "";
                string MailPort = "";
                string MailIsSSL = "";
                if (TableName == "Supplier")
                {
                    BaseConfig_Supplier model = ShopCache.GetBaseConfig_Supplier(Keyid);
                    EmailFrom = model.MailAddress;
                    UserName = model.MailName;
                    PassWord = model.MailPassWord;
                    Smtp = model.SmtpAddress;
                    MailDisplayName = model.MailDisplayName;
                    MailPort = model.MailPort;
                    MailIsSSL = model.MailIsSSL;
                }
                else if (TableName == "DT")
                {
                    BaseConfig_DT model = ShopCache.GetBaseConfig_DT(Keyid);
                    EmailFrom = model.MailAddress;
                    UserName = model.MailName;
                    PassWord = model.MailPassWord;
                    Smtp = model.SmtpAddress;
                    MailDisplayName = model.MailDisplayName;
                    MailPort = model.MailPort;
                    MailIsSSL = model.MailIsSSL;
                }
                if (UserName == "" || PassWord == "" || Smtp == "" || EmailFrom == "" || EmailTo == "")
                {
                    EmailJob(to, title, body, false, user, conf, TableName, Keyid, Taskid);
                    return false;
                }
                int port = 25;
                int.TryParse(MailPort, out port);
                //SystemLog.Add(EmailFrom + "|" + UserName + "|" + PassWord + "|" + Smtp + "|" + MailDisplayName);
                MailAddress from_ = new MailAddress(EmailFrom, MailDisplayName);
                MailAddress to_ = new MailAddress(EmailTo);
                MailMessage message = new MailMessage(from_, to_);
                message.Subject = Title;        //设置邮件主题 
                message.IsBodyHtml = true;      //设置邮件正文为html格式 
                message.Body = Content;         //设置邮件内容 
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Priority = MailPriority.High;    //优先级 
                //message.BodyEncoding = System.Text.Encoding.GetEncoding("GB2312");   
                //message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");               
                SmtpClient client = new SmtpClient(Smtp);
                if (port > 0)
                    client = new SmtpClient(Smtp, port);
                //设置发送邮件身份验证方式 
                //注意如果发件人地址是abc@def.com，则用户名是abc而不是abc@def.com 
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(UserName, PassWord);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                if (MailIsSSL == "1")
                    client.EnableSsl = true;
                client.Send(message);
                message.Dispose();
                EmailJob(to, title, body, true, user, conf, TableName, Keyid, Taskid);
                return true;
            }
            catch (Exception ex)
            {
                SystemLog.Add(ex.ToString());
                EmailJob(to, title, body, false, user, conf, TableName, Keyid, Taskid);
                return false;
            }
        }
        /// <summary>
        /// 添加邮件发送记录
        /// </summary>
        public static void EmailJob(string Emailto, string Title, string Content, bool sendstatus, Lebi_User user, BaseConfig conf, string TableName = null, int Keyid = 0, int Taskid = 0)
        {
            if (!Validator.IsEmail(Emailto) || string.IsNullOrEmpty(Title))
            {
                return;
            }
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
            if (Taskid == 0)
            {
                Lebi_Email log = new Lebi_Email();
                log.Content = Content;
                log.Count_send = 1;
                log.Email = Emailto;
                log.Title = Title;
                log.Type_id_EmailStatus = Type_id_EmailStatus;
                log.User_id = user.id;
                log.User_Name = user.UserName;
                log.TableName = TableName;
                log.Keyid = Keyid;
                B_Lebi_Email.Add(log);
            }else
            {
                Lebi_Email log = B_Lebi_Email.GetModel(Taskid);
                if (log != null)
                {
                    log.Type_id_EmailStatus = Type_id_EmailStatus;
                    log.Time_Send = DateTime.Now;
                    B_Lebi_Email.Update(log);
                }
            }
        }
        /// <summary>
        /// 添加邮件任务
        /// </summary>
        /// <param name="Emailto"></param>
        /// <param name="Title"></param>
        /// <param name="Content"></param>
        /// <param name="Type_id_EmailStatus"></param>
        /// <param name="user"></param>
        /// <param name="conf"></param>
        public static void EmailJob(string Emailto, string Title, string Content, Lebi_User user, BaseConfig conf, int Taskid = 0)
        {
            if (Taskid >0 || !Validator.IsEmail(Emailto) || string.IsNullOrEmpty(Title))
            {
                return;
            }
            Lebi_Email log = new Lebi_Email();
            int Type_id_EmailStatus = 270;
            log.Content = Content;
            log.Count_send = 1;
            log.Email = Emailto;
            log.Title = Title;
            log.Type_id_EmailStatus = Type_id_EmailStatus;
            log.User_id = user.id;
            log.User_Name = user.UserName;
            B_Lebi_Email.Add(log);

        }
        /// <summary>
        /// 新会员注册邮件
        /// </summary>
        /// <param name="user"></param>
        public static void SendEmail_newuser(Lebi_User user)
        {
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (ShopCache.GetBaseConfig().MailSign.ToLower().Contains("zhuce"))
            {
                string title = Language.Content(conf.EmailTPL_newuser_title, user.Language);
                string content = Language.Content(conf.EmailTPL_newuser, user.Language);
                if (user.DT_id == 0)
                {
                    title = ReplaceEmailTag(title, user, conf);
                    content = ReplaceEmailTag(content, user, conf);
                }
                else
                {
                    title = ReplaceEmailTag(title, user, user.DT_id);
                    content = ReplaceEmailTag(content, user, user.DT_id);
                }
                Send(user.Email, title, content, user);
            }
            if (ShopCache.GetBaseConfig().AdminMailSign.ToLower().Contains("register"))
            {
                string Admin_title = Language.Content(conf.EmailTPL_Admin_newuser_title, user.Language);
                string Admin_content = Language.Content(conf.EmailTPL_Admin_newuser, user.Language);
                Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                Admin_content = ReplaceEmailTag(Admin_content, user, conf);
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
            if (user.DT_id > 0)
            {
                BaseConfig_DT config_dt = ShopCache.GetBaseConfig_DT(user.DT_id);
                if (config_dt.AdminMailSign.ToLower().Contains("register"))
                {
                    string Admin_title = Language.Content(conf.EmailTPL_Admin_newuser_title, user.Language);
                    string Admin_content = Language.Content(conf.EmailTPL_Admin_newuser, user.Language);
                    Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
                    Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
                    Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
                }
            }
        }
        /// <summary>
        /// 找回密码邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="CurrentTheme"></param>
        public static void SendEmail_forgetpwd(Lebi_User user, Lebi_Theme CurrentTheme)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.MailSign.ToLower().Contains("getpwd"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string url = "";
                user.CheckCode = EX_User.MD5(System.DateTime.Now.ToString() + conf.InstallCode);
                B_Lebi_User.Update(user);
                url = "http://" + RequestTool.GetRequestDomain() + ThemeUrl.GetURL("P_FindPassword", "", "", user.Language) + "?id=" + user.id + "&email=" + user.Email + "&v=" + user.CheckCode;
                string title = Language.Content(conf.EmailTPL_getpwd_title, user.Language);
                string content = Language.Content(conf.EmailTPL_getpwd, user.Language);
                if (user.DT_id == 0)
                {
                    title = ReplaceEmailTag(title, user, conf);
                    content = ReplaceEmailTag(content, user, conf);
                }
                else
                {
                    title = ReplaceEmailTag(title, user, user.DT_id);
                    content = ReplaceEmailTag(content, user, user.DT_id);
                }
                content = content.Replace("{$UserPWDURL}", url);
                Send(user.Email, title, content, user);
            }
        }
        /// <summary>
        /// 订单提交邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        public static void SendEmail_ordersubmit(Lebi_User user, Lebi_Order order)
        {
            int IsUserMail = 0;
            int IsAdminMail = 0;
            int IsSupplierMail = 0;
            int IsDTMail = 0;
            int IsSupplierTransport = 0;
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.MailSign.ToLower().Contains("dingdantijiao"))
            {
                IsUserMail = 1;
            }
            if (conf.AdminMailSign.ToLower().Contains("ordersubmit"))
            {
                IsAdminMail = 1;
            }
            if (order.Supplier_id > 0)
            {
                if (ShopCache.GetBaseConfig_Supplier(order.Supplier_id).AdminMailSign.ToLower().Contains("ordersubmit"))
                {
                    IsSupplierMail = 1;
                }
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
                IsSupplierTransport = supplier.IsSupplierTransport;
            }
            if (user.DT_id > 0)
            {
                if (ShopCache.GetBaseConfig_DT(user.DT_id).AdminMailSign.ToLower().Contains("ordersubmit"))
                {
                    IsDTMail = 1;
                }
            }
            if (IsUserMail == 0 && IsAdminMail == 0 && IsSupplierMail == 0 && IsDTMail == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string list = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr><td style=\"background: #F5F5F5;width:100px;height:28px;\">" + Language.Tag("商品编号", user.Language) + "</td><td style=\"background: #F5F5F5;\">" + Language.Tag("商品", user.Language) + "</td><td style=\"background: #F5F5F5;width:80px;\">" + Language.Tag("数量", user.Language) + "</td></tr>";
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
            foreach (Lebi_Order_Product pro in pros)
            {
                list += "<tr><td style=\"height:25px;\">" + pro.Product_Number + "</td><td>" + Language.Content(pro.Product_Name, user.Language) + "</td><td>" + pro.Count + "</td></tr>";
            }
            if (IsSupplierTransport == 0)
            {
                list += "<tr><td colspan=\"3\">" + Language.Tag("订单金额", user.Language) + ":" + Language.FormatMoney(order.Money_Order, order.Currency_Code) + "</td></tr>";
            }
            list += "</table>";
            if (IsUserMail == 1)
            {
                string title = Language.Content(conf.EmailTPL_ordersubmit_title, user.Language);
                string content = Language.Content(conf.EmailTPL_ordersubmit, user.Language);
                if (user.DT_id == 0)
                {
                    title = ReplaceEmailTag(title, user, conf);
                    content = ReplaceEmailTag(content, user, conf);
                }
                else
                {
                    title = ReplaceEmailTag(title, user, user.DT_id);
                    content = ReplaceEmailTag(content, user, user.DT_id);
                }
                title = title.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$Order}", list);
                Send(user.Email, title, content, user);
            }
            string Admin_title = "";
            string Admin_content = "";
            if (IsAdminMail == 1 || IsSupplierMail == 1 || IsDTMail == 1)
            {
                Admin_title = Language.Content(conf.EmailTPL_Admin_ordersubmit_title, user.Language);
                Admin_content = Language.Content(conf.EmailTPL_Admin_ordersubmit, user.Language);
                if (user.DT_id == 0)
                {
                    Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                    Admin_content = ReplaceEmailTag(Admin_content, user, conf);
                }
                else
                {
                    Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
                    Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
                }
                Admin_title = Admin_title.Replace("{$OrderNO}", order.Code);
                Admin_content = Admin_content.Replace("{$OrderNO}", order.Code);
                Admin_content = Admin_content.Replace("{$Order}", list);
            }
            if (IsAdminMail == 1)
            {
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
            if (IsSupplierMail == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(order.Supplier_id);
                Send(config_supplier.AdminMailAddress, Admin_title, Admin_content, user, "Supplier", order.Supplier_id);
            }
            if (IsDTMail == 1)
            {
                BaseConfig_DT config_dt = ShopCache.GetBaseConfig_DT(user.DT_id);
                Send(config_dt.AdminMailAddress, Admin_title, Admin_content, user, "DT", user.DT_id);
            }
        }
        /// <summary>
        /// 订单付款邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        public static void SendEmail_orderpaid(Lebi_User user, Lebi_Order order)
        {
            int IsUserMail = 0;
            int IsAdminMail = 0;
            int IsSupplierMail = 0;
            int IsDTMail = 0;
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.MailSign.ToLower().Contains("orderpaid"))
            {
                IsUserMail = 1;
            }
            if (conf.AdminMailSign.ToLower().Contains("orderpaid"))
            {
                IsAdminMail = 1;
            }
            if (order.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
                if (supplier.IsSupplierTransport == 0)  //商家发货
                {
                    if (ShopCache.GetBaseConfig_Supplier(order.Supplier_id).AdminMailSign.ToLower().Contains("orderpaid"))
                    {
                        IsSupplierMail = 1;
                    }
                }
            }
            if (user.DT_id > 0)
            {
                if (ShopCache.GetBaseConfig_DT(user.DT_id).AdminMailSign.ToLower().Contains("orderpaid"))
                {
                    IsDTMail = 1;
                }
            }
            if (IsUserMail == 0 && IsAdminMail == 0 && IsSupplierMail == 0 && IsDTMail == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string list = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr><td style=\"background: #F5F5F5;width:100px;height:28px;\">" + Language.Tag("商品编号", user.Language) + "</td><td style=\"background: #F5F5F5;\">" + Language.Tag("商品", user.Language) + "</td><td style=\"background: #F5F5F5;width:80px;\">" + Language.Tag("数量", user.Language) + "</td></tr>";
            List<Lebi_Order_Product> pros = B_Lebi_Order_Product.GetList("Order_id=" + order.id + "", "");
            foreach (Lebi_Order_Product pro in pros)
            {
                list += "<tr><td style=\"height:25px;\">" + pro.Product_Number + "</td><td>" + Language.Content(pro.Product_Name, user.Language) + "</td><td>" + pro.Count + "</td></tr>";
            }
            list += "<tr><td colspan=\"3\">" + Language.Tag("订单金额", user.Language) + ":" + Language.FormatMoney(order.Money_Order, order.Currency_Code) + "</td></tr>";
            list += "</table>";
            if (IsUserMail == 1)
            {
                string title = Language.Content(conf.EmailTPL_orderpaid_title, user.Language);
                string content = Language.Content(conf.EmailTPL_orderpaid, user.Language);
                title = ReplaceEmailTag(title, user, conf);
                content = ReplaceEmailTag(content, user, conf);
                title = title.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$Order}", list);
                Send(user.Email, title, content, user);
            }
            string Admin_title = "";
            string Admin_content = "";
            if (IsAdminMail == 1 || IsSupplierMail == 1 || IsDTMail == 1)
            {
                Admin_title = Language.Content(conf.EmailTPL_Admin_ordersubmit_title, user.Language);
                Admin_content = Language.Content(conf.EmailTPL_Admin_ordersubmit, user.Language);
                if (IsDTMail == 0)
                {
                    Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                    Admin_content = ReplaceEmailTag(Admin_content, user, conf);
                }
                else
                {
                    Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
                    Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
                }
                Admin_title = Admin_title.Replace("{$OrderNO}", order.Code);
                Admin_content = Admin_content.Replace("{$OrderNO}", order.Code);
                Admin_content = Admin_content.Replace("{$Order}", list);
            }
            if (IsAdminMail == 1)
            {
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
            if (IsSupplierMail == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(order.Supplier_id);
                Send(config_supplier.AdminMailAddress, Admin_title, Admin_content, user, "Supplier", order.Supplier_id);
            }
            if (IsDTMail == 1)
            {
                BaseConfig_DT config_dt = ShopCache.GetBaseConfig_DT(user.DT_id);
                Send(config_dt.AdminMailAddress, Admin_title, Admin_content, user, "DT", user.DT_id);
            }
        }
        /// <summary>
        /// 预定商品到货
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="pro"></param>
        public static void SendEmail_reserveok(Lebi_User user, Lebi_Order order, Lebi_Order_Product pro)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.MailSign.Contains("reserveok"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string title = Language.Content(conf.EmailTPL_reserveok_title, user.Language);
                string content = Language.Content(conf.EmailTPL_reserveok, user.Language);
                if (user.DT_id == 0)
                {
                    title = ReplaceEmailTag(title, user, conf);
                    content = ReplaceEmailTag(content, user, conf);
                }
                else
                {
                    title = ReplaceEmailTag(title, user, user.DT_id);
                    content = ReplaceEmailTag(content, user, user.DT_id);
                }
                title = title.Replace("{$OrderNO}", order.Code);
                title = title.Replace("{$ProductName}", Language.Content(pro.Product_Name, user.Language));
                title = title.Replace("{$Count}", pro.Count.ToString());
                content = content.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$ProductName}", Language.Content(pro.Product_Name, user.Language));
                content = content.Replace("{$Count}", pro.Count.ToString());
                Send(user.Email, title, content, user);
            }
        }
        /// <summary>
        /// 订单发货邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="order"></param>
        /// <param name="torder"></param>
        public static void SendEmail_ordershipping(Lebi_User user, Lebi_Order order, Lebi_Transport_Order torder)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.MailSign.ToLower().Contains("dingdanfahuo"))
            {
                if (user.Language == "")
                    user.Language = Language.Languages().FirstOrDefault().Code;
                string list = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\" border=\"0\"><tr><td style=\"background: #F5F5F5;width:100px;height:28px;\">" + Language.Tag("商品编号", user.Language) + "</td><td style=\"background: #F5F5F5;\">" + Language.Tag("商品", user.Language) + "</td><td style=\"background: #F5F5F5;width:80px;\">" + Language.Tag("发货数量", user.Language) + "</td></tr>";

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
                    list += "<tr><td style=\"height:25px;\">" + pro.Product_Number + "</td><td>" + Language.Content(pro.Product_Name, user.Language) + "</td><td>" + pro.Count + "</td></tr>";
                }
                list += "<tr><td colspan=\"3\">" + Language.Tag("货运公司", user.Language) + ":" + torder.Transport_Name + "&nbsp;&nbsp;&nbsp;&nbsp;" + Language.Tag("货运单号", user.Language) + ":" + torder.Code + "</td></tr>";
                list += "</table>";
                string title = Language.Content(conf.EmailTPL_ordershipping_title, user.Language);
                string content = Language.Content(conf.EmailTPL_ordershipping, user.Language);
                if (user.DT_id == 0)
                {
                    title = ReplaceEmailTag(title, user, conf);
                    content = ReplaceEmailTag(content, user, conf);
                }
                else
                {
                    title = ReplaceEmailTag(title, user, user.DT_id);
                    content = ReplaceEmailTag(content, user, user.DT_id);
                }
                title = title.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$OrderNO}", order.Code);
                content = content.Replace("{$Order}", list);
                Send(user.Email, title, content, user);
            }
        }
        /// <summary>
        /// 留言反馈邮件
        /// </summary>
        /// <param name="model"></param>
        public static void SendEmail_inquiry(Lebi_Inquiry model)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.AdminMailSign.ToLower().Contains("inquiry"))
            {
                Lebi_User user = B_Lebi_User.GetModel(0);
                if (user == null)
                    user = new Lebi_User();
                string list = "" + Language.Tag("昵称", model.Language) + "：" + model.UserName + "<br />" + Language.Tag("电话", model.Language) + "：" + model.Phone + "<br />" + Language.Tag("EMAIL", model.Language) + "：" + model.Email + "<br />" + Language.Tag("标题", model.Language) + "：" + model.Subject + "<br />" + Language.Tag("内容", model.Language) + "：" + model.Content + "<br />" + Language.Tag("时间", model.Language) + "：" + model.Time_Add + "";

                string Admin_title = Language.Content(conf.EmailTPL_Admin_inquiry_title, model.Language);
                string Admin_content = Language.Content(conf.EmailTPL_Admin_inquiry, model.Language);
                //if (user.DT_id == 0)
                //{
                    Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                    Admin_content = ReplaceEmailTag(Admin_content, user, conf);
                //}
                //else
                //{
                //    Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
                //    Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
                //}
                Admin_title = Admin_title.Replace("{$Title}", model.Subject);
                Admin_content = Admin_content.Replace("{$Title}", model.Subject);
                Admin_content = Admin_content.Replace("{$Content}", list);
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
        }
        /// <summary>
        /// 商品评论邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void SendEmail_comment(Lebi_User user, Lebi_Comment model)
        {
            int IsAdminMail = 0;
            int IsSupplierMail = 0;
            Lebi_Product product = B_Lebi_Product.GetModel(model.Product_id);
            if (product == null)
                product = new Lebi_Product();
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.AdminMailSign.ToLower().Contains("comment"))
            {
                IsAdminMail = 1;
            }
            if (product.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(product.Supplier_id);
                if (supplier.IsSupplierTransport == 0)  //商家发货
                {
                    if (ShopCache.GetBaseConfig_Supplier(product.Supplier_id).AdminMailSign.ToLower().Contains("comment"))
                    {
                        IsSupplierMail = 1;
                    }
                }
            }
            if (IsAdminMail == 0 && IsSupplierMail == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string list = "" + Language.Tag("商品名称", user.Language) + "：<a target='_blank' href='http://" + conf.Domain + Shop.Bussiness.ThemeUrl.GetURL("P_Product", model.Product_id.ToString(), "", user.Language) + "'>" + Language.Content(product.Name, user.Language) + "</a><br />" + Language.Tag("会员帐号", user.Language) + "：" + model.User_UserName + "<br />" + Language.Tag("评分", user.Language) + "：" + model.Star + "<br />" + Language.Tag("内容", user.Language) + "：" + model.Content + "<br />";
            if (model.ImagesSmall != "")
            {
                list += "" + Language.Tag("图片", user.Language) + "：<br />";
                string[] images = model.ImagesSmall.Split('@');
                string[] bigs = model.Images.Split('@');
                for (int i = 0; i < images.Count(); i++)
                {
                    if (images[i] == "")
                        continue;
                    list += "<a href='http://" + conf.Domain + bigs[i] + "' target='_blank'><img src='http://" + conf.Domain + images[i] + "' /></a><br />";
                }
            }
            list += Language.Tag("时间", user.Language) + "：" + model.Time_Add;
            string Admin_title = Language.Content(conf.EmailTPL_Admin_comment_title, user.Language);
            string Admin_content = Language.Content(conf.EmailTPL_Admin_comment, user.Language);
            //if (user.DT_id == 0)
            //{
                Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                Admin_content = ReplaceEmailTag(Admin_content, user, conf);
            //}
            //else
            //{
            //    Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
            //    Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
            //}
            Admin_content = Admin_content.Replace("{$Content}", list);
            if (IsAdminMail == 1)
            {
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
            if (IsSupplierMail == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(product.Supplier_id);
                Send(config_supplier.AdminMailAddress, Admin_title, Admin_content, user, "Supplier", product.Supplier_id);
            }
        }
        /// <summary>
        /// 商品咨询邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void SendEmail_ask(Lebi_User user, Lebi_Comment model)
        {
            int IsAdminMail = 0;
            int IsSupplierMail = 0;
            Lebi_Product product = B_Lebi_Product.GetModel(model.Product_id);
            if (product == null)
                product = new Lebi_Product();
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.AdminMailSign.ToLower().Contains("ask"))
            {
                IsAdminMail = 1;
            }
            if (product.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(product.Supplier_id);
                if (supplier.IsSupplierTransport == 0)  //商家发货
                {
                    if (ShopCache.GetBaseConfig_Supplier(product.Supplier_id).AdminMailSign.ToLower().Contains("ask"))
                    {
                        IsSupplierMail = 1;
                    }
                }
            }
            if (IsAdminMail == 0 && IsSupplierMail == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string list = "" + Language.Tag("商品名称", user.Language) + "：<a target='_blank' href='http://" + conf.Domain + Shop.Bussiness.ThemeUrl.GetURL("P_Product", model.Product_id.ToString(), "", user.Language) + "'>" + Language.Content(product.Name, user.Language) + "</a><br />" + Language.Tag("会员帐号", user.Language) + "：" + model.User_UserName + "<br />" + Language.Tag("内容", user.Language) + "：" + model.Content + "<br />" + Language.Tag("时间", user.Language) + "：" + model.Time_Add + "";
            string Admin_title = Language.Content(conf.EmailTPL_Admin_ask_title, user.Language);
            string Admin_content = Language.Content(conf.EmailTPL_Admin_ask, user.Language);
            //if (user.DT_id == 0)
            //{
                Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                Admin_content = ReplaceEmailTag(Admin_content, user, conf);
            //}
            //else
            //{
            //    Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
            //    Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
            //}
            Admin_content = Admin_content.Replace("{$Content}", list);
            if (IsAdminMail == 1)
            {
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
            if (IsSupplierMail == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(product.Supplier_id);
                Send(config_supplier.AdminMailAddress, Admin_title, Admin_content, user, "Supplier", product.Supplier_id);
            }
        }
        /// <summary>
        /// 站内信邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void SendEmail_message(Lebi_User user, Lebi_Message model)
        {
            int IsAdminMail = 0;
            int IsDTMail = 0;
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (conf.AdminMailSign.ToLower().Contains("message"))
            {
                IsAdminMail = 1;
            }
            if (user.DT_id > 0)
            {
                if (ShopCache.GetBaseConfig_DT(user.DT_id).AdminMailSign.ToLower().Contains("message"))
                {
                    IsDTMail = 1;
                }
            }
            if (IsAdminMail == 0 && IsDTMail == 0)
            {
                return;
            }
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            Lebi_Message_Type type = B_Lebi_Message_Type.GetModel(model.Message_Type_id);
            if (type == null)
                type = new Lebi_Message_Type();
            string list = "" + Language.Tag("会员账号", user.Language) + "：" + model.User_Name_From + "<br />" + Language.Tag("类型", user.Language) + "：" + Language.Content(type.Name, user.Language) + "<br />" + Language.Tag("标题", user.Language) + "：" + model.Title + "<br />" + Language.Tag("内容", user.Language) + "：" + model.Content + "<br />" + Language.Tag("时间", user.Language) + "：" + model.Time_Add + "";

            string Admin_title = Language.Content(conf.EmailTPL_Admin_message_title, user.Language);
            string Admin_content = Language.Content(conf.EmailTPL_Admin_message, user.Language);
            if (IsDTMail == 0)
            {
                Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                Admin_content = ReplaceEmailTag(Admin_content, user, conf);
            }
            else
            {
                Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
                Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
            }
            Admin_title = Admin_title.Replace("{$Title}", model.Title);
            Admin_content = Admin_content.Replace("{$Title}", model.Title);
            Admin_content = Admin_content.Replace("{$Content}", list);
            if (IsAdminMail == 1)
            {
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
            if (IsDTMail == 1)
            {
                BaseConfig_DT config_dt = ShopCache.GetBaseConfig_DT(user.DT_id);
                Send(config_dt.AdminMailAddress, Admin_title, Admin_content, user, "DT", user.DT_id);
            }
        }
        /// <summary>
        /// 订单留言邮件
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        public static void SendEmail_ordercomment(Lebi_User user, Lebi_Comment model)
        {
            int IsAdminMail = 0;
            int IsSupplierMail = 0;
            BaseConfig conf = ShopCache.GetBaseConfig();
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            Lebi_Order order = B_Lebi_Order.GetModel(model.Keyid);
            if (order == null)
                order = new Lebi_Order();
            if (conf.AdminMailSign.ToLower().Contains("ordercomment"))
            {
                IsAdminMail = 1;
            }
            if (order.Supplier_id > 0)
            {
                Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
                if (supplier.IsSupplierTransport == 0)  //商家发货
                {
                    if (ShopCache.GetBaseConfig_Supplier(order.Supplier_id).AdminMailSign.ToLower().Contains("ordercomment"))
                    {
                        IsSupplierMail = 1;
                    }
                }
            }
            if (IsAdminMail == 0 && IsSupplierMail == 0)
            {
                return;
            }
            string list = "" + Language.Tag("订单编号", user.Language) + "：" + order.Code + "<br />" + Language.Tag("会员帐号", user.Language) + "：" + model.User_UserName + "<br />" + Language.Tag("内容", user.Language) + "：" + model.Content + "<br />" + Language.Tag("时间", user.Language) + "：" + model.Time_Add + "";
            string Admin_title = Language.Content(conf.EmailTPL_Admin_ordercomment_title, user.Language);
            string Admin_content = Language.Content(conf.EmailTPL_Admin_ordercomment, user.Language);
            //if (user.DT_id == 0)
            //{
                Admin_title = ReplaceEmailTag(Admin_title, user, conf);
                Admin_content = ReplaceEmailTag(Admin_content, user, conf);
            //}
            //else
            //{
            //    Admin_title = ReplaceEmailTag(Admin_title, user, user.DT_id);
            //    Admin_content = ReplaceEmailTag(Admin_content, user, user.DT_id);
            //}
            Admin_content = Admin_content.Replace("{$Content}", list);
            Admin_title = Admin_title.Replace("{$OrderNO}", order.Code);
            if (IsAdminMail == 1)
            {
                Send(conf.AdminMailAddress, Admin_title, Admin_content, user);
            }
            if (IsSupplierMail == 1)
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(order.Supplier_id);
                Send(config_supplier.AdminMailAddress, Admin_title, Admin_content, user, "Supplier", order.Supplier_id);
            }
        }
        /// <summary>
        /// 常购清单提醒邮件
        /// </summary>
        /// <param name="pro"></param>
        public static void SendEmail_changgouqingdan(Lebi_User_Product pro)
        {
            BaseConfig conf = ShopCache.GetBaseConfig();
            Lebi_Product product = B_Lebi_Product.GetModel(pro.Product_id);
            if (product == null)
                return;
            Lebi_User user = B_Lebi_User.GetModel(pro.User_id);
            if (user == null)
                return;
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            string title = Language.Content(conf.EmailTPL_changgouqingdan_title, user.Language);
            string content = Language.Content(conf.EmailTPL_changgouqingdan, user.Language);
            if (user.DT_id == 0)
            {
                title = ReplaceEmailTag(title, user, conf);
                content = ReplaceEmailTag(content, user, conf);
            }
            else
            {
                title = ReplaceEmailTag(title, user, user.DT_id);
                content = ReplaceEmailTag(content, user, user.DT_id);
            }
            string list = "" + Language.Tag("商品名称") + "：<a target='_blank' href='http://" + conf.Domain + Shop.Bussiness.ThemeUrl.GetURL("P_Product", product.id.ToString(), "", user.Language) + "'>" + Language.Content(product.Name, user.Language) + "</a><br />" + Language.Tag("数量") + "：" + pro.count + "<br />" + Language.Tag("预购时间") + "：" + pro.Time_addemail.ToString("yyyy-MM-dd") + "";
            title = title.Replace("{$ProductName}", Language.Content(product.Name, user.Language));
            title = title.Replace("{$Count}", pro.count.ToString());
            title = title.Replace("{$Time}", System.DateTime.Now.Date.ToString());
            content = content.Replace("{$ProductName}", Language.Content(product.Name, user.Language));
            content = content.Replace("{$Count}", pro.count.ToString());
            content = content.Replace("{$Time}", System.DateTime.Now.Date.ToString());
            content = content.Replace("{$Content}", list);
            //bool flag = Send(user.Email, title, content);
            EmailJob(user.Email, title, content, user, conf);
        }
        /// <summary>
        /// 邮件验证码邮件
        /// </summary>
        /// <param name="user"></param>
        public static void SendEmail_checkcode(Lebi_User user)
        {
            if (user.Language == "")
                user.Language = Language.Languages().FirstOrDefault().Code;
            BaseConfig conf = ShopCache.GetBaseConfig();
            string code = Common.GetRnd(6, true, false, false, false, "");
            string title = Language.Content(conf.EmailTPL_checkcode_title, user.Language);
            string content = Language.Content(conf.EmailTPL_checkcode, user.Language);
            HttpContext.Current.Session["emailcheckcode"] = user.Email + code;
            if (user.DT_id == 0)
            {
                title = ReplaceEmailTag(title, user, conf);
                content = ReplaceEmailTag(content, user, conf);
            }
            else
            {
                title = ReplaceEmailTag(title, user, user.DT_id);
                content = ReplaceEmailTag(content, user, user.DT_id);
            }
            title = title.Replace("{$CheckCode}", code);
            content = content.Replace("{$CheckCode}", code);
            Send(user.Email, title, content, user);
            //EmailJob(user.Email, title, content, user, conf);
        }
        /// <summary>
        /// 邮件分享邮件
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="Content"></param>
        /// <param name="product"></param>
        /// <param name="CurrentLanguage"></param>
        public static void SendEmail_sendfriend(string From, string To, string Content, Lebi_Product product, string CurrentLanguage)
        {
            Lebi_User user = new Lebi_User();
            BaseConfig conf = ShopCache.GetBaseConfig();
            string ProductURL = conf.Domain + Shop.Bussiness.ThemeUrl.GetURL("P_Product", product.Product_id.ToString(), "", CurrentLanguage);
            string title = Language.Content(conf.EmailTPL_sendfriend_title, CurrentLanguage);
            string content = Language.Content(conf.EmailTPL_sendfriend, CurrentLanguage);
            string FromUserName = From.Split('|')[0];
            string FromEmail = From.Split('|')[1];
            string ToUserName = To.Split('|')[0];
            string ToEmail = To.Split('|')[1];
            title = title.Replace("{$ToUserName}", ToUserName);
            title = title.Replace("{$UserName}", FromUserName);
            title = title.Replace("{$ProductName}", Language.Content(product.Name, CurrentLanguage));
            title = title.Replace("{$Time}", System.DateTime.Now.Date.ToString());
            content = content.Replace("{$UserName}", FromUserName);
            content = content.Replace("{$Email}", FromEmail);
            content = content.Replace("{$ToUserName}", ToUserName);
            content = content.Replace("{$Time}", System.DateTime.Now.Date.ToString());
            content = content.Replace("{$ProductName}", Language.Content(product.Name, CurrentLanguage));
            content = content.Replace("{$ProductURL}", ProductURL);
            content = content.Replace("{$Content}", Content);
            if (user.DT_id == 0)
            {
                content = ReplaceEmailTag(content, user, conf);
            }
            else
            {
                content = ReplaceEmailTag(content, user, user.DT_id);
            }
            Send(ToEmail, title, content, user);
            //EmailJob(ToEmail, title, content, user, conf);
        }
        /// <summary>
        /// 发送测试
        /// </summary>
        /// <param name="user"></param>
        public static void SendEmail_test()
        {
            Lebi_User user = new Lebi_User();
            BaseConfig conf = ShopCache.GetBaseConfig();
            string title = "";
            string content = "";
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                title = Language.Tag("欢迎使用LebiShop");
                content = Language.Tag("这是系统设置自动发信参数时的测试邮件，如果您收到此邮件，表示自动发信的参数已经设置正确！");
                content += "<br/>LebiShop V" + conf.Version + "." + conf.Version_Son + " <a href='http://www.lebi.cn' target='_blank'>http://www.lebi.cn</a>";
            }
            else
            {
                title = Language.Tag("邮件发送测试");
                content = Language.Tag("这是系统设置自动发信参数时的测试邮件，如果您收到此邮件，表示自动发信的参数已经设置正确！");
            }
            content += "<br/>" + System.DateTime.Now.Date.ToString();
            Send(conf.MailAddress, title, content, user);
        }
        public static void SendEmail_test(string TableName, int Keyid)
        {
            Lebi_User user = new Lebi_User();
            BaseConfig conf = ShopCache.GetBaseConfig();
            string title = Language.Tag("邮件发送测试");
            string content = Language.Tag("这是系统设置自动发信参数时的测试邮件，如果您收到此邮件，表示自动发信的参数已经设置正确！");
            if (!Shop.LebiAPI.Service.Instanse.Check("managelicenese"))
            {
                content += "<br/>LebiShop V" + conf.Version + "." + conf.Version_Son + " <a href='http://www.lebi.cn' target='_blank'>http://www.lebi.cn</a>";
            }
            content += "<br/>" + System.DateTime.Now.Date.ToString();
            if (TableName == "Supplier")
            {
                BaseConfig_Supplier config_supplier = ShopCache.GetBaseConfig_Supplier(Keyid);
                Send(config_supplier.MailAddress, title, content, user, "Supplier", Keyid);
            }
            else if (TableName == "DT")
            {
                BaseConfig_DT config_dt = ShopCache.GetBaseConfig_DT(Keyid);
                Send(config_dt.MailAddress, title, content, user, "DT", Keyid);
            }
        }
        public static string ReplaceEmailTag(string instr, Lebi_User user, BaseConfig conf)
        {
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
        public static string ReplaceEmailTag(string instr, Lebi_User user, int DT_id)
        {
            Lebi_DT DT = B_Lebi_DT.GetModel(DT_id);
            instr = instr.Replace("{$UserName}", user.UserName);
            instr = instr.Replace("{$UserID}", user.id.ToString());
            instr = instr.Replace("{$Phone}", Language.Content(DT.Site_Phone, user.Language));
            instr = instr.Replace("{$Email}", Language.Content(DT.Site_Email, user.Language));
            instr = instr.Replace("{$QQ}", Language.Content(DT.Site_QQ, user.Language));
            instr = instr.Replace("{$SiteName}", Language.Content(DT.Site_Name, user.Language));
            instr = instr.Replace("{$Domain}", DT.Domain);
            return instr;
        }
    }

}

