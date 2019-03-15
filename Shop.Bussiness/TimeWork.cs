using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;
using System.IO;

namespace Shop.Bussiness
{
    public delegate void AutoActionEventHandler();
    public class TimeWork
    {
        protected BaseConfig conf;
        private static System.Timers.Timer timer_sendmail = null;
        private static System.Timers.Timer timer_work_databackup = null;
        private static System.Timers.Timer timer_work_license = null;
        private static System.Timers.Timer timer_work_checkcard = null;
        private static System.Timers.Timer timer_work_checkdomain = null;
        private static System.Timers.Timer timer_work_orderReceived = null;
        private static System.Timers.Timer timer_work_changgouqingdEmail = null;

        //十分钟自动执行事件
        private static System.Timers.Timer timer_work_AutoAction10 = null;

        private static System.Timers.Timer timer_work_AutoAction1m = null;

        public static event AutoActionEventHandler AutoActionEvent_10m;
        protected static void AutoAction_10m()
        {
            if (AutoActionEvent_10m != null)
            {
                AutoActionEvent_10m();
            }
        }
        public static event AutoActionEventHandler AutoActionEvent_1m;
        protected static void AutoAction_1m()
        {
            if (AutoActionEvent_1m != null)
            {
                AutoActionEvent_1m();
            }
        }

        public TimeWork()
        {
            conf = ShopCache.GetBaseConfig();
            licensecheck_start();
            orderReceived_start();
            cardcheck_start();
            AutoAction_10_start();
            AutoAction_1m_start();
        }

        #region 邮件任务

        /// <summary>
        /// 处理邮件队列任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void work_email(object sender, System.Timers.ElapsedEventArgs e)
        {

            lock (this)
            {
                int Mail_SendTop = 0;
                int.TryParse(conf.Mail_SendTop, out Mail_SendTop);
                string where = "(Type_id_EmailStatus=270 and Count_send < " + Mail_SendTop + ") and Time_Task<=GetDate()";
                List<Lebi_Email> models = B_Lebi_Email.GetList(where, "", 100, 1);
                int Type_id_EmailStatus = 270;
                foreach (Lebi_Email model in models)
                {
                    Type_id_EmailStatus = 270;
                    model.Count_send++;
                    Lebi_User user = B_Lebi_User.GetModel(model.User_id);
                    if (user == null)
                    {
                        user = new Lebi_User();
                    }
                    if (model.TableName == "Supplier" && model.Keyid > 0)
                    {
                        Email.Send(model.Email, model.Title, model.Content, user, "Supplier", model.Keyid, model.id);
                    }
                    else if (model.TableName == "DT" && model.Keyid > 0)
                    {
                        Email.Send(model.Email, model.Title, model.Content, user, "DT", model.Keyid, model.id);
                    }
                    else
                    {
                        Email.Send(model.Email, model.Title, model.Content, user, model.id);
                    }
                    model.Type_id_EmailStatus = Type_id_EmailStatus;
                    B_Lebi_Email.Update(model);

                }
            }
        }
        /// <summary>
        /// 启动邮件任务
        /// </summary>
        public void work_email_start()
        {
            int Mail_SendTime = 0;
            int.TryParse(conf.Mail_SendTime, out Mail_SendTime);
            if (Mail_SendTime > 0)
            {
                timer_sendmail = new System.Timers.Timer(1000 * 60 * Mail_SendTime);//1000 * 60 表示1分钟
                timer_sendmail.Elapsed += new System.Timers.ElapsedEventHandler(work_email);
                timer_sendmail.Enabled = true;
                timer_sendmail.Start();
            }
        }
        /// <summary>
        /// 重新启动邮件任务
        /// </summary>
        public void work_email_restart()
        {
            if (null != timer_sendmail)
                work_email_stop();
            work_email_start();
        }
        /// <summary>
        /// 停止邮件任务
        /// </summary>
        public void work_email_stop()
        {
            timer_sendmail.Stop();
            timer_sendmail.Dispose();
            timer_sendmail = null;
        }
        #endregion
        #region 数据库自动备份
        /// <summary>
        /// 数据库自动备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void work_databackup(object sender, System.Timers.ElapsedEventArgs e)
        {
            BackUP.Add();
        }

