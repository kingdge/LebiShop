using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Message_Type
	{
		#region Model
		private int _id=0;
		private string _name="";
		private int _sort=0;
		private int _type_id_messagetypeclass=0;
		private Lebi_Message_Type _model;
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
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
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
		public int Type_id_MessageTypeClass
		{
			set{ _type_id_messagetypeclass=value;}
			get{return _type_id_messagetypeclass;}
		}
		#endregion

	}
}

