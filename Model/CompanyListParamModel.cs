using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// CompanyList:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class CompanyListParamModel
    {
        #region Model
        private int _id;
        private string _sqlname = "";
        private string _qz = "";
        private string _companyname = "";
        private string _companyallname = "";
        private string _jin2_username = "";
        private string _jin2_password = "";
        private string _auto = "";
        private string _dataType = "";
        private DateTime _ValidityDate = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
        private bool _secdLawfCtrl = false;
        private int _CheckCtrl = 0;
        private string _oms = "";
        private string _appendTypecd = "";
        private int _apitest = 0;
        private int _uploadAgent = 0;
        private int _sendSave = 0;

        /// <summary>
        /// 发送暂存（东方支付）
        /// </summary>
        public int sendSave
        {
            get { return _sendSave; }
            set { _sendSave = value; }
        }

        /// <summary>
        /// 必填客户简称（表头表体上传）
        /// </summary>
        public int uploadAgent
        {
            get { return _uploadAgent; }
            set { _uploadAgent = value; }
        }

        /// <summary>
        /// oms库名
        /// </summary>
        public string oms
        {
            get { return _oms; }
            set { _oms = value; }
        }

        public bool secdLawfCtrl
        {
            get { return _secdLawfCtrl; }
            set { _secdLawfCtrl = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sqlName
        {
            set { _sqlname = value; }
            get { return _sqlname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string qz
        {
            set { _qz = value; }
            get { return _qz; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string companyName
        {
            set { _companyname = value; }
            get { return _companyname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string companyAllName
        {
            set { _companyallname = value; }
            get { return _companyallname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string jin2_username
        {
            set { _jin2_username = value; }
            get { return _jin2_username; }
        }
        public string jin2_password
        {
            set { _jin2_password = value; }
            get { return _jin2_password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string auto
        {
            set { _auto = value; }
            get { return _auto; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string dataType
        {
            set { _dataType = value; }
            get { return _dataType; }
        }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime ValidityDate
        {
            get { return _ValidityDate; }
            set { _ValidityDate = value; }
        }

        public int CheckCtrl
        {
            get { return _CheckCtrl; }
            set { _CheckCtrl = value; }
        }

        public string appendTypecd
        {
            get { return _appendTypecd; }
            set { _appendTypecd = value; }
        }

        public int apitest
        {
            get { return _apitest; }
            set { _apitest = value; }
        }

        #endregion Model
    }
}
