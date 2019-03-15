using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User_Address
	{
		#region Model
		private int _id=0;
		private string _address="";
		private int _area_id=0;
		private string _email="";
		private string _mobilephone="";
		private string _name="";
		private string _phone="";
		private string _postalcode="";
		private string _say="";
		private int _user_id=0;
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
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		public string Say
		{
			set{ _say=value;}
			get{return _say;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

