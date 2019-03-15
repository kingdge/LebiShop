using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Tips
	{
		#region Model
		private int _id=0;
		private string _content="";
		private DateTime _time_update=DateTime.Now;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

