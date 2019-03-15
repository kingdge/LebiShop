using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Tab
	{
		#region Model
		private int _id=0;
		private string _tname="";
		private string _title="";
		private string _tkey="";
		private string _tdes="";
		private string _tdirname="";
		private int _tsort=0;
		private int _mode=0;
		private string _url="";
		private string _description="";
		private int _position=0;
		private Lebi_Tab _model;
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
		public string Tname
		{
			set{ _tname=value;}
			get{return _tname;}
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
		public string Tkey
		{
			set{ _tkey=value;}
			get{return _tkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tdes
		{
			set{ _tdes=value;}
			get{return _tdes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tdirname
		{
			set{ _tdirname=value;}
			get{return _tdirname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Tsort
		{
			set{ _tsort=value;}
			get{return _tsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Mode
		{
			set{ _mode=value;}
			get{return _mode;}
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Position
		{
			set{ _position=value;}
			get{return _position;}
		}
		#endregion

	}
}

