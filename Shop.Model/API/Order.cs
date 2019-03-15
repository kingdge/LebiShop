using System;
using System.Collections.Generic;
namespace Shop.Model
{

    [Serializable]
    public class apiOrder
    {
        public apiOrder()
        { }
        #region Model
        private int _id = 0;
        private string _code = "";
        private int _user_id = 0;
        private string _user_username = "";
        private string _t_name = "";
        private string _t_area = "";
        private string _t_address = "";
        private string _t_phone = "";
        private string _t_mobilephone = "";
        private string _t_postalcode = "";
        private string _t_email = "";
        private string _remark_user = "";
        private string _pay = "";
        private string _onlinepay = "";
        private string _onlinepay_code = "";
        private decimal _money_order = 0;
        private decimal _money_product = 0;
        private decimal _money_transport = 0;
        private decimal _money_bill = 0;
        private decimal _money_market = 0;
        private decimal _weight = 0;
        private decimal _volume = 0;
        private decimal _point = 0;
        private decimal _point_buy = 0;
        private string _transport_name = "";
        private string _transport_code = "";
        private string _transport_mark = "";
        private int _isverified = 0;
        private int _ispaid = 0;
        private int _isshipped = 0;
        private int _isshipped_all = 0;
        private int _isreceived = 0;
        private int _isreceived_all = 0;
        private int _iscompleted = 0;
        private int _isinvalid = 0;
        private DateTime _time_add = DateTime.Now;
        private DateTime _time_verified = DateTime.Now;
        private DateTime _time_paid = DateTime.Now;
        private DateTime _time_shipped = DateTime.Now;
        private DateTime _time_received = DateTime.Now;
        private DateTime _time_completed = DateTime.Now;
        private string _remark_admin = "";
        private string _ordertype = "";
        private string _currency_code = "";
        private decimal _currency_exchangerate = 0;
        private string _currency_msige = "";
        private List<apiOrderProduct> _products;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int User_id
        {
            set { _user_id = value; }
            get { return _user_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string User_UserName
        {
            set { _user_username = value; }
            get { return _user_username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string T_Name
        {
            set { _t_name = value; }
            get { return _t_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string T_Area
        {
            set { _t_area = value; }
            get { return _t_area; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string T_Address
        {
            set { _t_address = value; }
            get { return _t_address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string T_Phone
        {
            set { _t_phone = value; }
            get { return _t_phone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string T_MobilePhone
        {
            set { _t_mobilephone = value; }
            get { return _t_mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string T_Postalcode
        {
            set { _t_postalcode = value; }
            get { return _t_postalcode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string T_Email
        {
            set { _t_email = value; }
            get { return _t_email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark_User
        {
            set { _remark_user = value; }
            get { return _remark_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Pay
        {
            set { _pay = value; }
            get { return _pay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OnlinePay
        {
            set { _onlinepay = value; }
            get { return _onlinepay; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OnlinePay_Code
        {
            set { _onlinepay_code = value; }
            get { return _onlinepay_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Money_Order
        {
            set { _money_order = value; }
            get { return _money_order; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal Money_Product
        {
            set { _money_product = value; }
            get { return _money_product; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Money_Transport
        {
            set { _money_transport = value; }
            get { return _money_transport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Money_Bill
        {
            set { _money_bill = value; }
            get { return _money_bill; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Money_Market
        {
            set { _money_market = value; }
            get { return _money_market; }
        }

        /// <summary>
        /// 
        /// </summary>
        public decimal Weight
        {
            set { _weight = value; }
            get { return _weight; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Volume
        {
            set { _volume = value; }
            get { return _volume; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Point
        {
            set { _point = value; }
            get { return _point; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Point_Buy
        {
            set { _point_buy = value; }
            get { return _point_buy; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Transport_Name
        {
            set { _transport_name = value; }
            get { return _transport_name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Transport_Code
        {
            set { _transport_code = value; }
            get { return _transport_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Transport_Mark
        {
            set { _transport_mark = value; }
            get { return _transport_mark; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int IsVerified
        {
            set { _isverified = value; }
            get { return _isverified; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsPaid
        {
            set { _ispaid = value; }
            get { return _ispaid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsShipped
        {
            set { _isshipped = value; }
            get { return _isshipped; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsShipped_All
        {
            set { _isshipped_all = value; }
            get { return _isshipped_all; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsReceived
        {
            set { _isreceived = value; }
            get { return _isreceived; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsReceived_All
        {
            set { _isreceived_all = value; }
            get { return _isreceived_all; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsCompleted
        {
            set { _iscompleted = value; }
            get { return _iscompleted; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsInvalid
        {
            set { _isinvalid = value; }
            get { return _isinvalid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Add
        {
            set { _time_add = value; }
            get { return _time_add; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Verified
        {
            set { _time_verified = value; }
            get { return _time_verified; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Paid
        {
            set { _time_paid = value; }
            get { return _time_paid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Shipped
        {
            set { _time_shipped = value; }
            get { return _time_shipped; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Received
        {
            set { _time_received = value; }
            get { return _time_received; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime Time_Completed
        {
            set { _time_completed = value; }
            get { return _time_completed; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Remark_Admin
        {
            set { _remark_admin = value; }
            get { return _remark_admin; }
        }

        /// <summary>
        /// 211订购单,212退货单,213订购子订单,214充值单,215订单组
        /// </summary>
        public string OrderType
        {
            set { _ordertype = value; }
            get { return _ordertype; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Currency_Code
        {
            set { _currency_code = value; }
            get { return _currency_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Currency_ExchangeRate
        {
            set { _currency_exchangerate = value; }
            get { return _currency_exchangerate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Currency_Msige
        {
            set { _currency_msige = value; }
            get { return _currency_msige; }
        }
        public List<apiOrderProduct> Products
        {
            set { _products = value; }
            get { return _products; }
        }
        #endregion Model

    }
    [Serializable]
    public class apiOrderProduct
    {
        public apiOrderProduct()
        { }
        public int Count
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Number
        {
            get;
            set;
        }
        public string ImageSmall
        {
            get;
            set;
        }
        public string ImageBig
        {
            get;
            set;
        }
        public string Guige
        {
            get;
            set;
        }
        public decimal Price
        {
            get;
            set;
        }
        public string URL
        {
            get;
            set;
        }


    }
}

