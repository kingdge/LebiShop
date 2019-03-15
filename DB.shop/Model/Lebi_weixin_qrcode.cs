using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_weixin_qrcode
	{
		#region Model
		private int _id=0;
		private int _dt_id=0;
		private DateTime _time_add=DateTime.Now;
		private int _user_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

