using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Pay
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _description="";
		private decimal _feerate=0;
		private int _isused=0;
		private string _name="";
		private int _sort=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public decimal FeeRate
		{
			set{ _feerate=value;}
			get{return _feerate;}
		}
		public int IsUsed
		{
			set{ _isused=value;}
			get{return _isused;}
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
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

