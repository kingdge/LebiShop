using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Agent_Product_User
	{
		#region Model
		private int _id=0;
		private int _agent_product_level_id=0;
		private decimal _commission=0;
		private int _count_product=0;
		private int _count_product_change=0;
		private int _count_product_change_used=0;
		private int _isfailure=0;
		private string _remark="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _user_id=0;
		private string _user_username="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Agent_Product_Level_id
		{
			set{ _agent_product_level_id=value;}
			get{return _agent_product_level_id;}
		}
		public decimal Commission
		{
			set{ _commission=value;}
			get{return _commission;}
		}
		public int Count_Product
		{
			set{ _count_product=value;}
			get{return _count_product;}
		}
		public int Count_product_change
		{
			set{ _count_product_change=value;}
			get{return _count_product_change;}
		}
		public int Count_product_change_used
		{
			set{ _count_product_change_used=value;}
			get{return _count_product_change_used;}
		}
		public int IsFailure
		{
			set{ _isfailure=value;}
			get{return _isfailure;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_end
		{
			set{ _time_end=value;}
			get{return _time_end;}
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
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

