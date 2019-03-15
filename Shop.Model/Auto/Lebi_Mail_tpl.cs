using System;
namespace Shop.Model
{
	/// <summary>
	/// 实体类Lebi_Mail_tpl 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class Lebi_Mail_tpl
	{
		public Lebi_Mail_tpl()
		{}
		#region Model
		private int _id=0;
		private string _mail_zc_title="";
		private string _mail_zc_content="";
		private string _mail_dd_title="";
		private string _mail_dd_content="";
		private string _mail_ddqrfh_title="";
		private string _mail_ddqrfh_content="";
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
		public string Mail_zc_Title
		{
			set{ _mail_zc_title=value;}
			get{return _mail_zc_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mail_zc_Content
		{
			set{ _mail_zc_content=value;}
			get{return _mail_zc_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mail_dd_Title
		{
			set{ _mail_dd_title=value;}
			get{return _mail_dd_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mail_dd_Content
		{
			set{ _mail_dd_content=value;}
			get{return _mail_dd_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mail_ddqrfh_Title
		{
			set{ _mail_ddqrfh_title=value;}
			get{return _mail_ddqrfh_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Mail_ddqrfh_content
		{
			set{ _mail_ddqrfh_content=value;}
			get{return _mail_ddqrfh_content;}
		}
		#endregion Model

	}
}

