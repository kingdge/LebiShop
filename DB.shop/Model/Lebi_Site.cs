using System;
namespace DB.LebiShop
{
	[Serializable]
	public class Lebi_Site
	{
		#region Model
		private int _id=0;
		private string _copyright="";
		private string _description="";
		private string _domain="";
		private string _email="";
		private string _fax="";
		private string _foothtml="";
		private int _ismobile=0;
		private string _keywords="";
		private string _logoimg="";
		private string _name="";
		private string _path="";
		private string _phone="";
		private string _platform_weixin_custemtoken="";
		private string _platform_weixin_id="";
		private string _platform_weixin_image_qrcode="";
		private string _platform_weixin_number="";
		private string _platform_weixin_secret="";
		private string _platform_weixin_subscribe_automsg="";
		private string _qq="";
		private string _servicep="";
		private int _sort=0;
		private string _subname="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_edit=DateTime.Now;
		private string _title="";
		private string _UpdateCols="";
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		public string Copyright
		{
			set{ _copyright=value;}
			get{return _copyright;}
		}
		public string Description
		{
			set{ _description=value;}
			get{return _description;}
		}
		public string Domain
		{
			set{ _domain=value;}
			get{return _domain;}
		}
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		public string Fax
		{
			set{ _fax=value;}
			get{return _fax;}
		}
		public string FootHtml
		{
			set{ _foothtml=value;}
			get{return _foothtml;}
		}
		public int IsMobile
		{
			set{ _ismobile=value;}
			get{return _ismobile;}
		}
		public string Keywords
		{
			set{ _keywords=value;}
			get{return _keywords;}
		}
		public string Logoimg
		{
			set{ _logoimg=value;}
			get{return _logoimg;}
		}
		public string Name
		{
			set{ _name=value;}
			get{return _name;}
		}
		public string Path
		{
			set{ _path=value;}
			get{return _path;}
		}
		public string Phone
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		public string platform_weixin_custemtoken
		{
			set{ _platform_weixin_custemtoken=value;}
			get{return _platform_weixin_custemtoken;}
		}
		public string platform_weixin_id
		{
			set{ _platform_weixin_id=value;}
			get{return _platform_weixin_id;}
		}
		public string platform_weixin_image_qrcode
		{
			set{ _platform_weixin_image_qrcode=value;}
			get{return _platform_weixin_image_qrcode;}
		}
		public string platform_weixin_number
		{
			set{ _platform_weixin_number=value;}
			get{return _platform_weixin_number;}
		}
		public string platform_weixin_secret
		{
			set{ _platform_weixin_secret=value;}
			get{return _platform_weixin_secret;}
		}
		public string platform_weixin_subscribe_automsg
		{
			set{ _platform_weixin_subscribe_automsg=value;}
			get{return _platform_weixin_subscribe_automsg;}
		}
		public string QQ
		{
			set{ _qq=value;}
			get{return _qq;}
		}
		public string ServiceP
		{
			set{ _servicep=value;}
			get{return _servicep;}
		}
		public int Sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}
		public string SubName
		{
			set{ _subname=value;}
			get{return _subname;}
		}
		public DateTime Time_add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		public DateTime Time_Edit
		{
			set{ _time_edit=value;}
			get{return _time_edit;}
		}
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		public string UpdateCols
		{
			set{_UpdateCols=value;}
			get{return _UpdateCols;}
		}
		#endregion

	}
}

