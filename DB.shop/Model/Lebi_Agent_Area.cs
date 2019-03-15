using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Agent_Area
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private int _area_id=0;
		private int _cardorder_id=0;
		private decimal _commission_1=0;
		private decimal _commission_2=0;
		private int _isfailure=0;
		private decimal _price=0;
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
		public int Area_id
		{
			set{ _area_id=value;}
			get{return _area_id;}
		}
		public int CardOrder_id
		{
			set{ _cardorder_id=value;}
			get{return _cardorder_id;}
		}
		public decimal Commission_1
		{
			set{ _commission_1=value;}
			get{return _commission_1;}
		}
		public decimal Commission_2
		{
			set{ _commission_2=value;}
			get{return _commission_2;}
		}
		public int IsFailure
		{
			set{ _isfailure=value;}
			get{return _isfailure;}
		}
		public decimal Price
		{
			set{ _price=value;}
			get{return _price;}
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

