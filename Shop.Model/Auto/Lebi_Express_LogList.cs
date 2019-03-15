using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Express_LogList
	{
		#region Model
		private int _id=0;
		private int _express_log_id=0;
		private int _order_id=0;
		private string _order_code="";
		private int _status=0;
		private int _supplier_id=0;
		private Lebi_Express_LogList _model;
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
		public int Express_Log_Id
		{
			set{ _express_log_id=value;}
			get{return _express_log_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Order_Id
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
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		#endregion

	}
}

