using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Pro_Tag
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private Lebi_Pro_Tag _model;
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
		#endregion

	}
}

