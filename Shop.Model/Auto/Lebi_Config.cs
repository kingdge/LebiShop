using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Config
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _value="";
		private Lebi_Config _model;
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
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		#endregion

	}
}

