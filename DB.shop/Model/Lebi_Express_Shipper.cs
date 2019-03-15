using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Express_Shipper
	{
		#region Model
		private int _id=0;
		private string _address="";
		private string _city="";
		private string _mobile="";
		private string _remark="";
		private string _sitename="";
		private int _sort=0;
		private int _status=0;
		private int _supplier_id=0;
		private string _tel="";
		private string _username="";
		private string _zipcode="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Address
		{
			set{ _address=value;}
			get{return _address;}
		}
		public string City
		{
			set{ _city=value;}
			get{return _city;}
		}
		public string Mobile
		{
			set{ _mobile=value;}
			get{return _mobile;}
		}
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public string SiteName
		{
			set{ _sitename=value;}
			get{return _sitename;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public int Status
		{
			set{ _status=value;}
			get{return _status;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		public string ZipCode
		{
			set{ _zipcode=value;}
			get{return _zipcode;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

