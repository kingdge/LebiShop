using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier
	{
		#region Model
		private int _id=0;
		private string _username="";
		private string _password="";
		private string _email="";
		private string _realname="";
		private string _company="";
		private string _subname="";
		private string _sex="";
		private int _area_id=0;
		private string _mobilephone="";
		private string _phone="";
		private string _address="";
		private string _qq="";
		private string _fax="";
		private string _postalcode="";
		private string _msn="";
		private int _count_login=0;
		private string _ip_last="";
		private string _ip_this="";
		private DateTime _time_this=DateTime.Now;
		private DateTime _time_last=DateTime.Now;
		private DateTime _time_reg=DateTime.Now;
		private int _status=0;
		private string _language="";
		private int _supplier_group_id=0;
		private string _remark="";
		private decimal _money_margin=0;
		private decimal _money_margin_pay=0;
		private decimal _money_service=0;
		private decimal _money=0;
		private DateTime _time_begin=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _billingdays=0;
		private int _level_id=0;
		private int _type_id_supplierstatus=0;
		private int _producttop=0;
		private string _name="";
		private string _classname="";
		private string _seo_title="";
		private string _seo_description="";
		private string _seo_keywords="";
		private string _description="";
		private int _user_id=0;
		private int _issuppliertransport=0;
		private string _servicepanel="";
		private int _usertop=0;
		private int _userlow=0;
		private int _days_checkuserlow=0;
		private int _iscash=0;
		private int _supplier_skin_id=0;
		private string _head="";
		private string _shortbar="";
		private string _longbar="";
		private string _logo="";
		private string _domain="";
		private string _freezeremark="";
		private int _isspread=0;
		private string _suppliernumber="";
		private decimal _pointtomoney=0;
		private Lebi_Supplier _model;
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
		public string Company
		{
			set{ _company=value;}
			get{return _company;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SubName
		{
			set{ _subname=value;}
			get{return _subname;}
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
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
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
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
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
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
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
		public int Supplier_Group_id
		{
			set{ _supplier_group_id=value;}
			get{return _supplier_group_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Margin
		{
			set{ _money_margin=value;}
			get{return _money_margin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Margin_pay
		{
			set{ _money_margin_pay=value;}
			get{return _money_margin_pay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Service
		{
			set{ _money_service=value;}
			get{return _money_service;}
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
		public DateTime Time_Begin
		{
			set{ _time_begin=value;}
			get{return _time_begin;}
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
		public int BillingDays
		{
			set{ _billingdays=value;}
			get{return _billingdays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Level_id
		{
			set{ _level_id=value;}
			get{return _level_id;}
		}
		/// <summary>
		/// 441未审核,442已审核,443已停用,444冻结
		/// </summary>
		public int Type_id_SupplierStatus
		{
			set{ _type_id_supplierstatus=value;}
			get{return _type_id_supplierstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ProductTop
		{
			set{ _producttop=value;}
			get{return _producttop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ClassName
		{
			set{ _classname=value;}
			get{return _classname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SEO_Title
		{
			set{ _seo_title=value;}
			get{return _seo_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SEO_Description
		{
			set{ _seo_description=value;}
			get{return _seo_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SEO_Keywords
		{
			set{ _seo_keywords=value;}
			get{return _seo_keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSupplierTransport
		{
			set{ _issuppliertransport=value;}
			get{return _issuppliertransport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ServicePanel
		{
			set{ _servicepanel=value;}
			get{return _servicepanel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserTop
		{
			set{ _usertop=value;}
			get{return _usertop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserLow
		{
			set{ _userlow=value;}
			get{return _userlow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Days_checkuserlow
		{
			set{ _days_checkuserlow=value;}
			get{return _days_checkuserlow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCash
		{
			set{ _iscash=value;}
			get{return _iscash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_Skin_id
		{
			set{ _supplier_skin_id=value;}
			get{return _supplier_skin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string head
		{
			set{ _head=value;}
			get{return _head;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string shortbar
		{
			set{ _shortbar=value;}
			get{return _shortbar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string longbar
		{
			set{ _longbar=value;}
			get{return _longbar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FreezeRemark
		{
			set{ _freezeremark=value;}
			get{return _freezeremark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSpread
		{
			set{ _isspread=value;}
			get{return _isspread;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SupplierNumber
		{
			set{ _suppliernumber=value;}
			get{return _suppliernumber;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal PointToMoney
		{
			set{ _pointtomoney=value;}
			get{return _pointtomoney;}
		}
		#endregion

	}
}

