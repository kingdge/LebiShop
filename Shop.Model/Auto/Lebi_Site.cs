using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Site
	{
		#region Model
		private int _id=0;
		private string _name="";
		private string _subname="";
		private string _title="";
		private string _keywords="";
		private string _description="";
		private string _logoimg="";
		private string _phone="";
		private string _fax="";
		private string _email="";
		private string _qq="";
		private string _copyright="";
		private string _servicep="";
		private string _domain="";
		private int _sort=0;
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_edit=DateTime.Now;
		private string _path="";
		private int _ismobile=0;
		private string _foothtml="";
		private string _platform_weixin_number="";
		private string _platform_weixin_id="";
		private string _platform_weixin_secret="";
		private string _platform_weixin_image_qrcode="";
		private string _platform_weixin_custemtoken="";
		private string _platform_weixin_subscribe_automsg="";
		private Lebi_Site _model;
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
		public string SubName
		{
			set{ _subname=value;}
			get{return _subname;}
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
		public string Keywords
		{
			set{ _keywords=value;}
			get{return _keywords;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Logoimg
		{
			set{ _logoimg=value;}
			get{return _logoimg;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Copyright
		{
			set{ _copyright=value;}
			get{return _copyright;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ServiceP
		{
			set{ _servicep=value;}
			get{return _servicep;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
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
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Edit
		{
			set{ _time_edit=value;}
			get{return _time_edit;}
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
		public int IsMobile
		{
			set{ _ismobile=value;}
			get{return _ismobile;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FootHtml
		{
			set{ _foothtml=value;}
			get{return _foothtml;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string platform_weixin_number
		{
			set{ _platform_weixin_number=value;}
			get{return _platform_weixin_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string platform_weixin_id
		{
			set{ _platform_weixin_id=value;}
			get{return _platform_weixin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string platform_weixin_secret
		{
			set{ _platform_weixin_secret=value;}
			get{return _platform_weixin_secret;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string platform_weixin_image_qrcode
		{
			set{ _platform_weixin_image_qrcode=value;}
			get{return _platform_weixin_image_qrcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string platform_weixin_custemtoken
		{
			set{ _platform_weixin_custemtoken=value;}
			get{return _platform_weixin_custemtoken;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string platform_weixin_subscribe_automsg
		{
			set{ _platform_weixin_subscribe_automsg=value;}
			get{return _platform_weixin_subscribe_automsg;}
		}
		#endregion

	}
}

