using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Agent_Money
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private string _user_username="";
		private decimal _money=0;
		private int _type_id_agentmoneytype=0;
		private int _type_id_agentmoneystatus=0;
		private DateTime _time_add=DateTime.Now;
		private string _remark="";
		private int _order_id=0;
		private string _order_code="";
		private int _area_id=0;
		private int _product_id=0;
		private int _product_id_parent=0;
		private string _product_number="";
		private int _supplier_id=0;
		private int _dt_id=0;
		private Lebi_Agent_Money _model;
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
		public decimal Money
		{
			set{ _money=value;}
			get{return _money;}
		}
		/// <summary>
		/// 391推广佣金
		/// </summary>
		public int Type_id_AgentMoneyType
		{
			set{ _type_id_agentmoneytype=value;}
			get{return _type_id_agentmoneytype;}
		}
		/// <summary>
		/// 381冻结,382已生效,383无效,384已提现
		/// </summary>
		public int Type_id_AgentMoneyStatus
		{
			set{ _type_id_agentmoneystatus=value;}
			get{return _type_id_agentmoneystatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
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
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Product_id_parent
		{
			set{ _product_id_parent=value;}
			get{return _product_id_parent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Product_Number
		{
			set{ _product_number=value;}
			get{return _product_number;}
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
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		#endregion

	}
}