        /// <summary>
        /// 启动数据库自动备份
        /// </summary>
        public void work_databackup_start()
        {
            int DataBase_BackUpTime = 0;
            int.TryParse(conf.DataBase_BackUpTime, out DataBase_BackUpTime);
            if (DataBase_BackUpTime > 0)
            {

                timer_work_databackup = new System.Timers.Timer(1000 * 60 * DataBase_BackUpTime);//1000 * 60 表示1分钟
                timer_work_databackup.Elapsed += new System.Timers.ElapsedEventHandler(work_databackup);
                timer_work_databackup.Enabled = true;
                timer_work_databackup.Start();
            }
        }
        /// <summary>
        /// 重新启动数据库自动备份
        /// </summary>
        public void work_databackup_restart()
        {
            if (null != timer_work_databackup)
                work_databackup_stop();
            work_databackup_start();
        }
        /// <summary>
        /// 停止数据库自动备份
        /// </summary>
        public void work_databackup_stop()
        {
            timer_work_databackup.Stop();
            timer_work_databackup.Dispose();
            timer_work_databackup = null;
        }
        #endregion
        #region 检查授权
        /// <summary>
        /// 检查授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void licensecheck(object sender, System.Timers.ElapsedEventArgs e)
        {
            Shop.LebiAPI.Service.Instanse.License_Update(false);
            ShopCache.SetDomainStatus();//更新域名授权状况
        }

        /// <summary>
        /// 检查授权-24小时
        /// </summary>
        public void licensecheck_start()
        {


            timer_work_license = new System.Timers.Timer(1000 * 60 * 60 * 24);//1000 * 60 表示1分钟
            timer_work_license.Elapsed += new System.Timers.ElapsedEventHandler(licensecheck);
            timer_work_license.Enabled = true;
            timer_work_license.Start();

        }
        #endregion
        #region 卡券过期
        /// <summary>
        /// 卡券过期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cardcheck(object sender, System.Timers.ElapsedEventArgs e)
        {
            string sql = "update [Lebi_Card] set Type_id_CardStatus=204 where Time_End<'" + System.DateTime.Now + "'";
            Common.ExecuteSql(sql);
        }

        /// <summary>
        /// 卡券过期-1小时
        /// </summary>
        public void cardcheck_start()
        {


            timer_work_checkcard = new System.Timers.Timer(1000 * 60 * 60);//1000 * 60 表示1分钟
            timer_work_checkcard.Elapsed += new System.Timers.ElapsedEventHandler(cardcheck);
            timer_work_checkcard.Enabled = true;
            timer_work_checkcard.Start();

        }
        #endregion
        #region 检查域名禁用
        /// <summary>
        /// 域名禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void domaincheck(object sender, System.Timers.ElapsedEventArgs e)
        {
            ShopCache.SetDomainStatus();
        }

        /// <summary>
        /// 卡券过期-1小时
        /// </summary>
        public void domaincheck_start()
        {


            timer_work_checkdomain = new System.Timers.Timer(1000 * 60 * 60 * 2);//1000 * 60 表示1分钟
            timer_work_checkdomain.Elapsed += new System.Timers.ElapsedEventHandler(domaincheck);
            timer_work_checkdomain.Enabled = true;
            timer_work_checkdomain.Start();

        }
        #endregion
        #region 订单自动收货确认+自动完结
        /// <summary>
        /// 订单自动收货确认+自动完结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void orderReceived(object sender, System.Timers.ElapsedEventArgs e)
        {
            string days = conf.OrderReceivedDays;
            int Days = 0;
            int.TryParse(days, out Days);
            if (Days > 0)
            {
                List<Lebi_Transport_Order> torders = B_Lebi_Transport_Order.GetList("Time_Add<'" + System.DateTime.Now.AddDays(0 - Days) + "' and Type_id_TransportOrderStatus=220", "");
                foreach (Lebi_Transport_Order torder in torders)
                {
                    torder.Type_id_TransportOrderStatus = 223;
                    torder.Time_Received = System.DateTime.Now;
                    B_Lebi_Transport_Order.Update(torder);
                    EX_Area.UpdateShouHuoCount(torder);
                    Log.Add("自动确认收货", "Order", torder.Order_id.ToString());
                }
            }

            days = conf.OrderCompleteDays;
            Days = 0;
            int.TryParse(days, out Days);
            if (Days > 0)
            {
                List<Lebi_Order> orders = B_Lebi_Order.GetList("Time_Received<'" + System.DateTime.Now.AddDays(0 - Days) + "' and IsReceived_All=1 and IsCompleted=0", "");
                foreach (Lebi_Order order in orders)
                {
                    Order.Order_Completed(order);
                    Log.Add("自动确认完成", "Order", order.id.ToString());
                }
            }
        }

