using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Admin_Group
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private string _menu_ids="";
		private string _menu_ids_index="";
		private Lebi_Admin_Group _model;
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
		/// <summary>
		/// 
		/// </summary>
		public string Menu_ids
		{
			set{ _menu_ids=value;}
			get{return _menu_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Menu_ids_index
		{
			set{ _menu_ids_index=value;}
			get{return _menu_ids_index;}
		}
		#endregion

	}
}

