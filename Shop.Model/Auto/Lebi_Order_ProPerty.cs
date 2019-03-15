using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Order_ProPerty
	{
		#region Model
		private int _id=0;
		private int _propertyid=0;
		private string _propertyname="";
		private string _propertyvalue="";
		private int _propertyparentid=0;
		private int _order_id=0;
		private string _order_code="";
		private int _user_id=0;
		private DateTime _time_add=DateTime.Now;
		private string _user_username="";
		private Lebi_Order_ProPerty _model;
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
		public int ProPertyid
		{
			set{ _propertyid=value;}
			get{return _propertyid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProPertyName
		{
			set{ _propertyname=value;}
			get{return _propertyname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProPertyValue
		{
			set{ _propertyvalue=value;}
			get{return _propertyvalue;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ProPertyParentid
		{
			set{ _propertyparentid=value;}
			get{return _propertyparentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
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
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
		}
		#endregion

	}
}

