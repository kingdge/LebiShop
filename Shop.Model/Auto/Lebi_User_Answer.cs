using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_User_Answer
	{
		#region Model
		private int _id=0;
		private string _answer="";
		private int _user_question_id=0;
		private int _user_id=0;
		private Lebi_User_Answer _model;
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
		public string Answer
		{
			set{ _answer=value;}
			get{return _answer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_Question_id
		{
			set{ _user_question_id=value;}
			get{return _user_question_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		#endregion

	}
}

