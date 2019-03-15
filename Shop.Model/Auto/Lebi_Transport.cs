using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Transport
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _description="";
		private string _code="";
		private int _iscanofflinepay=0;
		private int _sort=0;
		private int _type_id_transporttype=0;
		private Lebi_Transport _model;
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCanofflinePay
		{
			set{ _iscanofflinepay=value;}
			get{return _iscanofflinepay;}
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
		public int Type_id_TransportType
		{
			set{ _type_id_transporttype=value;}
			get{return _type_id_transporttype;}
		}
		#endregion

	}
}

