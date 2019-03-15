using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Express
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _width="";
		private string _height="";
		private int _status=0;
		private int _sort=0;
		private string _background="";
		private int _transport_id=0;
		private string _pos="";
		private Lebi_Express _model;
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
		public string Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Height
		{
			set{ _height=value;}
			get{return _height;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
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
		public string Background
		{
			set{ _background=value;}
			get{return _background;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pos
		{
			set{ _pos=value;}
			get{return _pos;}
		}
		#endregion

	}
}

