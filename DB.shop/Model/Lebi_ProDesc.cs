using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_ProDesc
	{
		#region Model
		private int _id=0;
		private string _description="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

