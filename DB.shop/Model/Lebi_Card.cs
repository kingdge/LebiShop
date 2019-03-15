using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Card
	{
		#region Model
		private int _id=0;
		private int _cardorder_id=0;
		private string _code="";
		private string _indexcode="";
		private int _iscanotheruse=0;
		private int _ispayonce=0;
		private decimal _money=0;
		private decimal _money_buy=0;
		private decimal _money_last=0;
		private decimal _money_used=0;
		private int _number=0;
		private string _order_code="";
		private string _order_id="";
		private string _password="";
		private string _pro_type_ids="";
		private string _remark="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_begin=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _type_id_cardstatus=0;
		private int _type_id_cardtype=0;
		private int _user_id=0;
		private string _user_username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int CardOrder_id
		{
			set{ _cardorder_id=value;}
			get{return _cardorder_id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string IndexCode
		{
			set{ _indexcode=value;}
			get{return _indexcode;}
		}
		public int IsCanOtherUse
		{
			set{ _iscanotheruse=value;}
			get{return _iscanotheruse;}
		}
		public int IsPayOnce
		{
			set{ _ispayonce=value;}
			get{return _ispayonce;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public decimal Money_Buy
		{
			set{ _money_buy=value;}
			get{return _money_buy;}
		}
		public decimal Money_Last
		{
			set{ _money_last=value;}
			get{return _money_last;}
		}
		public decimal Money_Used
		{
			set{ _money_used=value;}
			get{return _money_used;}
		}
		public int number
		{
			set{ _number=value;}
			get{return _number;}
		}
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		public string Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		public string Pro_Type_ids
		{
			set{ _pro_type_ids=value;}
			get{return _pro_type_ids;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Begin
		{
			set{ _time_begin=value;}
			get{return _time_begin;}
		}
		public DateTime Time_End
		{
			set{ _time_end=value;}
			get{return _time_end;}
		}
		public int Type_id_CardStatus
		{
			set{ _type_id_cardstatus=value;}
			get{return _type_id_cardstatus;}
		}
		public int Type_id_CardType
		{
			set{ _type_id_cardtype=value;}
			get{return _type_id_cardtype;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

