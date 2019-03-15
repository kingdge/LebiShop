using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_ImageSize
	{
		#region Model
		private int _id=0;
		private int _width=0;
		private int _height=0;
		private DateTime _time_add=DateTime.Now;
		private Lebi_ImageSize _model;
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
		public int Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Height
		{
			set{ _height=value;}
			get{return _height;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		#endregion

	}
}

