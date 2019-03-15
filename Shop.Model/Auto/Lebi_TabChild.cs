using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_TabChild
	{
		#region Model
		private int _id=0;
		private int _tabid=0;
		private int _protypeid=0;
		private int _sort=0;
		private int _num=0;
		private Lebi_TabChild _model;
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
		public int tabid
		{
			set{ _tabid=value;}
			get{return _tabid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int protypeid
		{
			set{ _protypeid=value;}
			get{return _protypeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int num
		{
			set{ _num=value;}
			get{return _num;}
		}
		#endregion

	}
}

