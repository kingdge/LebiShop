using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_DT
	{
		#region Model
		private int _id=0;
		private string _address="";
		private int _area_id=0;
		private int _commissionlevel=0;
		private int _count_login=0;
		private string _domain="";
		private string _email="";
		private int _group_id=0;
		private string _ip_last="";
		private string _ip_this="";
		private string _language="";
		private string _logo="";
		private string _mobilephone="";
		private decimal _money=0;
		private string _msn="";
		private string _phone="";
		private string _postalcode="";
		private string _product_ids="";
		private string _qq="";
		private string _realname="";
		private string _remark="";
		private string _site_copyright="";
		private string _site_description="";
		private string _site_email="";
		private string _site_keywords="";
		private string _site_logoimg="";
		private string _site_name="";
		private string _site_phone="";
		private string _site_qq="";
		private string _site_title="";
		private int _status=0;
		private DateTime _time_last=DateTime.Now;
		private DateTime _time_reg=DateTime.Now;
		private DateTime _time_this=DateTime.Now;
		private int _user_id=0;
		private int _userlevel_id=0;
		private string _username="";
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
		public int CommissionLevel
		{
			set{ _commissionlevel=value;}
			get{return _commissionlevel;}
		}
		public int Count_Login
		{
			set{ _count_login=value;}
			get{return _count_login;}
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
		public int Group_id
		{
			set{ _group_id=value;}
			get{return _group_id;}
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
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public string Msn
		{
			set{ _msn=value;}
			get{return _msn;}
		}
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		public string Postalcode
		{
			set{ _postalcode=value;}
			get{return _postalcode;}
		}
		public string Product_ids
		{
			set{ _product_ids=value;}
			get{return _product_ids;}
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
		public string Site_Copyright
		{
			set{ _site_copyright=value;}
			get{return _site_copyright;}
		}
		public string Site_Description
		{
			set{ _site_description=value;}
			get{return _site_description;}
		}
		public string Site_Email
		{
			set{ _site_email=value;}
			get{return _site_email;}
		}
		public string Site_Keywords
		{
			set{ _site_keywords=value;}
			get{return _site_keywords;}
		}
		public string Site_Logoimg
		{
			set{ _site_logoimg=value;}
			get{return _site_logoimg;}
		}
		public string Site_Name
		{
			set{ _site_name=value;}
			get{return _site_name;}
		}
		public string Site_Phone
		{
			set{ _site_phone=value;}
			get{return _site_phone;}
		}
		public string Site_QQ
		{
			set{ _site_qq=value;}
			get{return _site_qq;}
		}
		public string Site_Title
		{
			set{ _site_title=value;}
			get{return _site_title;}
		}
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
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
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
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
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

