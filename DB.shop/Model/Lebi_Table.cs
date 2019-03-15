using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Table
	{
		#region Model
		private int _id=0;
		private int _char_length=0;
		private string _defaultval="";
		private int _isidentity=0;
		private int _isnullable=0;
		private int _ispk=0;
		private string _name="";
		private int _numeric_length=0;
		private int _numeric_scale=0;
		private int _parentid=0;
		private string _parentname="";
		private string _remark="";
		private string _type="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int char_length
		{
			set{ _char_length=value;}
			get{return _char_length;}
		}
		public string defaultval
		{
			set{ _defaultval=value;}
			get{return _defaultval;}
		}
		public int isidentity
		{
			set{ _isidentity=value;}
			get{return _isidentity;}
		}
		public int isnullable
		{
			set{ _isnullable=value;}
			get{return _isnullable;}
		}
		public int ispk
		{
			set{ _ispk=value;}
			get{return _ispk;}
		}
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public int numeric_length
		{
			set{ _numeric_length=value;}
			get{return _numeric_length;}
		}
		public int numeric_scale
		{
			set{ _numeric_scale=value;}
			get{return _numeric_scale;}
		}
		public int parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public string parentname
		{
			set{ _parentname=value;}
			get{return _parentname;}
		}
		public string remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

