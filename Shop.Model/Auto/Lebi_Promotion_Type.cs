using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Promotion_Type
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _admin_id=0;
		private string _admin_username="";
		private string _remark="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private DateTime _time_start=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _sort=0;
		private int _isrefuseother=0;
		private int _type_id_promotionstatus=0;
		private string _userlevel_ids="";
		private string _content="";
		private int _supplier_id=0;
		private int _type_id_promotiontype=0;
		private Lebi_Promotion_Type _model;
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
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
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
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Start
		{
			set{ _time_start=value;}
			get{return _time_start;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_End
		{
			set{ _time_end=value;}
			get{return _time_end;}
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
		/// 是否排斥其他
		/// </summary>
		public int IsRefuseOther
		{
			set{ _isrefuseother=value;}
			get{return _isrefuseother;}
		}
		/// <summary>
		/// 240筹备中241进行中242已过期243停止
		/// </summary>
		public int Type_id_PromotionStatus
		{
			set{ _type_id_promotionstatus=value;}
			get{return _type_id_promotionstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserLevel_ids
		{
			set{ _userlevel_ids=value;}
			get{return _userlevel_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
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
		/// 410全场411自营412店铺
		/// </summary>
		public int Type_id_PromotionType
		{
			set{ _type_id_promotiontype=value;}
			get{return _type_id_promotiontype;}
		}
		#endregion

	}
}

