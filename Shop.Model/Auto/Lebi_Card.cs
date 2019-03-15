using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Card
	{
		#region Model
		private int _id=0;
		private int _type_id_cardtype=0;
		private decimal _money=0;
		private decimal _money_last=0;
		private decimal _money_used=0;
		private string _pro_type_ids="";
		private string _code="";
		private int _number=0;
		private string _password="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_begin=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _user_id=0;
		private string _user_username="";
		private int _ispayonce=0;
		private int _iscanotheruse=0;
		private int _type_id_cardstatus=0;
		private string _remark="";
		private int _cardorder_id=0;
		private decimal _money_buy=0;
		private string _indexcode="";
		private string _order_id="";
		private string _order_code="";
		private Lebi_Card _model;
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
		public int Type_id_CardType
		{
			set{ _type_id_cardtype=value;}
			get{return _type_id_cardtype;}
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
		public decimal Money_Last
		{
			set{ _money_last=value;}
			get{return _money_last;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Used
		{
			set{ _money_used=value;}
			get{return _money_used;}
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
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int number
		{
			set{ _number=value;}
			get{return _number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
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
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
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
		public int Type_id_CardStatus
		{
			set{ _type_id_cardstatus=value;}
			get{return _type_id_cardstatus;}
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
		public int CardOrder_id
		{
			set{ _cardorder_id=value;}
			get{return _cardorder_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Buy
		{
			set{ _money_buy=value;}
			get{return _money_buy;}
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
		public string Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		#endregion

	}
}

