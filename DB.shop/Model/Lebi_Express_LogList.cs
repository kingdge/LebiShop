using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Express_LogList
	{
		#region Model
		private int _id=0;
		private int _express_log_id=0;
		private string _order_code="";
		private int _order_id=0;
		private int _status=0;
		private int _supplier_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Express_Log_Id
		{
			set{ _express_log_id=value;}
			get{return _express_log_id;}
		}
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		public int Order_Id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

