using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_OnlinePay_Log
	{
		#region Model
		private int _id=0;
		private int _order_id=0;
		private int _onlinepay_id=0;
		private DateTime _time_add=DateTime.Now;
		private Lebi_OnlinePay_Log _model;
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
		public int OnlinePay_id
		{
			set{ _onlinepay_id=value;}
			get{return _onlinepay_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		#endregion

	}
}

