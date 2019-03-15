using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_ProPerty
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _parentid=0;
		private int _sort=0;
		private int _type_id_propertytype=0;
		private string _imageurl="";
		private string _code="";
		private int _parentsort=0;
		private string _taobaoid="";
		private decimal _price=0;
		private string _tag="";
		private string _remark="";
		private int _supplier_id=0;
		private Lebi_ProPerty _model;
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
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
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
		/// 131商品规格,132商品属性,133文字属性,134订购表单,135订单调查,136标签
		/// </summary>
		public int Type_id_ProPertyType
		{
			set{ _type_id_propertytype=value;}
			get{return _type_id_propertytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
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
		public int parentSort
		{
			set{ _parentsort=value;}
			get{return _parentsort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string taobaoid
		{
			set{ _taobaoid=value;}
			get{return _taobaoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Tag
		{
			set{ _tag=value;}
			get{return _tag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		#endregion

	}
}

