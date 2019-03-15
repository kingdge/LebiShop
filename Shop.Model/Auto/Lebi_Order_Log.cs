using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Order_Log
	{
		#region Model
		private int _id=0;
		private int _order_id=0;
		private int _user_id=0;
		private int _admin_id=0;
		private string _admin_name="";
		private string _content="";
		private DateTime _time_add=DateTime.Now;
		private Lebi_Order_Log _model;
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
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
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
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin_Name
		{
			set{ _admin_name=value;}
			get{return _admin_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		#endregion

	}
}

