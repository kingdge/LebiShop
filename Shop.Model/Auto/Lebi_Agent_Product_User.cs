using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Agent_Product_User
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private string _user_username="";
		private int _agent_product_level_id=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _count_product=0;
		private int _count_product_change=0;
		private int _count_product_change_used=0;
		private decimal _commission=0;
		private string _remark="";
		private int _isfailure=0;
		private Lebi_Agent_Product_User _model;
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
		public int Agent_Product_Level_id
		{
			set{ _agent_product_level_id=value;}
			get{return _agent_product_level_id;}
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
		public DateTime Time_end
		{
			set{ _time_end=value;}
			get{return _time_end;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_Product
		{
			set{ _count_product=value;}
			get{return _count_product;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_product_change
		{
			set{ _count_product_change=value;}
			get{return _count_product_change;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Count_product_change_used
		{
			set{ _count_product_change_used=value;}
			get{return _count_product_change_used;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal Commission
		{
			set{ _commission=value;}
			get{return _commission;}
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
		public int IsFailure
		{
			set{ _isfailure=value;}
			get{return _isfailure;}
		}
		#endregion

	}
}

