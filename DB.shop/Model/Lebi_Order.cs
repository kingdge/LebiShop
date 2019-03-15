using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Order
	{
		#region Model
		private int _id = 0;
		private string _code = "";
		private int _user_id = 0;
		private string _user_username = "";
		private string _t_name = "";
		private int _t_area_id = 0;
		private string _t_address = "";
		private string _t_phone = "";
		private string _t_mobilephone = "";
		private string _t_postalcode = "";
		private string _t_email = "";
		private string _remark_user = "";
		private int _pay_id = 0;
		private string _pay = "";
		private int _onlinepay_id = 0;
		private string _onlinepay = "";
		private string _onlinepay_code = "";
		private decimal _money_order = 0;
		private decimal _money_pay = 0;
		private decimal _money_product = 0;
		private decimal _money_transport = 0;
		private decimal _money_transport_cut = 0;
		private decimal _money_bill = 0;
		private decimal _money_market = 0;
		private decimal _money_give = 0;
		private decimal _money_cut = 0;
		private decimal _money_usercut = 0;
		private decimal _money_cost = 0;
		private decimal _money_usecard311 = 0;
		private decimal _money_usecard312 = 0;
		private string _usecardcode311 = "";
		private string _usecardcode312 = "";
		private decimal _weight = 0;
		private decimal _volume = 0;
		private decimal _point = 0;
		private decimal _point_product = 0;
		private decimal _point_free = 0;
		private string _transport_name = "";
		private int _transport_id = 0;
		private int _transport_price_id = 0;
		private string _transport_code = "";
		private string _transport_mark = "";
		private decimal _editmoney_order = 0;
		private decimal _editmoney_transport = 0;
		private decimal _editmoney_discount = 0;
		private int _isverified = 0;
		private int _ispaid = 0;
		private int _isshipped = 0;
		private int _isshipped_all = 0;
		private int _isreceived = 0;
		private int _isreceived_all = 0;
		private int _iscompleted = 0;
		private int _isinvalid = 0;
		private DateTime _time_add = DateTime.Now;
		private DateTime _time_verified = DateTime.Now;
		private DateTime _time_paid = DateTime.Now;
		private DateTime _time_shipped = DateTime.Now;
		private DateTime _time_received = DateTime.Now;
		private DateTime _time_completed = DateTime.Now;
		private string _remark_admin = "";
		private string _billtype_name = "";
		private int _billtype_id = 0;
		private decimal _billtype_taxrate = 0;
		private int _type_id_ordertype = 0;
		private int _order_id = 0;
		private int _isprintexpress = 0;
		private string _promotion_type_ids = "";
		private int _mark = 0;
		private int _currency_id = 0;
		private string _currency_code = "";
		private decimal _currency_exchangerate = 0;
		private string _currency_msige = "";
		private int _flag = 0;
		private decimal _money_fromorder = 0;
		private int _iscreatecash = 0;
		private int _iscreateneworder = 0;
		private DateTime _time_createcash = DateTime.Now;
		private DateTime _time_createneworder = DateTime.Now;
		private int _site_id = 0;
		private decimal _point_buy = 0;
		private string _blno = "";
		private string _containerno = "";
		private string _sealno = "";
		private int _supplier_id = 0;
		private decimal _money_property = 0;
		private string _weixin_prepay_id = "";
		private int _issuppliercash = 0;
		private decimal _money_onlinepayfee = 0;
		private int _site_id_pay = 0;
		private int _pickup_id = 0;
		private string _pickup_name = "";
		private DateTime _pickup_date = DateTime.Now;
		private decimal _refund_vat = 0;
		private decimal _refund_fee = 0;
		private int _language_id = 0;
		private int _supplier_delivery_id = 0;
		private int _isrefund = 0;
		private DateTime _time_refund = DateTime.Now;
		private string _promotion_type_name = "";
		private string _user_nickname = "";
		private decimal _money_paid = 0;
		private int _isreserve = 0;
		private decimal _money_fanxianpay = 0;
		private decimal _money_tax = 0;
		private int _dt_id = 0;
		private decimal _dt_money = 0;
		private int _isdel = 0;
		private string _keycode = "";
		private string _payno = "";
		private int _source = 0;
		private string _project_ids = "";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
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
		public string T_Name
		{
			set{ _t_name=value;}
			get{return _t_name;}
		}
		public int T_Area_id
		{
			set{ _t_area_id=value;}
			get{return _t_area_id;}
		}
		public string T_Address
		{
			set{ _t_address=value;}
			get{return _t_address;}
		}
		public string T_Phone
		{
			set{ _t_phone=value;}
			get{return _t_phone;}
		}
		public string T_MobilePhone
		{
			set{ _t_mobilephone=value;}
			get{return _t_mobilephone;}
		}
		public string T_Postalcode
		{
			set{ _t_postalcode=value;}
			get{return _t_postalcode;}
		}
		public string T_Email
		{
			set{ _t_email=value;}
			get{return _t_email;}
		}
		public string Remark_User
		{
			set{ _remark_user=value;}
			get{return _remark_user;}
		}
		public int Pay_id
		{
			set{ _pay_id=value;}
			get{return _pay_id;}
		}
		public string Pay
		{
			set{ _pay=value;}
			get{return _pay;}
		}
		public int OnlinePay_id
		{
			set{ _onlinepay_id=value;}
			get{return _onlinepay_id;}
		}
		public string OnlinePay
		{
			set{ _onlinepay=value;}
			get{return _onlinepay;}
		}
		public string OnlinePay_Code
		{
			set{ _onlinepay_code=value;}
			get{return _onlinepay_code;}
		}
		public decimal Money_Order
		{
			set{ _money_order=value;}
			get{return _money_order;}
		}
		public decimal Money_Pay
		{
			set{ _money_pay=value;}
			get{return _money_pay;}
		}
		public decimal Money_Product
		{
			set{ _money_product=value;}
			get{return _money_product;}
		}
		public decimal Money_Transport
		{
			set{ _money_transport=value;}
			get{return _money_transport;}
		}
		public decimal Money_Transport_Cut
		{
			set{ _money_transport_cut=value;}
			get{return _money_transport_cut;}
		}
		public decimal Money_Bill
		{
			set{ _money_bill=value;}
			get{return _money_bill;}
		}
		public decimal Money_Market
		{
			set{ _money_market=value;}
			get{return _money_market;}
		}
		public decimal Money_Give
		{
			set{ _money_give=value;}
			get{return _money_give;}
		}
		public decimal Money_Cut
		{
			set{ _money_cut=value;}
			get{return _money_cut;}
		}
		public decimal Money_UserCut
		{
			set{ _money_usercut=value;}
			get{return _money_usercut;}
		}
		public decimal Money_Cost
		{
			set{ _money_cost=value;}
			get{return _money_cost;}
		}
		public decimal Money_UseCard311
		{
			set{ _money_usecard311=value;}
			get{return _money_usecard311;}
		}
		public decimal Money_UseCard312
		{
			set{ _money_usecard312=value;}
			get{return _money_usecard312;}
		}
		public string UseCardCode311
		{
			set{ _usecardcode311=value;}
			get{return _usecardcode311;}
		}
		public string UseCardCode312
		{
			set{ _usecardcode312=value;}
			get{return _usecardcode312;}
		}
		public decimal Weight
		{
			set{ _weight=value;}
			get{return _weight;}
		}
		public decimal Volume
		{
			set{ _volume=value;}
			get{return _volume;}
		}
		public decimal Point
		{
			set{ _point=value;}
			get{return _point;}
		}
		public decimal Point_Product
		{
			set{ _point_product=value;}
			get{return _point_product;}
		}
		public decimal Point_Free
		{
			set{ _point_free=value;}
			get{return _point_free;}
		}
		public string Transport_Name
		{
			set{ _transport_name=value;}
			get{return _transport_name;}
		}
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		public int Transport_Price_id
		{
			set{ _transport_price_id=value;}
			get{return _transport_price_id;}
		}
		public string Transport_Code
		{
			set{ _transport_code=value;}
			get{return _transport_code;}
		}
		public string Transport_Mark
		{
			set{ _transport_mark=value;}
			get{return _transport_mark;}
		}
		public decimal EditMoney_Order
		{
			set{ _editmoney_order=value;}
			get{return _editmoney_order;}
		}
		public decimal EditMoney_Transport
		{
			set{ _editmoney_transport=value;}
			get{return _editmoney_transport;}
		}
		public decimal EditMoney_Discount
		{
			set{ _editmoney_discount=value;}
			get{return _editmoney_discount;}
		}
		public int IsVerified
		{
			set{ _isverified=value;}
			get{return _isverified;}
		}
		public int IsPaid
		{
			set{ _ispaid=value;}
			get{return _ispaid;}
		}
		public int IsShipped
		{
			set{ _isshipped=value;}
			get{return _isshipped;}
		}
		public int IsShipped_All
		{
			set{ _isshipped_all=value;}
			get{return _isshipped_all;}
		}
		public int IsReceived
		{
			set{ _isreceived=value;}
			get{return _isreceived;}
		}
		public int IsReceived_All
		{
			set{ _isreceived_all=value;}
			get{return _isreceived_all;}
		}
		public int IsCompleted
		{
			set{ _iscompleted=value;}
			get{return _iscompleted;}
		}
		public int IsInvalid
		{
			set{ _isinvalid=value;}
			get{return _isinvalid;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Verified
		{
			set{ _time_verified=value;}
			get{return _time_verified;}
		}
		public DateTime Time_Paid
		{
			set{ _time_paid=value;}
			get{return _time_paid;}
		}
		public DateTime Time_Shipped
		{
			set{ _time_shipped=value;}
			get{return _time_shipped;}
		}
		public DateTime Time_Received
		{
			set{ _time_received=value;}
			get{return _time_received;}
		}
		public DateTime Time_Completed
		{
			set{ _time_completed=value;}
			get{return _time_completed;}
		}
		public string Remark_Admin
		{
			set{ _remark_admin=value;}
			get{return _remark_admin;}
		}
		public string BillType_Name
		{
			set{ _billtype_name=value;}
			get{return _billtype_name;}
		}
		public int BillType_id
		{
			set{ _billtype_id=value;}
			get{return _billtype_id;}
		}
		public decimal BillType_TaxRate
		{
			set{ _billtype_taxrate=value;}
			get{return _billtype_taxrate;}
		}
		public int Type_id_OrderType
		{
			set{ _type_id_ordertype=value;}
			get{return _type_id_ordertype;}
		}
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		public int IsPrintExpress
		{
			set{ _isprintexpress=value;}
			get{return _isprintexpress;}
		}
		public string Promotion_Type_ids
		{
			set{ _promotion_type_ids=value;}
			get{return _promotion_type_ids;}
		}
		public int Mark
		{
			set{ _mark=value;}
			get{return _mark;}
		}
		public int Currency_id
		{
			set{ _currency_id=value;}
			get{return _currency_id;}
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
		public string Currency_Msige
		{
			set{ _currency_msige=value;}
			get{return _currency_msige;}
		}
		public int Flag
		{
			set{ _flag=value;}
			get{return _flag;}
		}
		public decimal Money_fromorder
		{
			set{ _money_fromorder=value;}
			get{return _money_fromorder;}
		}
		public int IsCreateCash
		{
			set{ _iscreatecash=value;}
			get{return _iscreatecash;}
		}
		public int IsCreateNewOrder
		{
			set{ _iscreateneworder=value;}
			get{return _iscreateneworder;}
		}
		public DateTime Time_CreateCash
		{
			set{ _time_createcash=value;}
			get{return _time_createcash;}
		}
		public DateTime Time_CreateNewOrder
		{
			set{ _time_createneworder=value;}
			get{return _time_createneworder;}
		}
		public int Site_id
		{
			set{ _site_id=value;}
			get{return _site_id;}
		}
		public decimal Point_Buy
		{
			set{ _point_buy=value;}
			get{return _point_buy;}
		}
		public string BLNo
		{
			set{ _blno=value;}
			get{return _blno;}
		}
		public string ContainerNo
		{
			set{ _containerno=value;}
			get{return _containerno;}
		}
		public string SealNo
		{
			set{ _sealno=value;}
			get{return _sealno;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public decimal Money_Property
		{
			set{ _money_property=value;}
			get{return _money_property;}
		}
		public string weixin_prepay_id
		{
			set{ _weixin_prepay_id=value;}
			get{return _weixin_prepay_id;}
		}
		public int IsSupplierCash
		{
			set{ _issuppliercash=value;}
			get{return _issuppliercash;}
		}
		public decimal Money_OnlinepayFee
		{
			set{ _money_onlinepayfee=value;}
			get{return _money_onlinepayfee;}
		}
		public int Site_id_pay
		{
			set{ _site_id_pay=value;}
			get{return _site_id_pay;}
		}
		public int PickUp_id
		{
			set{ _pickup_id=value;}
			get{return _pickup_id;}
		}
		public string PickUp_Name
		{
			set{ _pickup_name=value;}
			get{return _pickup_name;}
		}
		public DateTime PickUp_Date
		{
			set{ _pickup_date=value;}
			get{return _pickup_date;}
		}
		public decimal Refund_VAT
		{
			set{ _refund_vat=value;}
			get{return _refund_vat;}
		}
		public decimal Refund_Fee
		{
			set{ _refund_fee=value;}
			get{return _refund_fee;}
		}
		public int Language_id
		{
			set{ _language_id=value;}
			get{return _language_id;}
		}
		public int Supplier_Delivery_id
		{
			set{ _supplier_delivery_id=value;}
			get{return _supplier_delivery_id;}
		}
		public int IsRefund
		{
			set{ _isrefund=value;}
			get{return _isrefund;}
		}
		public DateTime Time_Refund
		{
			set{ _time_refund=value;}
			get{return _time_refund;}
		}
		public string Promotion_Type_Name
		{
			set{ _promotion_type_name=value;}
			get{return _promotion_type_name;}
		}
		public string User_NickName
		{
			set{ _user_nickname=value;}
			get{return _user_nickname;}
		}
		public decimal Money_Paid
		{
			set{ _money_paid=value;}
			get{return _money_paid;}
		}
		public int IsReserve
		{
			set{ _isreserve=value;}
			get{return _isreserve;}
		}
		public decimal Money_fanxianpay
		{
			set{ _money_fanxianpay=value;}
			get{return _money_fanxianpay;}
		}
		public decimal Money_Tax
		{
			set{ _money_tax=value;}
			get{return _money_tax;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public decimal DT_Money
		{
			set{ _dt_money=value;}
			get{return _dt_money;}
		}
		public int IsDel
		{
			set{ _isdel=value;}
			get{return _isdel;}
		}
		public string KeyCode
		{
			set{ _keycode=value;}
			get{return _keycode;}
		}
		public string PayNo
		{
			set{ _payno=value;}
			get{return _payno;}
		}
		public int Source
		{
			set{ _source=value;}
			get{return _source;}
		}
		public string Project_ids
		{
			set{ _project_ids=value;}
			get{return _project_ids;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

