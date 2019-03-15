using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_User
	{
		#region Model
		private int _id=0;
		private string _username="";
		private string _password="";
		private string _jypwd="";
		private string _email="";
		private string _realname="";
		private int _lnum=0;
		private string _sex="";
		private string _nickname="";
		private DateTime _birthday=DateTime.Now;
		private string _pwdwen="";
		private string _pwdda="";
		private decimal _point=0;
		private string _uavatar="";
		private string _city="";
		private int _area_id=0;
		private string _address="";
		private string _mobilephone="";
		private string _phone="";
		private string _qq="";
		private string _fax="";
		private string _postalcode="";
		private string _msn="";
		private decimal _yfmoney=0;
		private decimal _money=0;
		private decimal _money_xiaofei=0;
		private int _count_login=0;
		private string _ip_last="";
		private string _ip_this="";
		private DateTime _time_this=DateTime.Now;
		private DateTime _time_last=DateTime.Now;
		private DateTime _time_reg=DateTime.Now;
		private int _upass=0;
		private int _isopen=0;
		private string _introduce="";
		private int _userlevel_id=0;
		private DateTime _time_lastorder=DateTime.Now;
		private int _user_address_id=0;
		private string _transport_price_id="";
		private int _pay_id=0;
		private int _onlinepay_id=0;
		private int _count_order=0;
		private int _count_order_ok=0;
		private string _language="";
		private string _checkcode="";
		private int _currency_id=0;
		private string _currency_code="";
		private string _face="";
		private string _bind_qq_id="";
		private string _bind_qq_token="";
		private string _bind_qq_nickname="";
		private string _bind_weibo_id="";
		private string _bind_weibo_token="";
		private string _bind_weibo_nickname="";
		private string _bind_taobao_id="";
		private string _bind_taobao_token="";
		private string _bind_taobao_nickname="";
		private string _bind_facebook_id="";
		private string _bind_facebook_token="";
		private string _bind_facebook_nickname="";
		private int _isplatformaccount=0;
		private int _isanonymous=0;
		private int _user_id_parent=0;
		private string _pay_password="";
		private decimal _agentmoney=0;
		private decimal _agentmoney_history=0;
		private int _count_sonuser=0;
		private string _cashaccount_code="";
		private string _cashaccount_name="";
		private string _cashaccount_bank="";
		private string _bind_weixin_nickname="";
		private string _bind_weixin_token="";
		private string _bind_weixin_id="";
		private int _site_id=0;
		private string _usernumber="";
		private string _weixin="";
		private string _alipay="";
		private string _aliwangwang="";
		private string _momo="";
		private string _job="";
		private string _idnumber="";
		private string _idtype="";
		private decimal _money_order=0;
		private decimal _money_product=0;
		private decimal _money_transport=0;
		private decimal _money_bill=0;
		private DateTime _time_end=DateTime.Now;
		private int _ischeckedemail=0;
		private int _ischeckedmobilephone=0;
		private int _randnum=0;
		private string _pickup_id="";
		private DateTime _pickup_date=DateTime.Now;
		private string _device_id="";
		private string _device_system="";
		private decimal _money_fanxian=0;
		private int _dt_id=0;
		private int _isdel=0;
		private Lebi_User _model;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string JYpwd
		{
			set{ _jypwd=value;}
			get{return _jypwd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int lnum
		{
			set{ _lnum=value;}
			get{return _lnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pwdwen
		{
			set{ _pwdwen=value;}
			get{return _pwdwen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pwdda
		{
			set{ _pwdda=value;}
			get{return _pwdda;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Uavatar
		{
			set{ _uavatar=value;}
			get{return _uavatar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Postalcode
		{
			set{ _postalcode=value;}
			get{return _postalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Msn
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Yfmoney
		{
			set{ _yfmoney=value;}
			get{return _yfmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_xiaofei
		{
			set{ _money_xiaofei=value;}
			get{return _money_xiaofei;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Login
		{
			set{ _count_login=value;}
			get{return _count_login;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP_Last
		{
			set{ _ip_last=value;}
			get{return _ip_last;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP_This
		{
			set{ _ip_this=value;}
			get{return _ip_this;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_This
		{
			set{ _time_this=value;}
			get{return _time_this;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Last
		{
			set{ _time_last=value;}
			get{return _time_last;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Reg
		{
			set{ _time_reg=value;}
			get{return _time_reg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Upass
		{
			set{ _upass=value;}
			get{return _upass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsOpen
		{
			set{ _isopen=value;}
			get{return _isopen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Introduce
		{
			set{ _introduce=value;}
			get{return _introduce;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserLevel_id
		{
			set{ _userlevel_id=value;}
			get{return _userlevel_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_lastorder
		{
			set{ _time_lastorder=value;}
			get{return _time_lastorder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_Address_id
		{
			set{ _user_address_id=value;}
			get{return _user_address_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transport_Price_id
		{
			set{ _transport_price_id=value;}
			get{return _transport_price_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Pay_id
		{
			set{ _pay_id=value;}
			get{return _pay_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OnlinePay_id
		{
			set{ _onlinepay_id=value;}
			get{return _onlinepay_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Order
		{
			set{ _count_order=value;}
			get{return _count_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Order_OK
		{
			set{ _count_order_ok=value;}
			get{return _count_order_ok;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CheckCode
		{
			set{ _checkcode=value;}
			get{return _checkcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Currency_Code
		{
			set{ _currency_code=value;}
			get{return _currency_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Face
		{
			set{ _face=value;}
			get{return _face;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_qq_id
		{
			set{ _bind_qq_id=value;}
			get{return _bind_qq_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_qq_token
		{
			set{ _bind_qq_token=value;}
			get{return _bind_qq_token;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_qq_nickname
		{
			set{ _bind_qq_nickname=value;}
			get{return _bind_qq_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_weibo_id
		{
			set{ _bind_weibo_id=value;}
			get{return _bind_weibo_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_weibo_token
		{
			set{ _bind_weibo_token=value;}
			get{return _bind_weibo_token;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_weibo_nickname
		{
			set{ _bind_weibo_nickname=value;}
			get{return _bind_weibo_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_taobao_id
		{
			set{ _bind_taobao_id=value;}
			get{return _bind_taobao_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_taobao_token
		{
			set{ _bind_taobao_token=value;}
			get{return _bind_taobao_token;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_taobao_nickname
		{
			set{ _bind_taobao_nickname=value;}
			get{return _bind_taobao_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_facebook_id
		{
			set{ _bind_facebook_id=value;}
			get{return _bind_facebook_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_facebook_token
		{
			set{ _bind_facebook_token=value;}
			get{return _bind_facebook_token;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_facebook_nickname
		{
			set{ _bind_facebook_nickname=value;}
			get{return _bind_facebook_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPlatformAccount
		{
			set{ _isplatformaccount=value;}
			get{return _isplatformaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsAnonymous
		{
			set{ _isanonymous=value;}
			get{return _isanonymous;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id_parent
		{
			set{ _user_id_parent=value;}
			get{return _user_id_parent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pay_Password
		{
			set{ _pay_password=value;}
			get{return _pay_password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal AgentMoney
		{
			set{ _agentmoney=value;}
			get{return _agentmoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal AgentMoney_history
		{
			set{ _agentmoney_history=value;}
			get{return _agentmoney_history;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_sonuser
		{
			set{ _count_sonuser=value;}
			get{return _count_sonuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CashAccount_Code
		{
			set{ _cashaccount_code=value;}
			get{return _cashaccount_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CashAccount_Name
		{
			set{ _cashaccount_name=value;}
			get{return _cashaccount_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CashAccount_Bank
		{
			set{ _cashaccount_bank=value;}
			get{return _cashaccount_bank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_weixin_nickname
		{
			set{ _bind_weixin_nickname=value;}
			get{return _bind_weixin_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_weixin_token
		{
			set{ _bind_weixin_token=value;}
			get{return _bind_weixin_token;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string bind_weixin_id
		{
			set{ _bind_weixin_id=value;}
			get{return _bind_weixin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Site_id
		{
			set{ _site_id=value;}
			get{return _site_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserNumber
		{
			set{ _usernumber=value;}
			get{return _usernumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string weixin
		{
			set{ _weixin=value;}
			get{return _weixin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string alipay
		{
			set{ _alipay=value;}
			get{return _alipay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string aliwangwang
		{
			set{ _aliwangwang=value;}
			get{return _aliwangwang;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string momo
		{
			set{ _momo=value;}
			get{return _momo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string job
		{
			set{ _job=value;}
			get{return _job;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IDNumber
		{
			set{ _idnumber=value;}
			get{return _idnumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IDType
		{
			set{ _idtype=value;}
			get{return _idtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Order
		{
			set{ _money_order=value;}
			get{return _money_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Product
		{
			set{ _money_product=value;}
			get{return _money_product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Transport
		{
			set{ _money_transport=value;}
			get{return _money_transport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Bill
		{
			set{ _money_bill=value;}
			get{return _money_bill;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_End
		{
			set{ _time_end=value;}
			get{return _time_end;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCheckedEmail
		{
			set{ _ischeckedemail=value;}
			get{return _ischeckedemail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCheckedMobilePhone
		{
			set{ _ischeckedmobilephone=value;}
			get{return _ischeckedmobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RandNum
		{
			set{ _randnum=value;}
			get{return _randnum;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PickUp_id
		{
			set{ _pickup_id=value;}
			get{return _pickup_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime PickUp_Date
		{
			set{ _pickup_date=value;}
			get{return _pickup_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Device_id
		{
			set{ _device_id=value;}
			get{return _device_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Device_system
		{
			set{ _device_system=value;}
			get{return _device_system;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_fanxian
		{
			set{ _money_fanxian=value;}
			get{return _money_fanxian;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		#endregion

	}
}

