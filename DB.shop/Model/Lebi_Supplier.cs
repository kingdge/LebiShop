using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier
	{
		#region Model
		private int _id=0;
		private string _address="";
		private int _area_id=0;
		private int _billingdays=0;
		private string _classname="";
		private string _company="";
		private int _count_login=0;
		private int _days_checkuserlow=0;
		private string _description="";
		private string _domain="";
		private string _email="";
		private string _fax="";
		private string _freezeremark="";
		private string _head="";
		private string _ip_last="";
		private string _ip_this="";
		private int _iscash=0;
		private int _isspread=0;
		private int _issuppliertransport=0;
		private string _language="";
		private int _level_id=0;
		private string _logo="";
		private string _longbar="";
		private string _mobilephone="";
		private decimal _money_margin_pay=0;
		private string _msn="";
		private string _name="";
		private string _password="";
		private string _phone="";
		private decimal _pointtomoney=0;
		private string _postalcode="";
		private int _producttop=0;
		private string _qq="";
		private string _realname="";
		private string _remark="";
		private string _seo_description="";
		private string _seo_keywords="";
		private string _seo_title="";
		private string _servicepanel="";
		private string _sex="";
		private string _shortbar="";
		private int _status=0;
		private string _subname="";
		private int _supplier_group_id=0;
		private int _supplier_skin_id=0;
		private string _suppliernumber="";
		private DateTime _time_begin=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private DateTime _time_last=DateTime.Now;
		private DateTime _time_reg=DateTime.Now;
		private DateTime _time_this=DateTime.Now;
		private int _type_id_supplierstatus=0;
		private int _user_id=0;
		private int _userlow=0;
		private string _username="";
		private int _usertop=0;
		private decimal _money=0;
		private decimal _money_service=0;
		private decimal _money_margin=0;
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
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
		}
		public int BillingDays
		{
			set{ _billingdays=value;}
			get{return _billingdays;}
		}
		public string ClassName
		{
			set{ _classname=value;}
			get{return _classname;}
		}
		public string Company
		{
			set{ _company=value;}
			get{return _company;}
		}
		public int Count_Login
		{
			set{ _count_login=value;}
			get{return _count_login;}
		}
		public int Days_checkuserlow
		{
			set{ _days_checkuserlow=value;}
			get{return _days_checkuserlow;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		public string FreezeRemark
		{
			set{ _freezeremark=value;}
			get{return _freezeremark;}
		}
		public string head
		{
			set{ _head=value;}
			get{return _head;}
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
		public int IsCash
		{
			set{ _iscash=value;}
			get{return _iscash;}
		}
		public int IsSpread
		{
			set{ _isspread=value;}
			get{return _isspread;}
		}
		public int IsSupplierTransport
		{
			set{ _issuppliertransport=value;}
			get{return _issuppliertransport;}
		}
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public int Level_id
		{
			set{ _level_id=value;}
			get{return _level_id;}
		}
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		public string longbar
		{
			set{ _longbar=value;}
			get{return _longbar;}
		}
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		public decimal Money_Margin_pay
		{
			set{ _money_margin_pay=value;}
			get{return _money_margin_pay;}
		}
		public string Msn
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		public decimal PointToMoney
		{
			set{ _pointtomoney=value;}
			get{return _pointtomoney;}
		}
		public string Postalcode
		{
			set{ _postalcode=value;}
			get{return _postalcode;}
		}
		public int ProductTop
		{
			set{ _producttop=value;}
			get{return _producttop;}
		}
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		public string RealName
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public string SEO_Description
		{
			set{ _seo_description=value;}
			get{return _seo_description;}
		}
		public string SEO_Keywords
		{
			set{ _seo_keywords=value;}
			get{return _seo_keywords;}
		}
		public string SEO_Title
		{
			set{ _seo_title=value;}
			get{return _seo_title;}
		}
		public string ServicePanel
		{
			set{ _servicepanel=value;}
			get{return _servicepanel;}
		}
		public string Sex
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		public string shortbar
		{
			set{ _shortbar=value;}
			get{return _shortbar;}
		}
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		public string SubName
		{
			set{ _subname=value;}
			get{return _subname;}
		}
		public int Supplier_Group_id
		{
			set{ _supplier_group_id=value;}
			get{return _supplier_group_id;}
		}
		public int Supplier_Skin_id
		{
			set{ _supplier_skin_id=value;}
			get{return _supplier_skin_id;}
		}
		public string SupplierNumber
		{
			set{ _suppliernumber=value;}
			get{return _suppliernumber;}
		}
		public DateTime Time_Begin
		{
			set{ _time_begin=value;}
			get{return _time_begin;}
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
		public int Type_id_SupplierStatus
		{
			set{ _type_id_supplierstatus=value;}
			get{return _type_id_supplierstatus;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public int UserLow
		{
			set{ _userlow=value;}
			get{return _userlow;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public int UserTop
		{
			set{ _usertop=value;}
			get{return _usertop;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public decimal Money_Service
		{
			set{ _money_service=value;}
			get{return _money_service;}
		}
		public decimal Money_Margin
		{
			set{ _money_margin=value;}
			get{return _money_margin;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

