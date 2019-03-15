using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Language_Code
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _code="";
		private Lebi_Language_Code _model;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		#endregion

	}
}

