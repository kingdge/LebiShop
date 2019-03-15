using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Cash_alipay
	{
		#region Model
		private int _id=0;
		private string _alipay_user="";
		private string _alipay_username="";
		private string _cash_ids="";
		private string _code="";
		private string _content="";
		private int _count=0;
		private int _ispaid=0;
		private decimal _money=0;
		private string _result_no="";
		private string _result_yes="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_paid=DateTime.Now;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string alipay_user
		{
			set{ _alipay_user=value;}
			get{return _alipay_user;}
		}
		public string alipay_username
		{
			set{ _alipay_username=value;}
			get{return _alipay_username;}
		}
		public string Cash_ids
		{
			set{ _cash_ids=value;}
			get{return _cash_ids;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public int count
		{
			set{ _count=value;}
			get{return _count;}
		}
		public int IsPaid
		{
			set{ _ispaid=value;}
			get{return _ispaid;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public string result_no
		{
			set{ _result_no=value;}
			get{return _result_no;}
		}
		public string result_yes
		{
			set{ _result_yes=value;}
			get{return _result_yes;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Paid
		{
			set{ _time_paid=value;}
			get{return _time_paid;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

