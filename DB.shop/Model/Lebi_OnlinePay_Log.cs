using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_OnlinePay_Log
	{
		#region Model
		private int _id=0;
		private int _order_id=0;
		private int _onlinepay_id=0;
		private DateTime _time_add=DateTime.Now;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public int OnlinePay_id
		{
			set{ _onlinepay_id=value;}
			get{return _onlinepay_id;}
		}
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

