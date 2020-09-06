using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public partial class nimp_agent
    {
        public nimp_agent()
        { }
            #region Model
        private int _id;
        private string _agentName;
        private string _agentAllName;
        private string _contactPep;
        private string _contactTel;
        private string _ypep;
        private string _ytel;
        private string _cpep;
        private string _ctel;
        private string _address;
        private string _tel;
        private string _userName;
        private string _pwd;
        private string _fax;
        private string _bank;
        private string _bank_ads;
        private string _bank_tel;
        private string _bank_account;
        private string _invoiceNo;
        private string _in_pep;
        private DateTime? _in_date;

        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; } = 0;
        /// <summary>
        /// 客户简称
        /// </summary>
        public string AgentName { get; set; }
        /// <summary>
        /// 客户全称
        /// </summary>
        public string agentAllName { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string contactPep { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string contactTel { get; set; }
        /// <summary>
        /// 业务联系人
        /// </summary>
        public string ypep { get; set; }
        /// <summary>
        /// 业务联系电话
        /// </summary>
        public string ytel { get; set; }
        /// <summary>
        /// 财务联系人
        /// </summary>
        public string cpep { get; set; }
        /// <summary>
        /// 财务联系电话
        /// </summary>
        public string ctel { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 平台用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 平台密码
        /// </summary>
        public string pwd { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public string fax { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        public string bank { get; set; }
        /// <summary>
        /// 开户行地址
        /// </summary>
        public string bank_ads { get; set; }
        /// <summary>
        /// 开户行电话
        /// </summary>
        public string bank_tel { get; set; }
        /// <summary>
        /// 开户行账号
        /// </summary>
        public string bank_account { get; set; }
        /// <summary>
        /// 税号
        /// </summary>
        public string invoiceNo { get; set; }
        /// <summary>
        /// in_pep
        /// </summary>
        public string in_pep { get; set; }
        /// <summary>
        /// in_data
        /// </summary>
        public DateTime in_date { get; set; }



        #endregion
    }
}
