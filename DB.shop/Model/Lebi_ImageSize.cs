using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_ImageSize
	{
		#region Model
		private int _id=0;
		private int _height=0;
		private DateTime _time_add=DateTime.Now;
		private int _width=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Height
		{
			set{ _height=value;}
			get{return _height;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

