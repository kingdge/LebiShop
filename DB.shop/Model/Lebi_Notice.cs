using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Notice
	{
		#region Model
		private int _id=0;
		private string _ncontentcn="";
		private string _ncontenten="";
		private int _nsort=0;
		private DateTime _nsubmittime=DateTime.Now;
		private string _ntitlecn="";
		private string _ntitleen="";
		private int _typeid=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string NcontentCN
		{
			set{ _ncontentcn=value;}
			get{return _ncontentcn;}
		}
		public string NcontentEN
		{
			set{ _ncontenten=value;}
			get{return _ncontenten;}
		}
		public int Nsort
		{
			set{ _nsort=value;}
			get{return _nsort;}
		}
		public DateTime Nsubmittime
		{
			set{ _nsubmittime=value;}
			get{return _nsubmittime;}
		}
		public string NtitleCN
		{
			set{ _ntitlecn=value;}
			get{return _ntitlecn;}
		}
		public string NtitleEN
		{
			set{ _ntitleen=value;}
			get{return _ntitleen;}
		}
		public int Typeid
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