        /// <summary>
        /// 订单自动售货确认-1小时
        /// </summary>
        public void orderReceived_start()
        {


            timer_work_orderReceived = new System.Timers.Timer(1000 * 60 * 60);//1000 * 60 表示1分钟
            timer_work_orderReceived.Elapsed += new System.Timers.ElapsedEventHandler(orderReceived);
            timer_work_orderReceived.Enabled = true;
            timer_work_orderReceived.Start();

        }
        #endregion

        #region 常购清单自动邮件
        /// <summary>
        /// 常购清单自动邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changgouqingdEmail(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (conf.MailSign.Contains("changgouqingdan"))
            {
                List<Lebi_User_Product> pros = B_Lebi_User_Product.GetList("Type_id_UserProductType=144 and Time_addemail='" + DateTime.Today + "'", "");
                foreach (Lebi_User_Product pro in pros)
                {
                    Email.SendEmail_changgouqingdan(pro);
                    pro.Time_addemail = DateTime.Today.AddDays(pro.WarnDays);
                    B_Lebi_User_Product.Update(pro);
                }
            }
        }

        /// <summary>
        /// 常购清单自动邮件-1小时
        /// </summary>
        public void changgouqingdEmail_start()
        {


            timer_work_changgouqingdEmail = new System.Timers.Timer(1000 * 60 * 60);//1000 * 60 表示1分钟
            timer_work_changgouqingdEmail.Elapsed += new System.Timers.ElapsedEventHandler(changgouqingdEmail);
            timer_work_changgouqingdEmail.Enabled = true;
            timer_work_changgouqingdEmail.Start();

        }
        #endregion
        #region 十分钟自动执行事件

        /// <summary>
        /// 十分钟自动执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoAction_10(object sender, System.Timers.ElapsedEventArgs e)
        {

            string lockfile_10m = System.Web.HttpRuntime.AppDomainAppPath + "temp/AutoAction_10m_lock.txt";
            FileStream objFileStream = null;
            try
            {
                objFileStream = new FileStream(lockfile_10m, FileMode.Append, FileAccess.Write, FileShare.None);
            }
            catch (Exception)
            {
                if (objFileStream != null)
                    objFileStream.Close();
                return;
            }
            try
            {
                AutoAction_10m();
            }
            catch (Exception)
            {

            }
            if (objFileStream != null)
                objFileStream.Close();
        }

        /// <summary>
        /// 十分钟自动执行-启动
        /// </summary>
        public void AutoAction_10_start()
        {


            timer_work_AutoAction10 = new System.Timers.Timer(1000 * 60 * 10);//1000 * 60 表示1分钟
            timer_work_AutoAction10.Elapsed += new System.Timers.ElapsedEventHandler(AutoAction_10);
            timer_work_AutoAction10.Enabled = true;
            timer_work_AutoAction10.Start();

        }
        #endregion

        #region 1分钟自动执行事件

        /// <summary>
        /// 十分钟自动执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoAction_1m(object sender, System.Timers.ElapsedEventArgs e)
        {
            string lockfile = System.Web.HttpRuntime.AppDomainAppPath + "temp/AutoAction_1m_lock.txt";
            FileStream objFileStream = null;
            try
            {
                objFileStream = new FileStream(lockfile, FileMode.Append, FileAccess.Write, FileShare.None);
            }
            catch (Exception)
            {
                if (objFileStream != null)
                    objFileStream.Close();
                return;
            }
            try
            {
                AutoAction_1m();
            }
            catch (Exception)
            {

            }
            if (objFileStream != null)
                objFileStream.Close();
        }

        /// <summary>
        /// 十分钟自动执行-启动
        /// </summary>
        public void AutoAction_1m_start()
        {


            timer_work_AutoAction1m = new System.Timers.Timer(1000 * 60);//1000 * 60 表示1分钟
            timer_work_AutoAction1m.Elapsed += new System.Timers.ElapsedEventHandler(AutoAction_1m);
            timer_work_AutoAction1m.Enabled = true;
            timer_work_AutoAction1m.Start();

        }
        #endregion
    }

}

