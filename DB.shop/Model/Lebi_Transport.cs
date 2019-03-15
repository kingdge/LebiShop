using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Transport
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _description="";
		private int _iscanofflinepay=0;
		private string _name="";
		private int _sort=0;
		private int _type_id_transporttype=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public int IsCanofflinePay
		{
			set{ _iscanofflinepay=value;}
			get{return _iscanofflinepay;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Type_id_TransportType
		{
			set{ _type_id_transporttype=value;}
			get{return _type_id_transporttype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

