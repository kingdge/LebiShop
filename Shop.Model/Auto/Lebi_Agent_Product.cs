using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Agent_Product
	{
		#region Model
		private int _id=0;
		private int _user_id=0;
		private string _user_username="";
		private int _product_id=0;
		private Lebi_Agent_Product _model;
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
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
		}
		#endregion

	}
}

