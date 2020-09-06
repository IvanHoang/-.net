using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public partial class CompanyIfoModel
	{
       public CompanyIfoModel()
		{}
		#region Model
		private int _id;
		private string _customno;
		private string _bizopetpsnm;
		private string _bizopetpsno;
		private string _bizopetpssccd;
		private string _dclplccuscd;
		private string _createpep;
		private DateTime? _createdate;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 海关编码
		/// </summary>
		public string customNo
		{
			set{ _customno=value;}
			get{return _customno;}
		}
		/// <summary>
		/// 企业名称
		/// </summary>
		public string bizopEtpsNm
		{
			set{ _bizopetpsnm=value;}
			get{return _bizopetpsnm;}
		}
		/// <summary>
		/// 企业编号
		/// </summary>
		public string bizopEtpsno
		{
			set{ _bizopetpsno=value;}
			get{return _bizopetpsno;}
		}
		/// <summary>
		/// 经营企业社会信用代码
		/// </summary>
		public string bizopEtpsSccd
		{
			set{ _bizopetpssccd=value;}
			get{return _bizopetpssccd;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string dclPlcCuscd
		{
			set{ _dclplccuscd=value;}
			get{return _dclplccuscd;}
		}
		/// <summary>
		/// 创建人
		/// </summary>
		public string createPep
		{
			set{ _createpep=value;}
			get{return _createpep;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime? createDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		#endregion Model

	}
}


