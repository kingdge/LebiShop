using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Order_ProPerty
	{
		#region Model
		private int _id=0;
		private string _order_code="";
		private int _order_id=0;
		private int _propertyid=0;
		private string _propertyname="";
		private int _propertyparentid=0;
		private string _propertyvalue="";
		private DateTime _time_add=DateTime.Now;
		private int _user_id=0;
		private string _user_username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public int ProPertyid
		{
			set{ _propertyid=value;}
			get{return _propertyid;}
		}
		public string ProPertyName
		{
			set{ _propertyname=value;}
			get{return _propertyname;}
		}
		public int ProPertyParentid
		{
			set{ _propertyparentid=value;}
			get{return _propertyparentid;}
		}
		public string ProPertyValue
		{
			set{ _propertyvalue=value;}
			get{return _propertyvalue;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

