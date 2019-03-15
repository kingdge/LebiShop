using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Menu
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _url="";
		private int _sort=0;
		private int _parentid=0;
		private int _isshow=0;
		private string _image="";
		private string _code="";
		private int _issys=0;
		private string _parentcode="";
		private Lebi_Menu _model;
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
		public string URL
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
		public int Isshow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
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
		public int IsSYS
		{
			set{ _issys=value;}
			get{return _issys;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string parentCode
		{
			set{ _parentcode=value;}
			get{return _parentcode;}
		}
		#endregion

	}
}

