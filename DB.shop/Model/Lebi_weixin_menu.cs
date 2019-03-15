using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_weixin_menu
	{
		#region Model
		private int _id=0;
		private int _dt_id=0;
		private string _name="";
		private int _parentid=0;
		private int _sort=0;
		private string _type="";
		private string _url="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		public string url
		{
			set{ _url=value;}
			get{return _url;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

