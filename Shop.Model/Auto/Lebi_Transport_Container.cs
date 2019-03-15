using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Transport_Container
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _description="";
		private decimal _volume=0;
		private decimal _weight=0;
		private int _sort=0;
		private Lebi_Transport_Container _model;
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
		public decimal Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion

	}
}

