using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_DT
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private string _username="";
		private string _email="";
		private string _realname="";
		private string _logo="";
		private string _mobilephone="";
		private string _phone="";
		private int _area_id=0;
		private string _address="";
		private string _qq="";
		private string _postalcode="";
		private string _msn="";
		private int _count_login=0;
		private string _ip_last="";
		private string _ip_this="";
		private DateTime _time_this=DateTime.Now;
		private DateTime _time_last=DateTime.Now;
		private DateTime _time_reg=DateTime.Now;
		private int _group_id=0;
		private int _status=0;
		private string _language="";
		private string _remark="";
		private decimal _money=0;
		private int _userlevel_id=0;
		private string _site_name="";
		private string _site_title="";
		private string _site_keywords="";
		private string _site_description="";
		private string _site_qq="";
		private string _site_phone="";
		private string _site_email="";
		private string _site_copyright="";
		private string _site_logoimg="";
		private string _product_ids="";
		private string _domain="";
		private int _commissionlevel=0;
		private Lebi_DT _model;
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
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
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
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
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
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
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
		public int Group_id
		{
			set{ _group_id=value;}
			get{return _group_id;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
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
		public int UserLevel_id
		{
			set{ _userlevel_id=value;}
			get{return _userlevel_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Name
		{
			set{ _site_name=value;}
			get{return _site_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Title
		{
			set{ _site_title=value;}
			get{return _site_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Keywords
		{
			set{ _site_keywords=value;}
			get{return _site_keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Description
		{
			set{ _site_description=value;}
			get{return _site_description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_QQ
		{
			set{ _site_qq=value;}
			get{return _site_qq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Phone
		{
			set{ _site_phone=value;}
			get{return _site_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Email
		{
			set{ _site_email=value;}
			get{return _site_email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Copyright
		{
			set{ _site_copyright=value;}
			get{return _site_copyright;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Site_Logoimg
		{
			set{ _site_logoimg=value;}
			get{return _site_logoimg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Product_ids
		{
			set{ _product_ids=value;}
			get{return _product_ids;}
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
		public int CommissionLevel
		{
			set{ _commissionlevel=value;}
			get{return _commissionlevel;}
		}
		#endregion

	}
}

