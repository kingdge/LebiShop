using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Cnzz
	{
		#region Model
		private int _id=0;
		private string _ccontent="";
		private int _state=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Ccontent
		{
			set{ _ccontent=value;}
			get{return _ccontent;}
		}
		public int state
		{
			set{ _state=value;}
			get{return _state;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

