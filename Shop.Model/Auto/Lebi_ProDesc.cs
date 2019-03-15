using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_ProDesc
	{
		#region Model
		private int _id=0;
		private string _description="";
		private Lebi_ProDesc _model;
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		#endregion

	}
}

