using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Pay
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private string _description="";
		private string _code="";
		private int _isused=0;
		private decimal _feerate=0;
		private Lebi_Pay _model;
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
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsUsed
		{
			set{ _isused=value;}
			get{return _isused;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal FeeRate
		{
			set{ _feerate=value;}
			get{return _feerate;}
		}
		#endregion

	}
}

