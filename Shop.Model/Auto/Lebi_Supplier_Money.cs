using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Money
	{
		#region Model
		private int _id=0;
		private int _supplier_id=0;
		private decimal _money=0;
		private DateTime _time_add=DateTime.Now;
		private int _type_id_moneystatus=0;
		private int _type_id_suppliermoneytype=0;
		private int _admin_id=0;
		private string _description="";
		private int _order_id=0;
		private string _order_code="";
		private DateTime _time_update=DateTime.Now;
		private string _admin_username="";
		private string _supplier_subname="";
		private string _remark="";
		private string _user_username="";
		private Lebi_Supplier_Money _model;
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
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
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
		/// 181有效资金,182无效资金,183冻结资金
		/// </summary>
		public int Type_id_MoneyStatus
		{
			set{ _type_id_moneystatus=value;}
			get{return _type_id_moneystatus;}
		}
		/// <summary>
		/// 341销售,342退货,343提现,344人工调整
		/// </summary>
		public int Type_id_SupplierMoneyType
		{
			set{ _type_id_suppliermoneytype=value;}
			get{return _type_id_suppliermoneytype;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
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
		public int Order_id
		{
			set{ _order_id=value;}
			get{return _order_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Order_Code
		{
			set{ _order_code=value;}
			get{return _order_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_SubName
		{
			set{ _supplier_subname=value;}
			get{return _supplier_subname;}
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
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
		}
		#endregion

	}
}

