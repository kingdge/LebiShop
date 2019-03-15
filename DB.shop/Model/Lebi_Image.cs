using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Image
	{
		#region Model
		private int _id=0;
		private string _image="";
		private int _keyid=0;
		private string _size="";
		private string _tablename="";
		private DateTime _time_update=DateTime.Now;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
		}
		public int Keyid
		{
			set{ _keyid=value;}
			get{return _keyid;}
		}
		public string Size
		{
			set{ _size=value;}
			get{return _size;}
		}
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
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

