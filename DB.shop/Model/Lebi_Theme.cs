using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Theme
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _description="";
		private int _imagebig_height=0;
		private int _imagebig_width=0;
		private int _imagemedium_height=0;
		private int _imagemedium_width=0;
		private int _imagesmall_height=0;
		private int _imagesmall_width=0;
		private string _imagesmallurl="";
		private string _imageurl="";
		private int _isnew=0;
		private int _isupdate=0;
		private string _language="";
		private string _lebiuser="";
		private int _lebiuser_id=0;
		private string _name="";
		private string _path_advert="";
		private string _path_create="";
		private string _path_css="";
		private string _path_files="";
		private string _path_image="";
		private string _path_js="";
		private int _sort=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _version=0;
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int ImageBig_Height
		{
			set{ _imagebig_height=value;}
			get{return _imagebig_height;}
		}
		public int ImageBig_Width
		{
			set{ _imagebig_width=value;}
			get{return _imagebig_width;}
		}
		public int ImageMedium_Height
		{
			set{ _imagemedium_height=value;}
			get{return _imagemedium_height;}
		}
		public int ImageMedium_Width
		{
			set{ _imagemedium_width=value;}
			get{return _imagemedium_width;}
		}
		public int ImageSmall_Height
		{
			set{ _imagesmall_height=value;}
			get{return _imagesmall_height;}
		}
		public int ImageSmall_Width
		{
			set{ _imagesmall_width=value;}
			get{return _imagesmall_width;}
		}
		public string ImageSmallUrl
		{
			set{ _imagesmallurl=value;}
			get{return _imagesmallurl;}
		}
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		public int IsNew
		{
			set{ _isnew=value;}
			get{return _isnew;}
		}
		public int IsUpdate
		{
			set{ _isupdate=value;}
			get{return _isupdate;}
		}
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public string LebiUser
		{
			set{ _lebiuser=value;}
			get{return _lebiuser;}
		}
		public int LebiUser_id
		{
			set{ _lebiuser_id=value;}
			get{return _lebiuser_id;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Path_Advert
		{
			set{ _path_advert=value;}
			get{return _path_advert;}
		}
		public string Path_Create
		{
			set{ _path_create=value;}
			get{return _path_create;}
		}
		public string Path_CSS
		{
			set{ _path_css=value;}
			get{return _path_css;}
		}
		public string Path_Files
		{
			set{ _path_files=value;}
			get{return _path_files;}
		}
		public string Path_Image
		{
			set{ _path_image=value;}
			get{return _path_image;}
		}
		public string Path_JS
		{
			set{ _path_js=value;}
			get{return _path_js;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public int Version
		{
			set{ _version=value;}
			get{return _version;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

