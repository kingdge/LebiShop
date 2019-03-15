using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Version
	{
		#region Model
		private int _id=0;
		private string _content="";
		private string _description="";
		private int _isdbstructupdate=0;
		private int _isnodeupdate=0;
		private int _ispageupdate=0;
		private int _issystemmenuupdate=0;
		private int _issystempageupdate=0;
		private int _isthemepageupdate=0;
		private int _istypeupdate=0;
		private int _isupdate=0;
		private string _path="";
		private string _path_rar="";
		private string _size="";
		private DateTime _time_update=DateTime.Now;
		private int _version=0;
		private string _version_check="";
		private decimal _version_son=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int IsDBStructUpdate
		{
			set{ _isdbstructupdate=value;}
			get{return _isdbstructupdate;}
		}
		public int IsNodeUpdate
		{
			set{ _isnodeupdate=value;}
			get{return _isnodeupdate;}
		}
		public int IsPageUpdate
		{
			set{ _ispageupdate=value;}
			get{return _ispageupdate;}
		}
		public int IsSystemMenuUpdate
		{
			set{ _issystemmenuupdate=value;}
			get{return _issystemmenuupdate;}
		}
		public int IsSystemPageUpdate
		{
			set{ _issystempageupdate=value;}
			get{return _issystempageupdate;}
		}
		public int IsThemePageUpdate
		{
			set{ _isthemepageupdate=value;}
			get{return _isthemepageupdate;}
		}
		public int IsTypeUpdate
		{
			set{ _istypeupdate=value;}
			get{return _istypeupdate;}
		}
		public int IsUpdate
		{
			set{ _isupdate=value;}
			get{return _isupdate;}
		}
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		public string Path_rar
		{
			set{ _path_rar=value;}
			get{return _path_rar;}
		}
		public string Size
		{
			set{ _size=value;}
			get{return _size;}
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
		public string Version_Check
		{
			set{ _version_check=value;}
			get{return _version_check;}
		}
		public decimal Version_Son
		{
			set{ _version_son=value;}
			get{return _version_son;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

