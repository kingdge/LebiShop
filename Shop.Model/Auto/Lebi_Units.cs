using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Units
	{
		#region Model
		private int _id=0;
		private string _name="";
		private Lebi_Units _model;
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
		#endregion

	}
}

