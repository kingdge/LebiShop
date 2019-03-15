using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_UserLevel
	{
		#region Model
		private int _id=0;
		private int _buyright=0;
		private int _comment=0;
		private int _grade=0;
		private string _imageurl="";
		private int _ishideprice=0;
		private int _isusedagent=0;
		private int _lisdefault=0;
		private int _loginpointadd=0;
		private int _loginpointcut=0;
		private int _lpoint=0;
		private decimal _moneytopoint=0;
		private string _name="";
		private decimal _ordersubmit=0;
		private int _ordersubmitcount=0;
		private string _pointtomoney="";
		private decimal _price=0;
		private string _pricename="";
		private int _registertype=0;
		private string _remark="";
		private string _type_id_pricetype="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int BuyRight
		{
			set{ _buyright=value;}
			get{return _buyright;}
		}
		public int Comment
		{
			set{ _comment=value;}
			get{return _comment;}
		}
		public int Grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		public string ImageUrl
		{
			set{ _imageurl=value;}
			get{return _imageurl;}
		}
		public int IsHidePrice
		{
			set{ _ishideprice=value;}
			get{return _ishideprice;}
		}
		public int IsUsedAgent
		{
			set{ _isusedagent=value;}
			get{return _isusedagent;}
		}
		public int LisDefault
		{
			set{ _lisdefault=value;}
			get{return _lisdefault;}
		}
		public int LoginPointAdd
		{
			set{ _loginpointadd=value;}
			get{return _loginpointadd;}
		}
		public int LoginPointCut
		{
			set{ _loginpointcut=value;}
			get{return _loginpointcut;}
		}
		public int Lpoint
		{
			set{ _lpoint=value;}
			get{return _lpoint;}
		}
		public decimal MoneyToPoint
		{
			set{ _moneytopoint=value;}
			get{return _moneytopoint;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public decimal OrderSubmit
		{
			set{ _ordersubmit=value;}
			get{return _ordersubmit;}
		}
		public int OrderSubmitCount
		{
			set{ _ordersubmitcount=value;}
			get{return _ordersubmitcount;}
		}
		public string PointToMoney
		{
			set{ _pointtomoney=value;}
			get{return _pointtomoney;}
		}
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
		}
		public string PriceName
		{
			set{ _pricename=value;}
			get{return _pricename;}
		}
		public int RegisterType
		{
			set{ _registertype=value;}
			get{return _registertype;}
		}
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public string Type_id_PriceType
		{
			set{ _type_id_pricetype=value;}
			get{return _type_id_pricetype;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

