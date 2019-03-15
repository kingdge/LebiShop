using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Message
	{
		#region Model
		private int _id=0;
		private string _content="";
		private int _dt_id=0;
		private string _ip="";
		private int _isread=0;
		private int _issystem=0;
		private string _language="";
		private int _message_type_id=0;
		private int _parentid=0;
		private int _supplier_id=0;
		private DateTime _time_add=DateTime.Now;
		private string _title="";
		private int _user_id_from=0;
		private int _user_id_to=0;
		private string _user_name_from="";
		private string _user_name_to="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Content
		{
			set{ _content=value;}
			get{return _content;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public string IP
		{
			set{ _ip=value;}
			get{return _ip;}
		}
		public int IsRead
		{
			set{ _isread=value;}
			get{return _isread;}
		}
		public int IsSystem
		{
			set{ _issystem=value;}
			get{return _issystem;}
		}
		public string Language
		{
			set{ _language=value;}
			get{return _language;}
		}
		public int Message_Type_id
		{
			set{ _message_type_id=value;}
			get{return _message_type_id;}
		}
		public int Parentid
		{
			set{ _parentid=value;}
			get{return _parentid;}
		}
		public int Supplier_id
		{
			set{ _supplier_id=value;}
			get{return _supplier_id;}
		}
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		public int User_id_From
		{
			set{ _user_id_from=value;}
			get{return _user_id_from;}
		}
		public int User_id_To
		{
			set{ _user_id_to=value;}
			get{return _user_id_to;}
		}
		public string User_Name_From
		{
			set{ _user_name_from=value;}
			get{return _user_name_from;}
		}
		public string User_Name_To
		{
			set{ _user_name_to=value;}
			get{return _user_name_to;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

