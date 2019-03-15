using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_ServicePanel
	{
		#region Model
		private int _id=0;
		private int _servicepanel_group_id=0;
		private string _name="";
		private int _face=0;
		private int _sort=0;
		private string _account="";
		private int _servicepanel_type_id=0;
		private string _language="";
		private int _supplier_id=0;
		private string _language_ids="";
		private Lebi_ServicePanel _model;
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
		public int ServicePanel_Group_id
		{
			set{ _servicepanel_group_id=value;}
			get{return _servicepanel_group_id;}
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
		public int Face
		{
			set{ _face=value;}
			get{return _face;}
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
		public string Account
		{
			set{ _account=value;}
			get{return _account;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ServicePanel_Type_id
		{
			set{ _servicepanel_type_id=value;}
			get{return _servicepanel_type_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
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
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		#endregion

	}
}

