using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Bill
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private string _billno="";
		private int _billtype_id=0;
		private string _company_address="";
		private string _company_bank="";
		private string _company_bank_user="";
		private string _company_code="";
		private string _company_name="";
		private string _company_phone="";
		private string _content="";
		private string _currency_code="";
		private decimal _currency_exchangerate=0;
		private int _currency_id=0;
		private string _currency_msige="";
		private decimal _money=0;
		private string _order_code="";
		private int _order_id=0;
		private decimal _taxrate=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_finish=DateTime.Now;
		private string _title="";
		private int _type_id_billstatus=0;
		private int _type_id_billtype=0;
		private int _user_id=0;
		private string _user_username="";
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
		public string BillNo
		{
			set{ _billno=value;}
			get{return _billno;}
		}
		public int BillType_id
		{
			set{ _billtype_id=value;}
			get{return _billtype_id;}
		}
		public string Company_Address
		{
			set{ _company_address=value;}
			get{return _company_address;}
		}
		public string Company_Bank
		{
			set{ _company_bank=value;}
			get{return _company_bank;}
		}
		public string Company_Bank_User
		{
			set{ _company_bank_user=value;}
			get{return _company_bank_user;}
		}
		public string Company_Code
		{
			set{ _company_code=value;}
			get{return _company_code;}
		}
		public string Company_Name
		{
			set{ _company_name=value;}
			get{return _company_name;}
		}
		public string Company_Phone
		{
			set{ _company_phone=value;}
			get{return _company_phone;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public string Currency_Code
		{
			set{ _currency_code=value;}
			get{return _currency_code;}
		}
		public decimal Currency_ExchangeRate
		{
			set{ _currency_exchangerate=value;}
			get{return _currency_exchangerate;}
		}
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
		}
		public string Currency_Msige
		{
			set{ _currency_msige=value;}
			get{return _currency_msige;}
		}
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public decimal TaxRate
		{
			set{ _taxrate=value;}
			get{return _taxrate;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Finish
		{
			set{ _time_finish=value;}
			get{return _time_finish;}
		}
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		public int Type_id_BillStatus
		{
			set{ _type_id_billstatus=value;}
			get{return _type_id_billstatus;}
		}
		public int Type_id_BillType
		{
			set{ _type_id_billtype=value;}
			get{return _type_id_billtype;}
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

