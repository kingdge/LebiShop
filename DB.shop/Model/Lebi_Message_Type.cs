using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Message_Type
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private int _type_id_messagetypeclass=0;
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
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
		public int Type_id_MessageTypeClass
		{
			set{ _type_id_messagetypeclass=value;}
			get{return _type_id_messagetypeclass;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

