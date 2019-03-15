using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Express
	{
		#region Model
		private int _id=0;
		private string _background="";
		private string _height="";
		private string _name="";
		private string _pos="";
		private int _sort=0;
		private int _status=0;
		private int _transport_id=0;
		private string _width="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Background
		{
			set{ _background=value;}
			get{return _background;}
		}
		public string Height
		{
			set{ _height=value;}
			get{return _height;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Pos
		{
			set{ _pos=value;}
			get{return _pos;}
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
		public int Transport_id
		{
			set{ _transport_id=value;}
			get{return _transport_id;}
		}
		public string Width
		{
			set{ _width=value;}
			get{return _width;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

