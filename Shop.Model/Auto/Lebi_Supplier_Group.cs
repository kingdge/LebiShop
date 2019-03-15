using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Group
	{
		#region Model
		private int _id=0;
		private int _grade=0;
		private string _name="";
		private decimal _marginprice=0;
		private decimal _serviceprice=0;
		private int _servicedays=0;
		private string _remark="";
		private int _sort=0;
		private int _issubmit=0;
		private string _menu_ids="";
		private string _menu_ids_index="";
		private int _billingdays=0;
		private string _verified_ids="";
		private int _isshow=0;
		private string _type="";
		private int _producttop=0;
		private int _usertop=0;
		private int _userlow=0;
		private int _days_checkuserlow=0;
		private string _supplier_skin_ids="";
		private Lebi_Supplier_Group _model;
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
		public decimal MarginPrice
		{
			set{ _marginprice=value;}
			get{return _marginprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal ServicePrice
		{
			set{ _serviceprice=value;}
			get{return _serviceprice;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ServiceDays
		{
			set{ _servicedays=value;}
			get{return _servicedays;}
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
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSubmit
		{
			set{ _issubmit=value;}
			get{return _issubmit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Menu_ids
		{
			set{ _menu_ids=value;}
			get{return _menu_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Menu_ids_index
		{
			set{ _menu_ids_index=value;}
			get{return _menu_ids_index;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int BillingDays
		{
			set{ _billingdays=value;}
			get{return _billingdays;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Verified_ids
		{
			set{ _verified_ids=value;}
			get{return _verified_ids;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ProductTop
		{
			set{ _producttop=value;}
			get{return _producttop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserTop
		{
			set{ _usertop=value;}
			get{return _usertop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserLow
		{
			set{ _userlow=value;}
			get{return _userlow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Days_checkuserlow
		{
			set{ _days_checkuserlow=value;}
			get{return _days_checkuserlow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_Skin_ids
		{
			set{ _supplier_skin_ids=value;}
			get{return _supplier_skin_ids;}
		}
		#endregion

	}
}

