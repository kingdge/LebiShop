using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Searchkey
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private int _type=0;
		private string _url="";
		private string _language_code="";
		private int _language_id=0;
		private Lebi_Searchkey _model;
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
		public int Type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_Code
		{
			set{ _language_code=value;}
			get{return _language_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Language_id
		{
			set{ _language_id=value;}
			get{return _language_id;}
		}
		#endregion

	}
}

