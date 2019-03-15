using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_TabChild
	{
		#region Model
		private int _id=0;
		private int _num=0;
		private int _protypeid=0;
		private int _sort=0;
		private int _tabid=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int num
		{
			set{ _num=value;}
			get{return _num;}
		}
		public int protypeid
		{
			set{ _protypeid=value;}
			get{return _protypeid;}
		}
		public int sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int tabid
		{
			set{ _tabid=value;}
			get{return _tabid;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

