using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Version
	{
		#region Model
		private int _id=0;
		private int _version=0;
		private decimal _version_son=0;
		private string _path_rar="";
		private string _path="";
		private DateTime _time_update=DateTime.Now;
		private int _isupdate=0;
		private string _content="";
		private string _version_check="";
		private string _description="";
		private string _size="";
		private int _istypeupdate=0;
		private int _isdbstructupdate=0;
		private int _issystemmenuupdate=0;
		private int _isthemepageupdate=0;
		private int _isnodeupdate=0;
		private int _ispageupdate=0;
		private int _issystempageupdate=0;
		private Lebi_Version _model;
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
		public int Version
		{
			set{ _version=value;}
			get{return _version;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Version_Son
		{
			set{ _version_son=value;}
			get{return _version_son;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Path_rar
		{
			set{ _path_rar=value;}
			get{return _path_rar;}
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
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsUpdate
		{
			set{ _isupdate=value;}
			get{return _isupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Version_Check
		{
			set{ _version_check=value;}
			get{return _version_check;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Size
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsTypeUpdate
		{
			set{ _istypeupdate=value;}
			get{return _istypeupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsDBStructUpdate
		{
			set{ _isdbstructupdate=value;}
			get{return _isdbstructupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSystemMenuUpdate
		{
			set{ _issystemmenuupdate=value;}
			get{return _issystemmenuupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsThemePageUpdate
		{
			set{ _isthemepageupdate=value;}
			get{return _isthemepageupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsNodeUpdate
		{
			set{ _isnodeupdate=value;}
			get{return _isnodeupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPageUpdate
		{
			set{ _ispageupdate=value;}
			get{return _ispageupdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSystemPageUpdate
		{
			set{ _issystempageupdate=value;}
			get{return _issystempageupdate;}
		}
		#endregion

	}
}

