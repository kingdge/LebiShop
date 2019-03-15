using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Promotion_Type
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private string _content="";
		private int _isrefuseother=0;
		private string _name="";
		private string _remark="";
		private int _sort=0;
		private int _supplier_id=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private DateTime _time_start=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _type_id_promotionstatus=0;
		private int _type_id_promotiontype=0;
		private string _userlevel_ids="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public int IsRefuseOther
		{
			set{ _isrefuseother=value;}
			get{return _isrefuseother;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_End
		{
			set{ _time_end=value;}
			get{return _time_end;}
		}
		public DateTime Time_Start
		{
			set{ _time_start=value;}
			get{return _time_start;}
		}
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public int Type_id_PromotionStatus
		{
			set{ _type_id_promotionstatus=value;}
			get{return _type_id_promotionstatus;}
		}
		public int Type_id_PromotionType
		{
			set{ _type_id_promotiontype=value;}
			get{return _type_id_promotiontype;}
		}
		public string UserLevel_ids
		{
			set{ _userlevel_ids=value;}
			get{return _userlevel_ids;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

