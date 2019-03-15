using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_AdminSkin
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _name="";
		private string _pagename="";
		private int _sort=0;
		private int _type_id_adminskintype=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string PageName
		{
			set{ _pagename=value;}
			get{return _pagename;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Type_id_AdminSkinType
		{
			set{ _type_id_adminskintype=value;}
			get{return _type_id_adminskintype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

