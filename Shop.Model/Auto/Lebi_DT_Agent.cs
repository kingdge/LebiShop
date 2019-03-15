using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_DT_Agent
	{
		#region Model
		private int _id=0;
		private int _isusedagent=0;
		private int _commissionmoneydays=0;
		private int _dt_id=0;
		private decimal _angent1_commission=0;
		private decimal _angent2_commission=0;
		private decimal _angent3_commission=0;
		private Lebi_DT_Agent _model;
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
		public int IsUsedAgent
		{
			set{ _isusedagent=value;}
			get{return _isusedagent;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CommissionMoneyDays
		{
			set{ _commissionmoneydays=value;}
			get{return _commissionmoneydays;}
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
		public decimal Angent1_Commission
		{
			set{ _angent1_commission=value;}
			get{return _angent1_commission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Angent2_Commission
		{
			set{ _angent2_commission=value;}
			get{return _angent2_commission;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Angent3_Commission
		{
			set{ _angent3_commission=value;}
			get{return _angent3_commission;}
		}
		#endregion

	}
}

