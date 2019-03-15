using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_PickUp
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _description="";
		private int _iscanweekend=0;
		private string _noservicedays="";
		private int _sort=0;
		private DateTime _time_add=DateTime.Now;
		private string _address="";
		private string _language_ids="";
		private int _begindays=0;
		private int _supplier_id=0;
		private Lebi_PickUp _model;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCanWeekend
		{
			set{ _iscanweekend=value;}
			get{return _iscanweekend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NoServiceDays
		{
			set{ _noservicedays=value;}
			get{return _noservicedays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BeginDays
		{
			set{ _begindays=value;}
			get{return _begindays;}
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

