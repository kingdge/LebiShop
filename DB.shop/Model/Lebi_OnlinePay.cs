using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_OnlinePay
	{
		#region Model
		private int _id=0;
		private string _appid="";
		private string _appkey="";
		private string _code="";
		private string _currency_code="";
		private int _currency_id=0;
		private string _currency_name="";
		private string _description="";
		private string _email="";
		private decimal _exchangerate=0;
		private decimal _feerate=0;
		private int _isused=0;
		private string _language_ids="";
		private string _logo="";
		private string _name="";
		private int _parentid=0;
		private string _remark="";
		private string _showtype="";
		private int _sort=0;
		private int _supplier_id=0;
		private string _terminal="";
		private string _typename="";
		private string _url="";
		private string _userkey="";
		private string _username="";
		private string _userrealname="";
		private int _freefeerate=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Appid
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		public string Appkey
		{
			set{ _appkey=value;}
			get{return _appkey;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public string Currency_Code
		{
			set{ _currency_code=value;}
			get{return _currency_code;}
		}
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
		}
		public string Currency_Name
		{
			set{ _currency_name=value;}
			get{return _currency_name;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public decimal ExchangeRate
		{
			set{ _exchangerate=value;}
			get{return _exchangerate;}
		}
		public decimal FeeRate
		{
			set{ _feerate=value;}
			get{return _feerate;}
		}
		public int IsUsed
		{
			set{ _isused=value;}
			get{return _isused;}
		}
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public string showtype
		{
			set{ _showtype=value;}
			get{return _showtype;}
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
		public string terminal
		{
			set{ _terminal=value;}
			get{return _terminal;}
		}
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		public string UserKey
		{
			set{ _userkey=value;}
			get{return _userkey;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string UserRealName
		{
			set{ _userrealname=value;}
			get{return _userrealname;}
		}
		public int FreeFeeRate
		{
			set{ _freefeerate=value;}
			get{return _freefeerate;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

