using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_User_Card
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private int _card_id=0;
		private Lebi_User_Card _model;
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
		public int Card_id
		{
			set{ _card_id=value;}
			get{return _card_id;}
		}
		#endregion

	}
}

