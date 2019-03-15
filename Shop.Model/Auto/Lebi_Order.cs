using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Order
	{
		#region Model
		private int _id=0;
		private string _code="";
		private int _user_id=0;
		private string _user_username="";
		private string _t_name="";
		private int _t_area_id=0;
		private string _t_address="";
		private string _t_phone="";
		private string _t_mobilephone="";
		private string _t_postalcode="";
		private string _t_email="";
		private string _remark_user="";
		private int _pay_id=0;
		private string _pay="";
		private int _onlinepay_id=0;
		private string _onlinepay="";
		private string _onlinepay_code="";
		private decimal _money_order=0;
		private decimal _money_pay=0;
		private decimal _money_product=0;
		private decimal _money_transport=0;
		private decimal _money_transport_cut=0;
		private decimal _money_bill=0;
		private decimal _money_market=0;
		private decimal _money_give=0;
		private decimal _money_cut=0;
		private decimal _money_usercut=0;
		private decimal _money_cost=0;
		private decimal _money_usecard311=0;
		private decimal _money_usecard312=0;
		private string _usecardcode311="";
		private string _usecardcode312="";
		private decimal _weight=0;
		private decimal _volume=0;
		private decimal _point=0;
		private decimal _point_product=0;
		private decimal _point_free=0;
		private string _transport_name="";
		private int _transport_id=0;
		private int _transport_price_id=0;
		private string _transport_code="";
		private string _transport_mark="";
		private decimal _editmoney_order=0;
		private decimal _editmoney_transport=0;
		private decimal _editmoney_discount=0;
		private int _isverified=0;
		private int _ispaid=0;
		private int _isshipped=0;
		private int _isshipped_all=0;
		private int _isreceived=0;
		private int _isreceived_all=0;
		private int _iscompleted=0;
		private int _isinvalid=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_verified=DateTime.Now;
		private DateTime _time_paid=DateTime.Now;
		private DateTime _time_shipped=DateTime.Now;
		private DateTime _time_received=DateTime.Now;
		private DateTime _time_completed=DateTime.Now;
		private string _remark_admin="";
		private string _billtype_name="";
		private int _billtype_id=0;
		private decimal _billtype_taxrate=0;
		private int _type_id_ordertype=0;
		private int _order_id=0;
		private int _isprintexpress=0;
		private string _promotion_type_ids="";
		private int _mark=0;
		private int _currency_id=0;
		private string _currency_code="";
		private decimal _currency_exchangerate=0;
		private string _currency_msige="";
		private int _flag=0;
		private decimal _money_fromorder=0;
		private int _iscreatecash=0;
		private int _iscreateneworder=0;
		private DateTime _time_createcash=DateTime.Now;
		private DateTime _time_createneworder=DateTime.Now;
		private int _site_id=0;
		private decimal _point_buy=0;
		private string _blno="";
		private string _containerno="";
		private string _sealno="";
		private int _supplier_id=0;
		private decimal _money_property=0;
		private string _weixin_prepay_id="";
		private int _issuppliercash=0;
		private decimal _money_onlinepayfee=0;
		private int _site_id_pay=0;
		private int _pickup_id=0;
		private string _pickup_name="";
		private DateTime _pickup_date=DateTime.Now;
		private decimal _refund_vat=0;
		private decimal _refund_fee=0;
		private int _language_id=0;
		private int _supplier_delivery_id=0;
		private int _isrefund=0;
		private DateTime _time_refund=DateTime.Now;
		private string _promotion_type_name="";
		private string _user_nickname="";
		private decimal _money_paid=0;
		private int _isreserve=0;
		private decimal _money_fanxianpay=0;
		private decimal _money_tax=0;
		private int _dt_id=0;
		private decimal _dt_money=0;
		private int _isdel=0;
		private string _keycode="";
		private string _payno="";
		private Lebi_Order _model;
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
		public string T_Name
		{
			set{ _t_name=value;}
			get{return _t_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int T_Area_id
		{
			set{ _t_area_id=value;}
			get{return _t_area_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Address
		{
			set{ _t_address=value;}
			get{return _t_address;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Phone
		{
			set{ _t_phone=value;}
			get{return _t_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_MobilePhone
		{
			set{ _t_mobilephone=value;}
			get{return _t_mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Postalcode
		{
			set{ _t_postalcode=value;}
			get{return _t_postalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string T_Email
		{
			set{ _t_email=value;}
			get{return _t_email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark_User
		{
			set{ _remark_user=value;}
			get{return _remark_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Pay_id
		{
			set{ _pay_id=value;}
			get{return _pay_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Pay
		{
			set{ _pay=value;}
			get{return _pay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int OnlinePay_id
		{
			set{ _onlinepay_id=value;}
			get{return _onlinepay_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OnlinePay
		{
			set{ _onlinepay=value;}
			get{return _onlinepay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OnlinePay_Code
		{
			set{ _onlinepay_code=value;}
			get{return _onlinepay_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Order
		{
			set{ _money_order=value;}
			get{return _money_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Pay
		{
			set{ _money_pay=value;}
			get{return _money_pay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Product
		{
			set{ _money_product=value;}
			get{return _money_product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Transport
		{
			set{ _money_transport=value;}
			get{return _money_transport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Transport_Cut
		{
			set{ _money_transport_cut=value;}
			get{return _money_transport_cut;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Bill
		{
			set{ _money_bill=value;}
			get{return _money_bill;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Market
		{
			set{ _money_market=value;}
			get{return _money_market;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Give
		{
			set{ _money_give=value;}
			get{return _money_give;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Cut
		{
			set{ _money_cut=value;}
			get{return _money_cut;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_UserCut
		{
			set{ _money_usercut=value;}
			get{return _money_usercut;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Cost
		{
			set{ _money_cost=value;}
			get{return _money_cost;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_UseCard311
		{
			set{ _money_usecard311=value;}
			get{return _money_usecard311;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_UseCard312
		{
			set{ _money_usecard312=value;}
			get{return _money_usecard312;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseCardCode311
		{
			set{ _usecardcode311=value;}
			get{return _usecardcode311;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UseCardCode312
		{
			set{ _usecardcode312=value;}
			get{return _usecardcode312;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point_Product
		{
			set{ _point_product=value;}
			get{return _point_product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point_Free
		{
			set{ _point_free=value;}
			get{return _point_free;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transport_Name
		{
			set{ _transport_name=value;}
			get{return _transport_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Transport_Price_id
		{
			set{ _transport_price_id=value;}
			get{return _transport_price_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transport_Code
		{
			set{ _transport_code=value;}
			get{return _transport_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Transport_Mark
		{
			set{ _transport_mark=value;}
			get{return _transport_mark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal EditMoney_Order
		{
			set{ _editmoney_order=value;}
			get{return _editmoney_order;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal EditMoney_Transport
		{
			set{ _editmoney_transport=value;}
			get{return _editmoney_transport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal EditMoney_Discount
		{
			set{ _editmoney_discount=value;}
			get{return _editmoney_discount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsVerified
		{
			set{ _isverified=value;}
			get{return _isverified;}
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
		public int IsShipped
		{
			set{ _isshipped=value;}
			get{return _isshipped;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsShipped_All
		{
			set{ _isshipped_all=value;}
			get{return _isshipped_all;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsReceived
		{
			set{ _isreceived=value;}
			get{return _isreceived;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsReceived_All
		{
			set{ _isreceived_all=value;}
			get{return _isreceived_all;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCompleted
		{
			set{ _iscompleted=value;}
			get{return _iscompleted;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsInvalid
		{
			set{ _isinvalid=value;}
			get{return _isinvalid;}
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
		public DateTime Time_Verified
		{
			set{ _time_verified=value;}
			get{return _time_verified;}
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
		public DateTime Time_Shipped
		{
			set{ _time_shipped=value;}
			get{return _time_shipped;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Received
		{
			set{ _time_received=value;}
			get{return _time_received;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Completed
		{
			set{ _time_completed=value;}
			get{return _time_completed;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark_Admin
		{
			set{ _remark_admin=value;}
			get{return _remark_admin;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BillType_Name
		{
			set{ _billtype_name=value;}
			get{return _billtype_name;}
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
		public decimal BillType_TaxRate
		{
			set{ _billtype_taxrate=value;}
			get{return _billtype_taxrate;}
		}
		/// <summary>
		/// 211订购单,212退货单,213订购子订单,214充值单,215订单组,216其它消费,217预定单,218临时订单
		/// </summary>
		public int Type_id_OrderType
		{
			set{ _type_id_ordertype=value;}
			get{return _type_id_ordertype;}
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
		public int IsPrintExpress
		{
			set{ _isprintexpress=value;}
			get{return _isprintexpress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Promotion_Type_ids
		{
			set{ _promotion_type_ids=value;}
			get{return _promotion_type_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Mark
		{
			set{ _mark=value;}
			get{return _mark;}
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
		public int Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_fromorder
		{
			set{ _money_fromorder=value;}
			get{return _money_fromorder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCreateCash
		{
			set{ _iscreatecash=value;}
			get{return _iscreatecash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCreateNewOrder
		{
			set{ _iscreateneworder=value;}
			get{return _iscreateneworder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_CreateCash
		{
			set{ _time_createcash=value;}
			get{return _time_createcash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_CreateNewOrder
		{
			set{ _time_createneworder=value;}
			get{return _time_createneworder;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Site_id
		{
			set{ _site_id=value;}
			get{return _site_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Point_Buy
		{
			set{ _point_buy=value;}
			get{return _point_buy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BLNo
		{
			set{ _blno=value;}
			get{return _blno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ContainerNo
		{
			set{ _containerno=value;}
			get{return _containerno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SealNo
		{
			set{ _sealno=value;}
			get{return _sealno;}
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
		public decimal Money_Property
		{
			set{ _money_property=value;}
			get{return _money_property;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string weixin_prepay_id
		{
			set{ _weixin_prepay_id=value;}
			get{return _weixin_prepay_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSupplierCash
		{
			set{ _issuppliercash=value;}
			get{return _issuppliercash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_OnlinepayFee
		{
			set{ _money_onlinepayfee=value;}
			get{return _money_onlinepayfee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Site_id_pay
		{
			set{ _site_id_pay=value;}
			get{return _site_id_pay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PickUp_id
		{
			set{ _pickup_id=value;}
			get{return _pickup_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PickUp_Name
		{
			set{ _pickup_name=value;}
			get{return _pickup_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime PickUp_Date
		{
			set{ _pickup_date=value;}
			get{return _pickup_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Refund_VAT
		{
			set{ _refund_vat=value;}
			get{return _refund_vat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Refund_Fee
		{
			set{ _refund_fee=value;}
			get{return _refund_fee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Language_id
		{
			set{ _language_id=value;}
			get{return _language_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_Delivery_id
		{
			set{ _supplier_delivery_id=value;}
			get{return _supplier_delivery_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRefund
		{
			set{ _isrefund=value;}
			get{return _isrefund;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Refund
		{
			set{ _time_refund=value;}
			get{return _time_refund;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Promotion_Type_Name
		{
			set{ _promotion_type_name=value;}
			get{return _promotion_type_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_NickName
		{
			set{ _user_nickname=value;}
			get{return _user_nickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Paid
		{
			set{ _money_paid=value;}
			get{return _money_paid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsReserve
		{
			set{ _isreserve=value;}
			get{return _isreserve;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_fanxianpay
		{
			set{ _money_fanxianpay=value;}
			get{return _money_fanxianpay;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_Tax
		{
			set{ _money_tax=value;}
			get{return _money_tax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal DT_Money
		{
			set{ _dt_money=value;}
			get{return _dt_money;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string KeyCode
		{
			set{ _keycode=value;}
			get{return _keycode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PayNo
		{
			set{ _payno=value;}
			get{return _payno;}
		}
		#endregion

	}
}

