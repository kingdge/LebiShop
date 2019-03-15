using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User_Bank
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _name="";
		private string _paytype="";
		private int _user_id=0;
		private string _username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string PayType
		{
			set{ _paytype=value;}
			get{return _paytype;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
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

