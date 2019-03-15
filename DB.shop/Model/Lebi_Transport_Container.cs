using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Transport_Container
	{
		#region Model
		private int _id=0;
		private string _description="";
		private string _name="";
		private int _sort=0;
		private decimal _volume=0;
		private decimal _weight=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public decimal Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		public decimal Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

