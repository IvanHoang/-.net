using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// companyList:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class companyListModel
    {
        public companyListModel() { }

        #region Model
        private int _id;
        private string _jin2code;
        private string _companyname;
        private string _putrecno;
        private string _bizopetpsno;
        private string _bizopetpsnm;
        private string _rcvgdetpsno;
        private string _rcvgdetpsnm;
        private string _dcletpsno;
        private string _dcletpsnm;
        private string _rltentrybizopetpssccd;
        private string _rltentrybizopetpsno;
        private string _rltentrybizopetpsnm;
        private string _createpep;
        private DateTime? _createdate;
        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string jin2Code
        {
            set { _jin2code = value; }
            get { return _jin2code; }
        }
        /// <summary>
        /// 企业代码
        /// </summary>
        public string companyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 账册编号
        /// </summary>
        public string putrecNo
        {
            set { _putrecno = value; }
            get { return _putrecno; }
        }
        /// <summary>
        /// 经营企业编号
        /// </summary>
        public string bizopEtpsno
        {
            set { _bizopetpsno = value; }
            get { return _bizopetpsno; }
        }
        /// <summary>
        /// 经营企业名称
        /// </summary>
        public string bizopEtpsNm
        {
            set { _bizopetpsnm = value; }
            get { return _bizopetpsnm; }
        }
        /// <summary>
        /// 收货企业编号
        /// </summary>
        public string rcvgdEtpsNo
        {
            set { _rcvgdetpsno = value; }
            get { return _rcvgdetpsno; }
        }
        /// <summary>
        /// 收货企业名称
        /// </summary>
        public string rcvgdEtpsNm
        {
            set { _rcvgdetpsnm = value; }
            get { return _rcvgdetpsnm; }
        }
        /// <summary>
        /// 申报企业编号
        /// </summary>
        public string dclEtpsno
        {
            set { _dcletpsno = value; }
            get { return _dcletpsno; }
        }
        /// <summary>
        /// 申报企业名称
        /// </summary>
        public string dclEtpsNm
        {
            set { _dcletpsnm = value; }
            get { return _dcletpsnm; }
        }
        /// <summary>
        /// 关联报关单境内收发货人社会信用代码
        /// </summary>
        public string rltEntryBizopEtpsSccd
        {
            set { _rltentrybizopetpssccd = value; }
            get { return _rltentrybizopetpssccd; }
        }
        /// <summary>
        /// 关联报关单境内收发货人编号
        /// </summary>
        public string rltEntryBizopEtpsno
        {
            set { _rltentrybizopetpsno = value; }
            get { return _rltentrybizopetpsno; }
        }
        /// <summary>
        /// 关联报关单境内收发货人名称
        /// </summary>
        public string rltEntryBizopEtpsNm
        {
            set { _rltentrybizopetpsnm = value; }
            get { return _rltentrybizopetpsnm; }
        }
        /// <summary>
        /// 创建人
        /// </summary>
        public string createPep
        {
            set { _createpep = value; }
            get { return _createpep; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? createDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model
    }
}
