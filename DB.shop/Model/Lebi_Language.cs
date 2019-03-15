using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Language
	{
		#region Model
		private int _id=0;
		private string _code="";
		private decimal _currency_exchangerate=0;
		private int _currency_id=0;
		private string _currency_msige="";
		private string _imageurl="";
		private int _islazyload=0;
		private string _name="";
		private string _path="";
		private int _site_id=0;
		private int _sort=0;
		private int _theme_id=0;
		private int _topareaid=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public decimal Currency_ExchangeRate
		{
			set{ _currency_exchangerate=value;}
			get{return _currency_exchangerate;}
		}
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
		}
		public string Currency_Msige
		{
			set{ _currency_msige=value;}
			get{return _currency_msige;}
		}
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		public int IsLazyLoad
		{
			set{ _islazyload=value;}
			get{return _islazyload;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		public int Site_id
		{
			set{ _site_id=value;}
			get{return _site_id;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		public int TopAreaid
		{
			set{ _topareaid=value;}
			get{return _topareaid;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

