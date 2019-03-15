using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_CardOrder
	{
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
		private int _no_start=0;
		private int _no_end=0;
		private int _no_now=0;
		private DateTime _time_begin=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _length=0;
		private int _type_id_cardtype=0;
		private decimal _money_buy=0;
		private Lebi_CardOrder _model;
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
		public int NO_Start
		{
			set{ _no_start=value;}
			get{return _no_start;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int NO_End
		{
			set{ _no_end=value;}
			get{return _no_end;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int NO_Now
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
		public int Length
		{
			set{ _length=value;}
			get{return _length;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type_id_CardType
		{
			set{ _type_id_cardtype=value;}
			get{return _type_id_cardtype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Buy
		{
			set{ _money_buy=value;}
			get{return _money_buy;}
		}
		#endregion

	}
}

