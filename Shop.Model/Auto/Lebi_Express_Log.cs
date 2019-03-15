using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Express_Log
	{
		#region Model
		private int _id=0;
		private string _number="";
		private DateTime _time_add=DateTime.Now;
		private int _status=0;
		private string _idlist="";
		private int _transport_id=0;
		private string _transport_name="";
		private int _supplier_id=0;
		private Lebi_Express_Log _model;
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
		public string Number
		{
			set{ _number=value;}
			get{return _number;}
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
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IdList
		{
			set{ _idlist=value;}
			get{return _idlist;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transport_Name
		{
			set{ _transport_name=value;}
			get{return _transport_name;}
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

