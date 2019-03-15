using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Language_Tag
	{
		#region Model
		private int _id=0;
		private string _tag="";
		private string _cn="";
		private int _iscn=0;
		private string _en="";
		private int _isen=0;
		private string _ja="";
		private int _isja=0;
		private string _ar="";
		private int _isar=0;
		private string _ca="";
		private int _isca=0;
		private string _cs="";
		private int _iscs=0;
		private string _cy="";
		private int _iscy=0;
		private string _da="";
		private int _isda=0;
		private string _de="";
		private int _isde=0;
		private string _el="";
		private int _isel=0;
		private string _es="";
		private int _ises=0;
		private string _eu="";
		private int _iseu=0;
		private string _fa="";
		private int _isfa=0;
		private string _fi="";
		private int _isfi=0;
		private string _fr="";
		private int _isfr=0;
		private string _gl="";
		private int _isgl=0;
		private string _he="";
		private int _ishe=0;
		private string _hr="";
		private int _ishr=0;
		private string _ru="";
		private int _isru=0;
		private string _sv="";
		private int _issv=0;
		private string _ta="";
		private int _ista=0;
		private string _th="";
		private int _isth=0;
		private string _tr="";
		private int _istr=0;
		private string _uk="";
		private int _isuk=0;
		private string _vi="";
		private int _isvi=0;
		private string _hu="";
		private int _ishu=0;
		private string _id_="";
		private int _isid_=0;
		private string _it="";
		private int _isit=0;
		private string _ka="";
		private int _iska=0;
		private string _ko="";
		private int _isko=0;
		private string _lt="";
		private int _islt=0;
		private string _mk="";
		private int _ismk=0;
		private string _ms="";
		private int _isms=0;
		private string _nl="";
		private int _isnl=0;
		private string _no="";
		private int _isno=0;
		private string _pl="";
		private int _ispl=0;
		private string _pt="";
		private int _ispt=0;
		private string _ro="";
		private int _isro=0;
		private string _tw="";
		private string _tcn="";
		private int _istcn=0;
		private Lebi_Language_Tag _model;
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
		public string tag
		{
			set{ _tag=value;}
			get{return _tag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CN
		{
			set{ _cn=value;}
			get{return _cn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCN
		{
			set{ _iscn=value;}
			get{return _iscn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EN
		{
			set{ _en=value;}
			get{return _en;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsEN
		{
			set{ _isen=value;}
			get{return _isen;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ja
		{
			set{ _ja=value;}
			get{return _ja;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isja
		{
			set{ _isja=value;}
			get{return _isja;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ar
		{
			set{ _ar=value;}
			get{return _ar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isar
		{
			set{ _isar=value;}
			get{return _isar;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ca
		{
			set{ _ca=value;}
			get{return _ca;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isca
		{
			set{ _isca=value;}
			get{return _isca;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cs
		{
			set{ _cs=value;}
			get{return _cs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Iscs
		{
			set{ _iscs=value;}
			get{return _iscs;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string cy
		{
			set{ _cy=value;}
			get{return _cy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Iscy
		{
			set{ _iscy=value;}
			get{return _iscy;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string da
		{
			set{ _da=value;}
			get{return _da;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isda
		{
			set{ _isda=value;}
			get{return _isda;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string de
		{
			set{ _de=value;}
			get{return _de;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isde
		{
			set{ _isde=value;}
			get{return _isde;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string el
		{
			set{ _el=value;}
			get{return _el;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isel
		{
			set{ _isel=value;}
			get{return _isel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string es
		{
			set{ _es=value;}
			get{return _es;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ises
		{
			set{ _ises=value;}
			get{return _ises;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string eu
		{
			set{ _eu=value;}
			get{return _eu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Iseu
		{
			set{ _iseu=value;}
			get{return _iseu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fa
		{
			set{ _fa=value;}
			get{return _fa;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isfa
		{
			set{ _isfa=value;}
			get{return _isfa;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fi
		{
			set{ _fi=value;}
			get{return _fi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isfi
		{
			set{ _isfi=value;}
			get{return _isfi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string fr
		{
			set{ _fr=value;}
			get{return _fr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isfr
		{
			set{ _isfr=value;}
			get{return _isfr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string gl
		{
			set{ _gl=value;}
			get{return _gl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isgl
		{
			set{ _isgl=value;}
			get{return _isgl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string he
		{
			set{ _he=value;}
			get{return _he;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ishe
		{
			set{ _ishe=value;}
			get{return _ishe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hr
		{
			set{ _hr=value;}
			get{return _hr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ishr
		{
			set{ _ishr=value;}
			get{return _ishr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ru
		{
			set{ _ru=value;}
			get{return _ru;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isru
		{
			set{ _isru=value;}
			get{return _isru;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string sv
		{
			set{ _sv=value;}
			get{return _sv;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Issv
		{
			set{ _issv=value;}
			get{return _issv;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ta
		{
			set{ _ta=value;}
			get{return _ta;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ista
		{
			set{ _ista=value;}
			get{return _ista;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string th
		{
			set{ _th=value;}
			get{return _th;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isth
		{
			set{ _isth=value;}
			get{return _isth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tr
		{
			set{ _tr=value;}
			get{return _tr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Istr
		{
			set{ _istr=value;}
			get{return _istr;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string uk
		{
			set{ _uk=value;}
			get{return _uk;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isuk
		{
			set{ _isuk=value;}
			get{return _isuk;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string vi
		{
			set{ _vi=value;}
			get{return _vi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isvi
		{
			set{ _isvi=value;}
			get{return _isvi;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string hu
		{
			set{ _hu=value;}
			get{return _hu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ishu
		{
			set{ _ishu=value;}
			get{return _ishu;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string id_
		{
			set{ _id_=value;}
			get{return _id_;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isid_
		{
			set{ _isid_=value;}
			get{return _isid_;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string it
		{
			set{ _it=value;}
			get{return _it;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isit
		{
			set{ _isit=value;}
			get{return _isit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ka
		{
			set{ _ka=value;}
			get{return _ka;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Iska
		{
			set{ _iska=value;}
			get{return _iska;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ko
		{
			set{ _ko=value;}
			get{return _ko;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isko
		{
			set{ _isko=value;}
			get{return _isko;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string lt
		{
			set{ _lt=value;}
			get{return _lt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Islt
		{
			set{ _islt=value;}
			get{return _islt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string mk
		{
			set{ _mk=value;}
			get{return _mk;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ismk
		{
			set{ _ismk=value;}
			get{return _ismk;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ms
		{
			set{ _ms=value;}
			get{return _ms;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isms
		{
			set{ _isms=value;}
			get{return _isms;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string nl
		{
			set{ _nl=value;}
			get{return _nl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isnl
		{
			set{ _isnl=value;}
			get{return _isnl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string no
		{
			set{ _no=value;}
			get{return _no;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isno
		{
			set{ _isno=value;}
			get{return _isno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pl
		{
			set{ _pl=value;}
			get{return _pl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ispl
		{
			set{ _ispl=value;}
			get{return _ispl;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string pt
		{
			set{ _pt=value;}
			get{return _pt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Ispt
		{
			set{ _ispt=value;}
			get{return _ispt;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ro
		{
			set{ _ro=value;}
			get{return _ro;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Isro
		{
			set{ _isro=value;}
			get{return _isro;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tw
		{
			set{ _tw=value;}
			get{return _tw;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string tcn
		{
			set{ _tcn=value;}
			get{return _tcn;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Istcn
		{
			set{ _istcn=value;}
			get{return _istcn;}
		}
		#endregion

	}
}

