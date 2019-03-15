using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Product_Limit
	{
		#region Model
		private int _id=0;
		private int _product_id=0;
		private int _user_id=0;
		private int _isshow=0;
		private int _ispriceshow=0;
		private int _isbuy=0;
		private int _userlevel_id=0;
		private Lebi_Product_Limit _model;
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
		public int Product_id
		{
			set{ _product_id=value;}
			get{return _product_id;}
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
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPriceShow
		{
			set{ _ispriceshow=value;}
			get{return _ispriceshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsBuy
		{
			set{ _isbuy=value;}
			get{return _isbuy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int UserLevel_id
		{
			set{ _userlevel_id=value;}
			get{return _userlevel_id;}
		}
		#endregion

	}
}

