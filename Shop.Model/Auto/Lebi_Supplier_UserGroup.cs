using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_UserGroup
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _isshow=0;
		private int _sort=0;
		private int _supplier_id=0;
		private string _description="";
		private string _menu_ids="";
		private string _menu_ids_index="";
		private string _limit_ids="";
		private string _limit_codes="";
		private DateTime _time_add=DateTime.Now;
		private int _user_id_add=0;
		private int _user_id_edit=0;
		private DateTime _time_edit=DateTime.Now;
		private Lebi_Supplier_UserGroup _model;
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
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
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
		/// <summary>
		/// 
		/// </summary>
		public string Limit_ids
		{
			set{ _limit_ids=value;}
			get{return _limit_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Limit_Codes
		{
			set{ _limit_codes=value;}
			get{return _limit_codes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id_Add
		{
			set{ _user_id_add=value;}
			get{return _user_id_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id_Edit
		{
			set{ _user_id_edit=value;}
			get{return _user_id_edit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Edit
		{
			set{ _time_edit=value;}
			get{return _time_edit;}
		}
		#endregion

	}
}

