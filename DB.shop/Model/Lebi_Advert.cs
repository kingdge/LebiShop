using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Advert
	{
		#region Model
		private int _id=0;
		private string _description="";
		private int _height=0;
		private string _image="";
		private string _imagesmall="";
		private string _language_codes="";
		private string _language_ids="";
		private int _sort=0;
		private string _tatget="";
		private string _theme_advert_code="";
		private int _theme_advert_id=0;
		private int _theme_id=0;
		private DateTime _time_add=DateTime.Now;
		private string _title="";
		private string _url="";
		private int _width=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int Height
		{
			set{ _height=value;}
			get{return _height;}
		}
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
		}
		public string ImageSmall
		{
			set{ _imagesmall=value;}
			get{return _imagesmall;}
		}
		public string Language_Codes
		{
			set{ _language_codes=value;}
			get{return _language_codes;}
		}
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string Tatget
		{
			set{ _tatget=value;}
			get{return _tatget;}
		}
		public string Theme_Advert_Code
		{
			set{ _theme_advert_code=value;}
			get{return _theme_advert_code;}
		}
		public int Theme_Advert_id
		{
			set{ _theme_advert_id=value;}
			get{return _theme_advert_id;}
		}
		public int Theme_id
		{
			set{ _theme_id=value;}
			get{return _theme_id;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		public string URL
		{
			set{ _url=value;}
			get{return _url;}
		}
		public int Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

