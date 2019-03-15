using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Currency
	{
		#region Model
		private int _id=0;
		private string _code="";
		private int _decimallength=0;
		private decimal _exchangerate=0;
		private int _isdefault=0;
		private string _msige="";
		private string _name="";
		private int _sort=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Code
		{
			set{ _code=value;}
			get{return _code;}
		}
		public int DecimalLength
		{
			set{ _decimallength=value;}
			get{return _decimallength;}
		}
		public decimal ExchangeRate
		{
			set{ _exchangerate=value;}
			get{return _exchangerate;}
		}
		public int IsDefault
		{
			set{ _isdefault=value;}
			get{return _isdefault;}
		}
		public string Msige
		{
			set{ _msige=value;}
			get{return _msige;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

