using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Notice
	{
		#region Model
		private int _id=0;
		private string _ntitlecn="";
		private string _ntitleen="";
		private string _ncontentcn="";
		private string _ncontenten="";
		private int _nsort=0;
		private int _typeid=0;
		private DateTime _nsubmittime=DateTime.Now;
		private Lebi_Notice _model;
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
		public string NtitleCN
		{
			set{ _ntitlecn=value;}
			get{return _ntitlecn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NtitleEN
		{
			set{ _ntitleen=value;}
			get{return _ntitleen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NcontentCN
		{
			set{ _ncontentcn=value;}
			get{return _ncontentcn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NcontentEN
		{
			set{ _ncontenten=value;}
			get{return _ncontenten;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Nsort
		{
			set{ _nsort=value;}
			get{return _nsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Typeid
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Nsubmittime
		{
			set{ _nsubmittime=value;}
			get{return _nsubmittime;}
		}
		#endregion

	}
}

