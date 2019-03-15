using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Agent_User
	{
		#region Model
		private int _id=0;
		private decimal _angent1_commission=0;
		private decimal _angent2_commission=0;
		private decimal _angent3_commission=0;
		private int _user_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public decimal Angent1_Commission
		{
			set{ _angent1_commission=value;}
			get{return _angent1_commission;}
		}
		public decimal Angent2_Commission
		{
			set{ _angent2_commission=value;}
			get{return _angent2_commission;}
		}
		public decimal Angent3_Commission
		{
			set{ _angent3_commission=value;}
			get{return _angent3_commission;}
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

