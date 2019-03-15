using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Supplier_Group
	{
		#region Model
		private int _id=0;
		private int _billingdays=0;
		private int _days_checkuserlow=0;
		private int _grade=0;
		private int _isshow=0;
		private int _issubmit=0;
		private decimal _marginprice=0;
		private string _menu_ids="";
		private string _menu_ids_index="";
		private string _name="";
		private int _producttop=0;
		private string _remark="";
		private int _servicedays=0;
		private decimal _serviceprice=0;
		private int _sort=0;
		private string _supplier_skin_ids="";
		private string _type="";
		private int _userlow=0;
		private int _usertop=0;
		private string _verified_ids="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int BillingDays
		{
			set{ _billingdays=value;}
			get{return _billingdays;}
		}
		public int Days_checkuserlow
		{
			set{ _days_checkuserlow=value;}
			get{return _days_checkuserlow;}
		}
		public int Grade
		{
			set{ _grade=value;}
			get{return _grade;}
		}
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		public int IsSubmit
		{
			set{ _issubmit=value;}
			get{return _issubmit;}
		}
		public decimal MarginPrice
		{
			set{ _marginprice=value;}
			get{return _marginprice;}
		}
		public string Menu_ids
		{
			set{ _menu_ids=value;}
			get{return _menu_ids;}
		}
		public string Menu_ids_index
		{
			set{ _menu_ids_index=value;}
			get{return _menu_ids_index;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int ProductTop
		{
			set{ _producttop=value;}
			get{return _producttop;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public int ServiceDays
		{
			set{ _servicedays=value;}
			get{return _servicedays;}
		}
		public decimal ServicePrice
		{
			set{ _serviceprice=value;}
			get{return _serviceprice;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string Supplier_Skin_ids
		{
			set{ _supplier_skin_ids=value;}
			get{return _supplier_skin_ids;}
		}
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		public int UserLow
		{
			set{ _userlow=value;}
			get{return _userlow;}
		}
		public int UserTop
		{
			set{ _usertop=value;}
			get{return _usertop;}
		}
		public string Verified_ids
		{
			set{ _verified_ids=value;}
			get{return _verified_ids;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

