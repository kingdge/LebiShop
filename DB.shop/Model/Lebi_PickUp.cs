using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_PickUp
	{
		#region Model
		private int _id=0;
		private string _address="";
		private int _begindays=0;
		private string _description="";
		private int _iscanweekend=0;
		private string _language_ids="";
		private string _name="";
		private string _noservicedays="";
		private int _sort=0;
		private int _supplier_id=0;
		private DateTime _time_add=DateTime.Now;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		public int BeginDays
		{
			set{ _begindays=value;}
			get{return _begindays;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int IsCanWeekend
		{
			set{ _iscanweekend=value;}
			get{return _iscanweekend;}
		}
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string NoServiceDays
		{
			set{ _noservicedays=value;}
			get{return _noservicedays;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
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

