using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_DT_Config
	{
		#region Model
		private int _id=0;
		private int _dt_id=0;
		private string _name="";
		private string _value="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public int DT_id
		{
			set{ _dt_id=value;}
			get{return _dt_id;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Value
		{
			set{ _value=value;}
			get{return _value;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

