using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Admin_Power
	{
		#region Model
		private int _id=0;
		private int _admin_group_id=0;
		private int _admin_limit_id=0;
		private string _admin_limit_code="";
		private string _url="";
		private Lebi_Admin_Power _model;
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
		public int Admin_Group_id
		{
			set{ _admin_group_id=value;}
			get{return _admin_group_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Admin_Limit_id
		{
			set{ _admin_limit_id=value;}
			get{return _admin_limit_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin_Limit_Code
		{
			set{ _admin_limit_code=value;}
			get{return _admin_limit_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		#endregion

	}
}

