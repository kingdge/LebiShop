using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User_Card
	{
		#region Model
		private int _id=0;
		private int _card_id=0;
		private int _user_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int Card_id
		{
			set{ _card_id=value;}
			get{return _card_id;}
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

