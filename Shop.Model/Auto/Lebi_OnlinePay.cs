using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_OnlinePay
	{
		#region Model
		private int _id=0;
		private string _typename="";
		private string _username="";
		private string _userkey="";
		private string _url="";
		private decimal _exchangerate=0;
		private int _isused=0;
		private string _description="";
		private string _language_ids="";
		private string _email="";
		private string _logo="";
		private string _code="";
		private string _name="";
		private int _sort=0;
		private int _currency_id=0;
		private string _currency_code="";
		private string _currency_name="";
		private string _showtype="";
		private int _supplier_id=0;
		private int _parentid=0;
		private decimal _feerate=0;
		private string _terminal="";
		private string _userrealname="";
		private string _remark="";
		private string _appid="";
		private string _appkey="";
		private int _freefeerate=0;
		private Lebi_OnlinePay _model;
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
		public string TypeName
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserKey
		{
			set{ _userkey=value;}
			get{return _userkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Url
		{
			set{ _url=value;}
			get{return _url;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal ExchangeRate
		{
			set{ _exchangerate=value;}
			get{return _exchangerate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsUsed
		{
			set{ _isused=value;}
			get{return _isused;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language_ids
		{
			set{ _language_ids=value;}
			get{return _language_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Logo
		{
			set{ _logo=value;}
			get{return _logo;}
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
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
		public string Currency_Name
		{
			set{ _currency_name=value;}
			get{return _currency_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string showtype
		{
			set{ _showtype=value;}
			get{return _showtype;}
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
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal FeeRate
		{
			set{ _feerate=value;}
			get{return _feerate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string terminal
		{
			set{ _terminal=value;}
			get{return _terminal;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserRealName
		{
			set{ _userrealname=value;}
			get{return _userrealname;}
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
		public string Appid
		{
			set{ _appid=value;}
			get{return _appid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Appkey
		{
			set{ _appkey=value;}
			get{return _appkey;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int FreeFeeRate
		{
			set{ _freefeerate=value;}
			get{return _freefeerate;}
		}
		#endregion

	}
}

