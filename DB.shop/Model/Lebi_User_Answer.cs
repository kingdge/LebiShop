using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_User_Answer
	{
		#region Model
		private int _id=0;
		private string _answer="";
		private int _user_id=0;
		private int _user_question_id=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Answer
		{
			set{ _answer=value;}
			get{return _answer;}
		}
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		public int User_Question_id
		{
			set{ _user_question_id=value;}
			get{return _user_question_id;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

