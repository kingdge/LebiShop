using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_ProPerty
	{
		#region Model
		private int _id=0;
		private string _property="";
		private int _pro_type_id=0;
		private int _supplier_id=0;
		private int _type_id_propertytype=0;
		private Lebi_Supplier_ProPerty _model;
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
		public string ProPerty
		{
			set{ _property=value;}
			get{return _property;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Pro_Type_id
		{
			set{ _pro_type_id=value;}
			get{return _pro_type_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 131商品规格,132商品属性,133文字属性,134订购表单,135订单调查,136标签
		/// </summary>
		public int Type_id_ProPertyType
		{
			set{ _type_id_propertytype=value;}
			get{return _type_id_propertytype;}
		}
		#endregion

	}
}

