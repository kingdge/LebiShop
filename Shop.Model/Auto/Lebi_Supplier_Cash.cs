using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Cash
	{
		#region Model
		private int _id=0;
		private decimal _money=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private int _supplier_bank_id=0;
		private string _supplier_user_username="";
		private string _supplier_user_realname="";
		private string _admin_username="";
		private int _admin_id=0;
		private string _remark="";
		private int _type_id_suppliercashstatus=0;
		private Lebi_Supplier_Cash _model;
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
		public int Supplier_Bank_id
		{
			set{ _supplier_bank_id=value;}
			get{return _supplier_bank_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_User_UserName
		{
			set{ _supplier_user_username=value;}
			get{return _supplier_user_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Supplier_User_RealName
		{
			set{ _supplier_user_realname=value;}
			get{return _supplier_user_realname;}
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
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
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
		/// 9000未转账,9001已转账
		/// </summary>
		public int Type_id_SupplierCashStatus
		{
			set{ _type_id_suppliercashstatus=value;}
			get{return _type_id_suppliercashstatus;}
		}
		#endregion

	}
}

