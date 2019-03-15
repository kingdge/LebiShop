using System;
namespace Shop.Model
{
	/// <summary>
	/// 实体类Lebi_CardType 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Lebi_CardType
	{
		public Lebi_CardType()
		{}
		#region Model
		private int _id=0;
		private string _name="";
		private decimal _money=0;
		private string _pro_type_ids="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _admin_id=0;
		private int _ispayonce=0;
		private int _iscanotheruse=0;
		private string _indexcode="";
		private string _no_start="";
		private string _no_end="";
		private string _no_now="";
		private DateTime _time_begin=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _cardtype_id=0;
		private int _length=0;
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
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pro_Type_ids
		{
			set{ _pro_type_ids=value;}
			get{return _pro_type_ids;}
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
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPayOnce
		{
			set{ _ispayonce=value;}
			get{return _ispayonce;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCanOtherUse
		{
			set{ _iscanotheruse=value;}
			get{return _iscanotheruse;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IndexCode
		{
			set{ _indexcode=value;}
			get{return _indexcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NO_Start
		{
			set{ _no_start=value;}
			get{return _no_start;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NO_End
		{
			set{ _no_end=value;}
			get{return _no_end;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NO_Now
		{
			set{ _no_now=value;}
			get{return _no_now;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Begin
		{
			set{ _time_begin=value;}
			get{return _time_begin;}
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
		public int CardType_id
		{
			set{ _cardtype_id=value;}
			get{return _cardtype_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Length
		{
			set{ _length=value;}
			get{return _length;}
		}
		#endregion Model

	}
}

