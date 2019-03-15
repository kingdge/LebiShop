using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Type
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _class="";
		private int _sort=0;
		private string _description="";
		private string _color="";
		private Lebi_Type _model;
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
		public string Class
		{
			set{ _class=value;}
			get{return _class;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Color
		{
			set{ _color=value;}
			get{return _color;}
		}
		#endregion

	}
}

