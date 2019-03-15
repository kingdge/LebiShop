using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Cnzz
	{
		#region Model
		private int _id=0;
		private int _state=0;
		private string _ccontent="";
		private Lebi_Cnzz _model;
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
		public int state
		{
			set{ _state=value;}
			get{return _state;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ccontent
		{
			set{ _ccontent=value;}
			get{return _ccontent;}
		}
		#endregion

	}
}

