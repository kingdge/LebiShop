using System;
namespace Shop.Model
{
	[Serializable]
	public class Lebi_Promotion
	{
		#region Model
		private int _id=0;
		private int _admin_id=0;
		private string _admin_username="";
		private string _remark="";
		private DateTime _time_add=DateTime.Now;
		private DateTime _time_update=DateTime.Now;
		private DateTime _time_start=DateTime.Now;
		private DateTime _time_end=DateTime.Now;
		private int _sort=0;
		private int _type_id_promotionstatus=0;
		private int _issetcase=0;
		private int _issetrule=0;
		private int _promotion_type_id=0;
		private int _case801=0;
		private int _case802=0;
		private int _case803=0;
		private int _case806=0;
		private int _iscase801=0;
		private int _iscase802=0;
		private int _iscase803=0;
		private int _iscase804=0;
		private int _iscase805=0;
		private int _iscase806=0;
		private int _rule901=0;
		private int _rule902=0;
		private int _rule903=0;
		private int _rule904=0;
		private int _rule905=0;
		private int _rule906=0;
		private int _rule907=0;
		private int _rule908=0;
		private string _rule909="";
		private int _rule910=0;
		private string _rule911="";
		private int _isrule901=0;
		private int _isrule902=0;
		private int _isrule903=0;
		private int _isrule904=0;
		private int _isrule905=0;
		private int _isrule906=0;
		private int _isrule907=0;
		private int _isrule908=0;
		private int _isrule909=0;
		private int _isrule910=0;
		private int _isrule911=0;
		private string _case805="";
		private string _case804="";
		private int _rule912=0;
		private int _isrule912=0;
		private Lebi_Promotion _model;
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
		public int Admin_id
		{
			set{ _admin_id=value;}
			get{return _admin_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Admin_UserName
		{
			set{ _admin_username=value;}
			get{return _admin_username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Add
		{
			set{ _time_add=value;}
			get{return _time_add;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Update
		{
			set{ _time_update=value;}
			get{return _time_update;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_Start
		{
			set{ _time_start=value;}
			get{return _time_start;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime Time_End
		{
			set{ _time_end=value;}
			get{return _time_end;}
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
		/// 240筹备中,241进行中,242已过期,243停止
		/// </summary>
		public int Type_id_PromotionStatus
		{
			set{ _type_id_promotionstatus=value;}
			get{return _type_id_promotionstatus;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSetCase
		{
			set{ _issetcase=value;}
			get{return _issetcase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsSetRule
		{
			set{ _issetrule=value;}
			get{return _issetrule;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Promotion_Type_id
		{
			set{ _promotion_type_id=value;}
			get{return _promotion_type_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Case801
		{
			set{ _case801=value;}
			get{return _case801;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Case802
		{
			set{ _case802=value;}
			get{return _case802;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Case803
		{
			set{ _case803=value;}
			get{return _case803;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Case806
		{
			set{ _case806=value;}
			get{return _case806;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCase801
		{
			set{ _iscase801=value;}
			get{return _iscase801;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCase802
		{
			set{ _iscase802=value;}
			get{return _iscase802;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCase803
		{
			set{ _iscase803=value;}
			get{return _iscase803;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCase804
		{
			set{ _iscase804=value;}
			get{return _iscase804;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCase805
		{
			set{ _iscase805=value;}
			get{return _iscase805;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsCase806
		{
			set{ _iscase806=value;}
			get{return _iscase806;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule901
		{
			set{ _rule901=value;}
			get{return _rule901;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule902
		{
			set{ _rule902=value;}
			get{return _rule902;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule903
		{
			set{ _rule903=value;}
			get{return _rule903;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule904
		{
			set{ _rule904=value;}
			get{return _rule904;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule905
		{
			set{ _rule905=value;}
			get{return _rule905;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule906
		{
			set{ _rule906=value;}
			get{return _rule906;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule907
		{
			set{ _rule907=value;}
			get{return _rule907;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule908
		{
			set{ _rule908=value;}
			get{return _rule908;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rule909
		{
			set{ _rule909=value;}
			get{return _rule909;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule910
		{
			set{ _rule910=value;}
			get{return _rule910;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Rule911
		{
			set{ _rule911=value;}
			get{return _rule911;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule901
		{
			set{ _isrule901=value;}
			get{return _isrule901;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule902
		{
			set{ _isrule902=value;}
			get{return _isrule902;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule903
		{
			set{ _isrule903=value;}
			get{return _isrule903;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule904
		{
			set{ _isrule904=value;}
			get{return _isrule904;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule905
		{
			set{ _isrule905=value;}
			get{return _isrule905;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule906
		{
			set{ _isrule906=value;}
			get{return _isrule906;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule907
		{
			set{ _isrule907=value;}
			get{return _isrule907;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule908
		{
			set{ _isrule908=value;}
			get{return _isrule908;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule909
		{
			set{ _isrule909=value;}
			get{return _isrule909;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule910
		{
			set{ _isrule910=value;}
			get{return _isrule910;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule911
		{
			set{ _isrule911=value;}
			get{return _isrule911;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Case805
		{
			set{ _case805=value;}
			get{return _case805;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Case804
		{
			set{ _case804=value;}
			get{return _case804;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Rule912
		{
			set{ _rule912=value;}
			get{return _rule912;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int IsRule912
		{
			set{ _isrule912=value;}
			get{return _isrule912;}
		}
		#endregion

	}
}

