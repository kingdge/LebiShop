using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Supplier_Skin
	{
		#region Model
		private int _id=0;
		private string _type="";
		private string _path="";
		private int _sort=0;
		private string _head="";
		private string _shortbar="";
		private string _longbar="";
		private string _image="";
		private string _name="";
		private int _isshow=0;
		private int _ispublic=0;
		private Lebi_Supplier_Skin _model;
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
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
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
		public string head
		{
			set{ _head=value;}
			get{return _head;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string shortbar
		{
			set{ _shortbar=value;}
			get{return _shortbar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string longbar
		{
			set{ _longbar=value;}
			get{return _longbar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Image
		{
			set{ _image=value;}
			get{return _image;}
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
		public int IsShow
		{
			set{ _isshow=value;}
			get{return _isshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsPublic
		{
			set{ _ispublic=value;}
			get{return _ispublic;}
		}
		#endregion

	}
}

