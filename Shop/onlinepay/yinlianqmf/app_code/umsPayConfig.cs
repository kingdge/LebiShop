using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Shop.Model;using DB.LebiShop;
using Shop.Bussiness;
using LB.Tools;
namespace yinlianqmf
{

    public class UmsPayConfig
    {
        #region 字段定义

        /// <summary>
        /// 商户私钥
        /// </summary>
        private string _privateKey = string.Empty;

        /// <summary>
        /// 银商公钥
        /// </summary>
        private string _publicKey = string.Empty;

        /// <summary>
        /// 下单地址
        /// </summary>
        private string _orderUrl = string.Empty;

        /// <summary>
        /// 订单查询地址
        /// </summary>
        private string _queryOrderUrl = string.Empty;

        /// <summary>
        /// 商户后台接收支付结果成功通知地址
        /// </summary>
        private string _notifyUrl = string.Empty;

        /// <summary>
        /// 商户号
        /// </summary>
        private string _merId = string.Empty;

        /// <summary>
        /// 终端号
        /// </summary>
        private string _merTermId = string.Empty;

        #endregion

        /// <summary>
        /// 初始化支付参数信息
        /// </summary>
        public UmsPayConfig(Lebi_OnlinePay pay)
        {

            //Lebi_OnlinePay pay = Money.GetOnlinePay(order, "yinlianqmf");
            //测试
            //商户号 898000093990002 终端号 99999999 
            // 下单地址 https://116.228.21.162:8603/merFrontMgr/orderBusinessServlet 
            // 订单查询地址 https://116.228.21.162:8603/merFrontMgr/orderQueryServlet 
            // 银商验签公钥 http://116.228.21.162:9149/download_qmf/spackage 下载 
            // 商户签名私钥 http://116.228.21.162:9149/download_qmf/spackage 
            //正式
            //下单查询地址https://mpos.quanminfu.com:6004/merFrontMgr/orderBusinessServlet 
            //订单查询地址 https://mpos.quanminfu.com:6004/merFrontMgr/orderQueryServlet 
            _privateKey = pay.UserKey;
            _publicKey = pay.Email;
            _orderUrl = "https://mpos.quanminfu.com:6004/merFrontMgr/orderBusinessServlet";
            _queryOrderUrl = "https://mpos.quanminfu.com:6004/merFrontMgr/orderQueryServlet";
            Shop.Bussiness.Site site = new Shop.Bussiness.Site();
            _notifyUrl = "http://" + RequestTool.GetRequestDomain() + site.WebPath + "/onlinepay/yinlianqmf/notify_url.aspx"; ;
            _merId = pay.UserName;
            _merTermId = pay.terminal;
        }
        #region 属性

        /// <summary>
        /// 商户私钥
        /// </summary>
        public string PrivateKey
        {
            get { return _privateKey; }
        }

        /// <summary>
        /// 银商公钥
        /// </summary>
        public string PublicKey
        {
            get { return _publicKey; }
        }

        /// <summary>
        /// 下单地址
        /// </summary>
        public string OrderUrl
        {
            get { return _orderUrl; }
        }

        /// <summary>
        /// 订单查询地址
        /// </summary>
        public string QueryOrderUrl
        {
            get { return _queryOrderUrl; }
        }

        /// <summary>
        /// 商户后台接收支付结果成功通知地址
        /// </summary>
        public string NotifyUrl
        {
            get { return _notifyUrl; }
        }

        /// <summary>
        /// 商户号
        /// </summary>
        public string MerId
        {
            get { return _merId; }
        }

        /// <summary>
        /// 终端号
        /// </summary>
        public string MerTermId
        {
            get { return _merTermId; }
        }

        #endregion
    }


    public class PayMessage
    {
        public bool IsSuccess { get; set; }
        public string Msg { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public Dictionary<string, string> OtherData { get; set; }

    }
}
