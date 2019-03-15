using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Tips
	{
		#region Model
		private int _id=0;
		private string _content="";
		private DateTime _time_update=DateTime.Now;
		private Lebi_Tips _model;
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
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		#endregion

	}
}

