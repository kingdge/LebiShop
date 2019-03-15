using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_AdminSkin
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _name="";
		private int _sort=0;
		private string _pagename="";
		private int _type_id_adminskintype=0;
		private Lebi_AdminSkin _model;
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
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
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
		public string PageName
		{
			set{ _pagename=value;}
			get{return _pagename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type_id_AdminSkinType
		{
			set{ _type_id_adminskintype=value;}
			get{return _type_id_adminskintype;}
		}
		#endregion

	}
}

