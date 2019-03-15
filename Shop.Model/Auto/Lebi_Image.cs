using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Image
	{
		#region Model
		private int _id=0;
		private string _image="";
		private string _tablename="";
		private int _keyid=0;
		private string _size="";
		private DateTime _time_update=DateTime.Now;
		private Lebi_Image _model;
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
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TableName
		{
			set{ _tablename=value;}
			get{return _tablename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Keyid
		{
			set{ _keyid=value;}
			get{return _keyid;}
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
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		#endregion

	}
}

