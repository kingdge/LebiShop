using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Linq;
using Shop.Model;using DB.LebiShop;
using LB.Tools;
using LB.DataAccess;
using System.Web.Script.Serialization;

namespace Shop.Bussiness
{
    public class Money
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="money"></param>
        /// <param name="type"></param>
        /// <param name="admin"></param>
        /// <param name="description"></param>
        /// <param name="remark"></param>
        /// <param name="fanxian">是否包含返现款消费</param>
        public static void AddMoney(Lebi_User user, decimal money, int type, Lebi_Administrator admin, string description, string remark, bool fanxian = true)
        {
            AddMoney(user,money,type,null,admin,description,remark,fanxian);
        }
        public static void AddMoney(Lebi_User user, decimal money, int type, Lebi_Order order, Lebi_Administrator admin, string description, string remark, bool fanxian = true)
        {
            if (money == 0)
                return;
            if (type == 192 || type == 193)
            {
                if (money > 0)
                {
                    money = 0 - money;
                }
            }
            Lebi_User_Money mo = new Lebi_User_Money();
            if (admin != null)
            {
                mo.Admin_id = admin.id;
                mo.Admin_UserName = admin.UserName;
            }
            mo.Money = money;
            mo.Type_id_MoneyStatus = 181;
            mo.Type_id_MoneyType = type;
            mo.User_id = user.id;
            mo.User_UserName = user.UserName;
            mo.User_RealName = user.RealName;
            mo.Description = description;
            mo.Remark = remark;
            if (order != null)
            {
                mo.Order_id = order.id;
                mo.Order_Code = order.Code;
                mo.Order_PayNo = order.PayNo;
            }
            //string money_ = B_Lebi_User_Money.GetValue("sum(Money)", "User_id=" + user.id + " and Type_id_MoneyStatus=181");
            //decimal Money = 0;
            //Decimal.TryParse(money_, out Money);
            if (type == 195)//返现
            {
                user.Money_fanxian = user.Money_fanxian + money;
            }
            if (type == 192 && fanxian == true)//消费
            {

                user.Money_fanxian = user.Money_fanxian + money;
                if (user.Money_fanxian < 0)
                    user.Money_fanxian = 0;
            }
            user.Money = user.Money + money;
            B_Lebi_User.Update(user);
            mo.Money_after = user.Money;
            mo.Money_fanxian_after = user.Money_fanxian;
            B_Lebi_User_Money.Add(mo);
            //发送短信
            SMS.SendSMS_balance(user);
            //APP推送
            APP.Push_balance(user);
        }
        /// <summary>
        /// 更新会员资金
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateUserMoney(Lebi_User user)
        {
            string money = B_Lebi_User_Money.GetValue("sum(Money)", "User_id=" + user.id + " and Type_id_MoneyStatus=181");
            decimal Money = 0;
            Decimal.TryParse(money, out Money);
            user.Money = Money;
            B_Lebi_User.Update(user);
        }

        /// <summary>
        /// 获得在线付款接口参数MODEL
        /// </summary>
        /// <param name="code"></param>
        /// <param name="order"></param>
        public static Lebi_OnlinePay GetOnlinePay(Lebi_Order order, string code)
        {
            if (order == null)
                return null;
            string where = "Code='" + code + "'";
            if (string.IsNullOrEmpty(order.KeyCode))
            {
                if (order.IsSupplierCash == 0 && order.Language_id > 0)
                    where += " and ','+Language_ids+',' like '%," + order.Language_id + ",%'";
            }
            Lebi_OnlinePay pay = B_Lebi_OnlinePay.GetModel(where);
            string log = "";
            if (pay == null)
            {
                pay = B_Lebi_OnlinePay.GetModel("Code='" + code + "'");
            }
            if (pay == null)
            {
                log = "在线支付接口 " + code + " 配置错误";
                Log.Add(log);
            }
            else
            {
                if (order.IsSupplierCash == 1)
                {
                    Lebi_Supplier supplier = B_Lebi_Supplier.GetModel(order.Supplier_id);
                    if (supplier == null)
                    {
                        supplier = new Lebi_Supplier();
                        log = "在线支付接口 " + code + " 配置错误[供应商ID:" + order.Supplier_id + "]";
                        Log.Add(log);
                        return pay;
                    }
                    Lebi_OnlinePay supplierpay = B_Lebi_OnlinePay.GetModel("Code='" + code + "' and Supplier_id=" + supplier.id + "");
                    if (supplierpay == null)
                    {
                        log = "在线支付接口 " + code + " 配置错误[供应商:" + supplier.SubName + ",ID:" + order.Supplier_id + "]";
                        Log.Add(log);
                        return pay;
                    }
                    supplierpay.TypeName = pay.TypeName;
                    supplierpay.Url = pay.Url;
                    supplierpay.Code = pay.Code;
                    supplierpay.Currency_Code = pay.Currency_Code;
                    supplierpay.Currency_id = pay.Currency_id;
                    supplierpay.Currency_Name = pay.Currency_Name;
                    return supplierpay;

                }
            }
            return pay;
        }
        public static Lebi_OnlinePay GetOnlinePay(string ordercode, string paycode)
        {
            Lebi_Order order = B_Lebi_Order.GetModel("Code=lbsql{'" + ordercode + "'}");
            return GetOnlinePay(order, paycode);
        }
    }
}

