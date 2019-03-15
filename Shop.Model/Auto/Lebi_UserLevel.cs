using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_UserLevel
	{
		#region Model
		private int _id=0;
		private int _grade=0;
		private string _name="";
		private string _pricename="";
		private string _type_id_pricetype="";
		private int _lpoint=0;
		private decimal _price=0;
		private string _remark="";
		private int _lisdefault=0;
		private string _imageurl="";
		private decimal _ordersubmit=0;
		private int _loginpointcut=0;
		private int _loginpointadd=0;
		private int _comment=0;
		private int _buyright=0;
		private decimal _moneytopoint=0;
		private string _pointtomoney="";
		private int _ishideprice=0;
		private int _ordersubmitcount=0;
		private int _registertype=0;
		private int _isusedagent=0;
		private Lebi_UserLevel _model;
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
		public int Grade
		{
			set{ _grade=value;}
			get{return _grade;}
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
		public string PriceName
		{
			set{ _pricename=value;}
			get{return _pricename;}
		}
		/// <summary>
		/// 110游客价,111普通会员价,112高级会员价,113钻石会员价
		/// </summary>
		public string Type_id_PriceType
		{
			set{ _type_id_pricetype=value;}
			get{return _type_id_pricetype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Lpoint
		{
			set{ _lpoint=value;}
			get{return _lpoint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int LisDefault
		{
			set{ _lisdefault=value;}
			get{return _lisdefault;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal OrderSubmit
		{
			set{ _ordersubmit=value;}
			get{return _ordersubmit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int LoginPointCut
		{
			set{ _loginpointcut=value;}
			get{return _loginpointcut;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int LoginPointAdd
		{
			set{ _loginpointadd=value;}
			get{return _loginpointadd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Comment
		{
			set{ _comment=value;}
			get{return _comment;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BuyRight
		{
			set{ _buyright=value;}
			get{return _buyright;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal MoneyToPoint
		{
			set{ _moneytopoint=value;}
			get{return _moneytopoint;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PointToMoney
		{
			set{ _pointtomoney=value;}
			get{return _pointtomoney;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsHidePrice
		{
			set{ _ishideprice=value;}
			get{return _ishideprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OrderSubmitCount
		{
			set{ _ordersubmitcount=value;}
			get{return _ordersubmitcount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int RegisterType
		{
			set{ _registertype=value;}
			get{return _registertype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsUsedAgent
		{
			set{ _isusedagent=value;}
			get{return _isusedagent;}
		}
		#endregion

	}
}

