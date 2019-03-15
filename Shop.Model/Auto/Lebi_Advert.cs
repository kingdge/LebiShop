using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Advert
	{
		#region Model
		private int _id=0;
		private int _theme_id=0;
		private string _language_codes="";
		private string _language_ids="";
		private int _theme_advert_id=0;
		private string _theme_advert_code="";
		private string _image="";
		private int _width=0;
		private int _height=0;
		private DateTime _time_add=DateTime.Now;
		private string _url="";
		private string _tatget="";
		private int _sort=0;
		private string _title="";
		private string _imagesmall="";
		private string _description="";
		private Lebi_Advert _model;
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
		public int Theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_Codes
		{
			set{ _language_codes=value;}
			get{return _language_codes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Theme_Advert_id
		{
			set{ _theme_advert_id=value;}
			get{return _theme_advert_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Theme_Advert_Code
		{
			set{ _theme_advert_code=value;}
			get{return _theme_advert_code;}
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
		public int Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Height
		{
			set{ _height=value;}
			get{return _height;}
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
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tatget
		{
			set{ _tatget=value;}
			get{return _tatget;}
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion

	}
}

