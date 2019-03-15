using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Money
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private string _description="";
		private decimal _money=0;
		private string _order_code="";
		private int _order_id=0;
		private string _remark="";
		private int _supplier_id=0;
		private string _supplier_subname="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _type_id_moneystatus=0;
		private int _type_id_suppliermoneytype=0;
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
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
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
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string Supplier_SubName
		{
			set{ _supplier_subname=value;}
			get{return _supplier_subname;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		public int Type_id_MoneyStatus
		{
			set{ _type_id_moneystatus=value;}
			get{return _type_id_moneystatus;}
		}
		public int Type_id_SupplierMoneyType
		{
			set{ _type_id_suppliermoneytype=value;}
			get{return _type_id_suppliermoneytype;}
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

