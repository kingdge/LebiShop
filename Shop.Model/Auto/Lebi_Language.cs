using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Language
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _code="";
		private int _sort=0;
		private int _theme_id=0;
		private string _imageurl="";
		private string _currency_msige="";
		private decimal _currency_exchangerate=0;
		private int _currency_id=0;
		private string _path="";
		private int _site_id=0;
		private int _topareaid=0;
		private int _islazyload=0;
		private Lebi_Language _model;
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
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
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
		public int Theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Currency_Msige
		{
			set{ _currency_msige=value;}
			get{return _currency_msige;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Currency_ExchangeRate
		{
			set{ _currency_exchangerate=value;}
			get{return _currency_exchangerate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Site_id
		{
			set{ _site_id=value;}
			get{return _site_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int TopAreaid
		{
			set{ _topareaid=value;}
			get{return _topareaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsLazyLoad
		{
			set{ _islazyload=value;}
			get{return _islazyload;}
		}
		#endregion

	}
}

