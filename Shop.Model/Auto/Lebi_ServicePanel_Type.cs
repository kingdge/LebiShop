using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_ServicePanel_Type
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _url="";
		private string _code="";
		private string _text="";
		private string _face="";
		private int _isonline=0;
		private int _sort=0;
		private Lebi_ServicePanel_Type _model;
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
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
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
		public string Text
		{
			set{ _text=value;}
			get{return _text;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Face
		{
			set{ _face=value;}
			get{return _face;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsOnline
		{
			set{ _isonline=value;}
			get{return _isonline;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		#endregion

	}
}

