using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_weixin_menu
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _type="";
		private string _url="";
		private int _sort=0;
		private int _parentid=0;
		private int _dt_id=0;
		private Lebi_weixin_menu _model;
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
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string url
		{
			set{ _url=value;}
			get{return _url;}
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
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		#endregion

	}
}

