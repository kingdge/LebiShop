using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Cash
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private string _user_username="";
		private string _bank="";
		private string _accountcode="";
		private string _accountname="";
		private decimal _money=0;
		private int _type_id_cashstatus=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private string _remark="";
		private string _admin_username="";
		private int _admin_id=0;
		private int _supplier_id=0;
		private string _supplier_subname="";
		private decimal _fee=0;
		private string _paytype="";
		private int _dt_id=0;
		private Lebi_Cash _model;
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
		public string Bank
		{
			set{ _bank=value;}
			get{return _bank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountCode
		{
			set{ _accountcode=value;}
			get{return _accountcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string AccountName
		{
			set{ _accountname=value;}
			get{return _accountname;}
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
		/// 401未付款,402已付款,403无效
		/// </summary>
		public int Type_id_CashStatus
		{
			set{ _type_id_cashstatus=value;}
			get{return _type_id_cashstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_update
		{
			set{ _time_update=value;}
			get{return _time_update;}
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
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_SubName
		{
			set{ _supplier_subname=value;}
			get{return _supplier_subname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Fee
		{
			set{ _fee=value;}
			get{return _fee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayType
		{
			set{ _paytype=value;}
			get{return _paytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		#endregion

	}
}

