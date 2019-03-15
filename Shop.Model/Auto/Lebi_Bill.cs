using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Bill
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private int _billtype_id=0;
		private int _type_id_billtype=0;
		private string _user_username="";
		private string _title="";
		private int _order_id=0;
		private string _order_code="";
		private string _content="";
		private decimal _money=0;
		private decimal _taxrate=0;
		private string _company_name="";
		private string _company_code="";
		private string _company_address="";
		private string _company_phone="";
		private string _company_bank="";
		private string _company_bank_user="";
		private int _type_id_billstatus=0;
		private int _currency_id=0;
		private string _currency_code="";
		private decimal _currency_exchangerate=0;
		private string _currency_msige="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_finish=DateTime.Now;
		private int _admin_id=0;
		private string _admin_username="";
		private string _billno="";
		private Lebi_Bill _model;
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
		public int BillType_id
		{
			set{ _billtype_id=value;}
			get{return _billtype_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type_id_BillType
		{
			set{ _type_id_billtype=value;}
			get{return _type_id_billtype;}
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Order_id
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
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal TaxRate
		{
			set{ _taxrate=value;}
			get{return _taxrate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Company_Name
		{
			set{ _company_name=value;}
			get{return _company_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Company_Code
		{
			set{ _company_code=value;}
			get{return _company_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Company_Address
		{
			set{ _company_address=value;}
			get{return _company_address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Company_Phone
		{
			set{ _company_phone=value;}
			get{return _company_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Company_Bank
		{
			set{ _company_bank=value;}
			get{return _company_bank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Company_Bank_User
		{
			set{ _company_bank_user=value;}
			get{return _company_bank_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Type_id_BillStatus
		{
			set{ _type_id_billstatus=value;}
			get{return _type_id_billstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Currency_Code
		{
			set{ _currency_code=value;}
			get{return _currency_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Currency_ExchangeRate
		{
			set{ _currency_exchangerate=value;}
			get{return _currency_exchangerate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Currency_Msige
		{
			set{ _currency_msige=value;}
			get{return _currency_msige;}
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
		public DateTime Time_Finish
		{
			set{ _time_finish=value;}
			get{return _time_finish;}
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
		public string BillNo
		{
			set{ _billno=value;}
			get{return _billno;}
		}
		#endregion

	}
}

