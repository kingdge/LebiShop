using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Currency
	{
		#region Model
		private int _id=0;
		private decimal _exchangerate=0;
		private string _msige="";
		private string _name="";
		private string _code="";
		private int _sort=0;
		private int _isdefault=0;
		private int _decimallength=0;
		private Lebi_Currency _model;
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
		public decimal ExchangeRate
		{
			set{ _exchangerate=value;}
			get{return _exchangerate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Msige
		{
			set{ _msige=value;}
			get{return _msige;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DecimalLength
		{
			set{ _decimallength=value;}
			get{return _decimallength;}
		}
		#endregion

	}
}

