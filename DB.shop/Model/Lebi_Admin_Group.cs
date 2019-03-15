using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Admin_Group
	{
		#region Model
		private int _id=0;
		private string _menu_ids="";
		private string _menu_ids_index="";
		private string _name="";
		private int _sort=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Menu_ids
		{
			set{ _menu_ids=value;}
			get{return _menu_ids;}
		}
		public string Menu_ids_index
		{
			set{ _menu_ids_index=value;}
			get{return _menu_ids_index;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

