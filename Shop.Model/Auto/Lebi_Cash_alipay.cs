using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Cash_alipay
	{
		#region Model
		private int _id=0;
		private string _code="";
		private string _cash_ids="";
		private DateTime _time_add=DateTime.Now;
		private string _alipay_user="";
		private string _alipay_username="";
		private decimal _money=0;
		private int _count=0;
		private string _content="";
		private int _ispaid=0;
		private DateTime _time_paid=DateTime.Now;
		private string _result_yes="";
		private string _result_no="";
		private Lebi_Cash_alipay _model;
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
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Cash_ids
		{
			set{ _cash_ids=value;}
			get{return _cash_ids;}
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
		public string alipay_user
		{
			set{ _alipay_user=value;}
			get{return _alipay_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string alipay_username
		{
			set{ _alipay_username=value;}
			get{return _alipay_username;}
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
		public int count
		{
			set{ _count=value;}
			get{return _count;}
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
		public int IsPaid
		{
			set{ _ispaid=value;}
			get{return _ispaid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Paid
		{
			set{ _time_paid=value;}
			get{return _time_paid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string result_yes
		{
			set{ _result_yes=value;}
			get{return _result_yes;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string result_no
		{
			set{ _result_no=value;}
			get{return _result_no;}
		}
		#endregion

	}
}

