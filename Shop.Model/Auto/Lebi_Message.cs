using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Message
	{
		#region Model
		private int _id=0;
		private string _title="";
		private string _content="";
		private DateTime _time_add=DateTime.Now;
		private int _user_id_from=0;
		private int _user_id_to=0;
		private int _isread=0;
		private string _language="";
		private int _message_type_id=0;
		private string _user_name_from="";
		private string _user_name_to="";
		private int _issystem=0;
		private string _ip="";
		private int _supplier_id=0;
		private int _parentid=0;
		private int _dt_id=0;
		private Lebi_Message _model;
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
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id_From
		{
			set{ _user_id_from=value;}
			get{return _user_id_from;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int User_id_To
		{
			set{ _user_id_to=value;}
			get{return _user_id_to;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Message_Type_id
		{
			set{ _message_type_id=value;}
			get{return _message_type_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_Name_From
		{
			set{ _user_name_from=value;}
			get{return _user_name_from;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string User_Name_To
		{
			set{ _user_name_to=value;}
			get{return _user_name_to;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSystem
		{
			set{ _issystem=value;}
			get{return _issystem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IP
		{
			set{ _ip=value;}
			get{return _ip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		#endregion

	}
}

