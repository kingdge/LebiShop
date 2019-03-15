using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User
	{
		#region Model
		private int _id=0;
		private string _address="";
		private decimal _agentmoney=0;
		private decimal _agentmoney_history=0;
		private string _alipay="";
		private string _aliwangwang="";
		private int _area_id=0;
		private string _bind_facebook_id="";
		private string _bind_facebook_nickname="";
		private string _bind_facebook_token="";
		private string _bind_qq_id="";
		private string _bind_qq_nickname="";
		private string _bind_qq_token="";
		private string _bind_taobao_id="";
		private string _bind_taobao_nickname="";
		private string _bind_taobao_token="";
		private string _bind_weibo_id="";
		private string _bind_weibo_nickname="";
		private string _bind_weibo_token="";
		private string _bind_weixin_id="";
		private string _bind_weixin_nickname="";
		private string _bind_weixin_token="";
		private DateTime _birthday=DateTime.Now;
		private string _cashaccount_bank="";
		private string _cashaccount_code="";
		private string _cashaccount_name="";
		private string _checkcode="";
		private string _city="";
		private int _count_login=0;
		private int _count_order=0;
		private int _count_order_ok=0;
		private int _count_sonuser=0;
		private string _currency_code="";
		private int _currency_id=0;
		private string _device_id="";
		private string _device_system="";
		private int _dt_id=0;
		private string _email="";
		private string _face="";
		private string _fax="";
		private string _idnumber="";
		private string _idtype="";
		private string _introduce="";
		private string _ip_last="";
		private string _ip_this="";
		private int _isanonymous=0;
		private int _ischeckedemail=0;
		private int _ischeckedmobilephone=0;
		private int _isdel=0;
		private int _isopen=0;
		private int _isplatformaccount=0;
		private string _job="";
		private string _jypwd="";
		private string _language="";
		private int _lnum=0;
		private string _mobilephone="";
		private string _momo="";
		private decimal _money=0;
		private decimal _money_bill=0;
		private decimal _money_fanxian=0;
		private decimal _money_order=0;
		private decimal _money_product=0;
		private decimal _money_transport=0;
		private decimal _money_xiaofei=0;
		private string _msn="";
		private string _nickname="";
		private int _onlinepay_id=0;
		private string _password="";
		private int _pay_id=0;
		private string _pay_password="";
		private string _phone="";
		private DateTime _pickup_date=DateTime.Now;
		private string _pickup_id="";
		private decimal _point=0;
		private string _postalcode="";
		private string _pwdda="";
		private string _pwdwen="";
		private string _qq="";
		private int _randnum=0;
		private string _realname="";
		private string _sex="";
		private int _site_id=0;
		private DateTime _time_end=DateTime.Now;
		private DateTime _time_last=DateTime.Now;
		private DateTime _time_lastorder=DateTime.Now;
		private DateTime _time_reg=DateTime.Now;
		private DateTime _time_this=DateTime.Now;
		private string _transport_price_id="";
		private string _uavatar="";
		private int _upass=0;
		private int _user_address_id=0;
		private int _user_id_parent=0;
		private int _userlevel_id=0;
		private string _username="";
		private string _usernumber="";
		private string _weixin="";
		private decimal _yfmoney=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		public decimal AgentMoney
		{
			set{ _agentmoney=value;}
			get{return _agentmoney;}
		}
		public decimal AgentMoney_history
		{
			set{ _agentmoney_history=value;}
			get{return _agentmoney_history;}
		}
		public string alipay
		{
			set{ _alipay=value;}
			get{return _alipay;}
		}
		public string aliwangwang
		{
			set{ _aliwangwang=value;}
			get{return _aliwangwang;}
		}
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
		}
		public string bind_facebook_id
		{
			set{ _bind_facebook_id=value;}
			get{return _bind_facebook_id;}
		}
		public string bind_facebook_nickname
		{
			set{ _bind_facebook_nickname=value;}
			get{return _bind_facebook_nickname;}
		}
		public string bind_facebook_token
		{
			set{ _bind_facebook_token=value;}
			get{return _bind_facebook_token;}
		}
		public string bind_qq_id
		{
			set{ _bind_qq_id=value;}
			get{return _bind_qq_id;}
		}
		public string bind_qq_nickname
		{
			set{ _bind_qq_nickname=value;}
			get{return _bind_qq_nickname;}
		}
		public string bind_qq_token
		{
			set{ _bind_qq_token=value;}
			get{return _bind_qq_token;}
		}
		public string bind_taobao_id
		{
			set{ _bind_taobao_id=value;}
			get{return _bind_taobao_id;}
		}
		public string bind_taobao_nickname
		{
			set{ _bind_taobao_nickname=value;}
			get{return _bind_taobao_nickname;}
		}
		public string bind_taobao_token
		{
			set{ _bind_taobao_token=value;}
			get{return _bind_taobao_token;}
		}
		public string bind_weibo_id
		{
			set{ _bind_weibo_id=value;}
			get{return _bind_weibo_id;}
		}
		public string bind_weibo_nickname
		{
			set{ _bind_weibo_nickname=value;}
			get{return _bind_weibo_nickname;}
		}
		public string bind_weibo_token
		{
			set{ _bind_weibo_token=value;}
			get{return _bind_weibo_token;}
		}
		public string bind_weixin_id
		{
			set{ _bind_weixin_id=value;}
			get{return _bind_weixin_id;}
		}
		public string bind_weixin_nickname
		{
			set{ _bind_weixin_nickname=value;}
			get{return _bind_weixin_nickname;}
		}
		public string bind_weixin_token
		{
			set{ _bind_weixin_token=value;}
			get{return _bind_weixin_token;}
		}
		public DateTime Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		public string CashAccount_Bank
		{
			set{ _cashaccount_bank=value;}
			get{return _cashaccount_bank;}
		}
		public string CashAccount_Code
		{
			set{ _cashaccount_code=value;}
			get{return _cashaccount_code;}
		}
		public string CashAccount_Name
		{
			set{ _cashaccount_name=value;}
			get{return _cashaccount_name;}
		}
		public string CheckCode
		{
			set{ _checkcode=value;}
			get{return _checkcode;}
		}
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		public int Count_Login
		{
			set{ _count_login=value;}
			get{return _count_login;}
		}
		public int Count_Order
		{
			set{ _count_order=value;}
			get{return _count_order;}
		}
		public int Count_Order_OK
		{
			set{ _count_order_ok=value;}
			get{return _count_order_ok;}
		}
		public int Count_sonuser
		{
			set{ _count_sonuser=value;}
			get{return _count_sonuser;}
		}
		public string Currency_Code
		{
			set{ _currency_code=value;}
			get{return _currency_code;}
		}
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
		}
		public string Device_id
		{
			set{ _device_id=value;}
			get{return _device_id;}
		}
		public string Device_system
		{
			set{ _device_system=value;}
			get{return _device_system;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public string Face
		{
			set{ _face=value;}
			get{return _face;}
		}
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		public string IDNumber
		{
			set{ _idnumber=value;}
			get{return _idnumber;}
		}
		public string IDType
		{
			set{ _idtype=value;}
			get{return _idtype;}
		}
		public string Introduce
		{
			set{ _introduce=value;}
			get{return _introduce;}
		}
		public string IP_Last
		{
			set{ _ip_last=value;}
			get{return _ip_last;}
		}
		public string IP_This
		{
			set{ _ip_this=value;}
			get{return _ip_this;}
		}
		public int IsAnonymous
		{
			set{ _isanonymous=value;}
			get{return _isanonymous;}
		}
		public int IsCheckedEmail
		{
			set{ _ischeckedemail=value;}
			get{return _ischeckedemail;}
		}
		public int IsCheckedMobilePhone
		{
			set{ _ischeckedmobilephone=value;}
			get{return _ischeckedmobilephone;}
		}
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		public int IsOpen
		{
			set{ _isopen=value;}
			get{return _isopen;}
		}
		public int IsPlatformAccount
		{
			set{ _isplatformaccount=value;}
			get{return _isplatformaccount;}
		}
		public string job
		{
			set{ _job=value;}
			get{return _job;}
		}
		public string JYpwd
		{
			set{ _jypwd=value;}
			get{return _jypwd;}
		}
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public int lnum
		{
			set{ _lnum=value;}
			get{return _lnum;}
		}
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		public string momo
		{
			set{ _momo=value;}
			get{return _momo;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public decimal Money_Bill
		{
			set{ _money_bill=value;}
			get{return _money_bill;}
		}
		public decimal Money_fanxian
		{
			set{ _money_fanxian=value;}
			get{return _money_fanxian;}
		}
		public decimal Money_Order
		{
			set{ _money_order=value;}
			get{return _money_order;}
		}
		public decimal Money_Product
		{
			set{ _money_product=value;}
			get{return _money_product;}
		}
		public decimal Money_Transport
		{
			set{ _money_transport=value;}
			get{return _money_transport;}
		}
		public decimal Money_xiaofei
		{
			set{ _money_xiaofei=value;}
			get{return _money_xiaofei;}
		}
		public string Msn
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		public string NickName
		{
			set{ _nickname=value;}
			get{return _nickname;}
		}
		public int OnlinePay_id
		{
			set{ _onlinepay_id=value;}
			get{return _onlinepay_id;}
		}
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		public int Pay_id
		{
			set{ _pay_id=value;}
			get{return _pay_id;}
		}
		public string Pay_Password
		{
			set{ _pay_password=value;}
			get{return _pay_password;}
		}
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		public DateTime PickUp_Date
		{
			set{ _pickup_date=value;}
			get{return _pickup_date;}
		}
		public string PickUp_id
		{
			set{ _pickup_id=value;}
			get{return _pickup_id;}
		}
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		public string Postalcode
		{
			set{ _postalcode=value;}
			get{return _postalcode;}
		}
		public string pwdda
		{
			set{ _pwdda=value;}
			get{return _pwdda;}
		}
		public string pwdwen
		{
			set{ _pwdwen=value;}
			get{return _pwdwen;}
		}
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		public int RandNum
		{
			set{ _randnum=value;}
			get{return _randnum;}
		}
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		public int Site_id
		{
			set{ _site_id=value;}
			get{return _site_id;}
		}
		public DateTime Time_End
		{
			set{ _time_end=value;}
			get{return _time_end;}
		}
		public DateTime Time_Last
		{
			set{ _time_last=value;}
			get{return _time_last;}
		}
		public DateTime Time_lastorder
		{
			set{ _time_lastorder=value;}
			get{return _time_lastorder;}
		}
		public DateTime Time_Reg
		{
			set{ _time_reg=value;}
			get{return _time_reg;}
		}
		public DateTime Time_This
		{
			set{ _time_this=value;}
			get{return _time_this;}
		}
		public string Transport_Price_id
		{
			set{ _transport_price_id=value;}
			get{return _transport_price_id;}
		}
		public string Uavatar
		{
			set{ _uavatar=value;}
			get{return _uavatar;}
		}
		public int Upass
		{
			set{ _upass=value;}
			get{return _upass;}
		}
		public int User_Address_id
		{
			set{ _user_address_id=value;}
			get{return _user_address_id;}
		}
		public int User_id_parent
		{
			set{ _user_id_parent=value;}
			get{return _user_id_parent;}
		}
		public int UserLevel_id
		{
			set{ _userlevel_id=value;}
			get{return _userlevel_id;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string UserNumber
		{
			set{ _usernumber=value;}
			get{return _usernumber;}
		}
		public string weixin
		{
			set{ _weixin=value;}
			get{return _weixin;}
		}
		public decimal Yfmoney
		{
			set{ _yfmoney=value;}
			get{return _yfmoney;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

