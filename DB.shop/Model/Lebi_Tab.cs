using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Tab
	{
		#region Model
		private int _id=0;
		private string _description="";
		private int _mode=0;
		private int _position=0;
		private string _tdes="";
		private string _tdirname="";
		private string _title="";
		private string _tkey="";
		private string _tname="";
		private int _tsort=0;
		private string _url="";
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
		public int Mode
		{
			set{ _mode=value;}
			get{return _mode;}
		}
		public int Position
		{
			set{ _position=value;}
			get{return _position;}
		}
		public string Tdes
		{
			set{ _tdes=value;}
			get{return _tdes;}
		}
		public string Tdirname
		{
			set{ _tdirname=value;}
			get{return _tdirname;}
		}
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		public string Tkey
		{
			set{ _tkey=value;}
			get{return _tkey;}
		}
		public string Tname
		{
			set{ _tname=value;}
			get{return _tname;}
		}
		public int Tsort
		{
			set{ _tsort=value;}
			get{return _tsort;}
		}
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

