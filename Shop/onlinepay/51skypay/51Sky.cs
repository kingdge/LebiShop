using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Shop.Model;
using Shop.Bussiness;
namespace onlinepay.epay
{
    /// <summary>
    /// Sky 的摘要说明
    /// </summary>
    public static class epayCommon
    {
        public static string ToBase64(string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }

        public static string UnToBase64(string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
        public static string MD5(string str)
        {
            return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
    }
    public class SendOrder
    {
        decimal _ord_amt;
        /// <summary>
        /// 接口版本号
        /// </summary>
        public string ver { get; set; }
        /// <summary>
        /// 商户代号
        /// </summary>
        public string mrch_no { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string ord_no
        {
            get;
            set;
        }
        /// <summary>
        /// 订单生成日期
        /// </summary>
        public string ord_date { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal ord_amt
        {
            get { return Math.Round(_ord_amt, 2); }
            set { _ord_amt = value; }
        }
        /// <summary>
        /// 请求订单时检验码
        /// </summary>               
        public virtual string mac
        {
            get { return epayCommon.MD5(string.Format("ver{0}mrch_no{1}ord_no{2}ord_date{3}ord_amt{4}{5}", epayCommon.ToBase64(ver), epayCommon.ToBase64(mrch_no), epayCommon.ToBase64(ord_no), epayCommon.ToBase64(ord_date), epayCommon.ToBase64(ord_amt.ToString()), key)).ToUpper(); }
        }
        /// <summary>
        /// 私钥
        /// </summary>
        internal string key
        {
            get
            {
                Lebi_OnlinePay pay = B_Lebi_OnlinePay.GetModel("Code='95epay'");
                if (pay == null)
                {
                    return "LdorD3927kdDLi";
                }
                return pay.UserKey;
            }
        }

    }
    public class ROrder : SendOrder
    {
        /// <summary>
        /// 交互软件生成的订单编码
        /// </summary>
        public string ord_seq { get; set; }
        /// <summary>
        /// 订单交易号（由购物卡网网站提供）
        /// </summary>
        public string sno { get; set; }
        /// <summary>
        /// 订单完成状态ID 1成功
        /// </summary>
        public string ord_status { get; set; }
        /// <summary>
        /// 订单完成结果
        /// </summary>
        public string ord_result { get; set; }
        /// <summary>
        /// 交易附加信息
        /// </summary>
        public string add_msg { get; set; }
        /// <summary>
        /// 接收后计算的检验码
        /// </summary>               
        public override string mac
        {
            get
            {
                return epayCommon.MD5("ver" + epayCommon.ToBase64(ver) +
                        "mrch_no" + epayCommon.ToBase64(mrch_no) +
                        "ord_no" + epayCommon.ToBase64(ord_no) +
                        "ord_date" + epayCommon.ToBase64(ord_date) +
                        "ord_amt" + epayCommon.ToBase64(ord_amt.ToString()) +
                        "ord_seq" + epayCommon.ToBase64(ord_seq) +
                        "sno" + epayCommon.ToBase64(sno) +
                        "ord_status" + epayCommon.ToBase64(ord_status) +
                        "ord_result" + epayCommon.ToBase64(ord_result) +
                        "add_msg" + epayCommon.ToBase64(add_msg) +
                          key).ToUpper();
            }
        }
    }
}