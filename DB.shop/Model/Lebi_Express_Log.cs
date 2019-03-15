using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Express_Log
	{
		#region Model
		private int _id=0;
		private string _idlist="";
		private string _number="";
		private int _status=0;
		private int _supplier_id=0;
		private DateTime _time_add=DateTime.Now;
		private int _transport_id=0;
		private string _transport_name="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string IdList
		{
			set{ _idlist=value;}
			get{return _idlist;}
		}
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
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
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		public string Transport_Name
		{
			set{ _transport_name=value;}
			get{return _transport_name;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

