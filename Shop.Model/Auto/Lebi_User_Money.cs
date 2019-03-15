using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_User_Money
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private decimal _money=0;
		private DateTime _time_add=DateTime.Now;
		private int _type_id_moneystatus=0;
		private int _type_id_moneytype=0;
		private int _admin_id=0;
		private string _description="";
		private int _order_id=0;
		private string _order_code="";
		private DateTime _time_update=DateTime.Now;
		private string _user_username="";
		private string _admin_username="";
		private string _user_realname="";
		private string _remark="";
		private decimal _money_after=0;
		private decimal _money_fanxian_after=0;
		private int _dt_id=0;
		private string _order_payno="";
		private Lebi_User_Money _model;
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
		/// 190销售扣点,191充值,192消费,193取现,194积分兑换,195返现,196购买代理资格,197商铺保证金,198商铺服务费,199佣金
		/// </summary>
		public int Type_id_MoneyType
		{
			set{ _type_id_moneytype=value;}
			get{return _type_id_moneytype;}
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
		public string User_UserName
		{
			set{ _user_username=value;}
			get{return _user_username;}
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
		public string User_RealName
		{
			set{ _user_realname=value;}
			get{return _user_realname;}
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
		public decimal Money_after
		{
			set{ _money_after=value;}
			get{return _money_after;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Money_fanxian_after
		{
			set{ _money_fanxian_after=value;}
			get{return _money_fanxian_after;}
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
		public string Order_PayNo
		{
			set{ _order_payno=value;}
			get{return _order_payno;}
		}
		#endregion

	}
}

