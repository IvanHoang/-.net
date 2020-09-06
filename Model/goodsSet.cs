using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Symboluser

    {

        public string id { set; get; }

        public string symbol { set; get; }

    }
    public partial class goodsSet
    {
        public goodsSet()
        { }
        #region Model
        private int _id;
        private string _sku;
        private string _gdsmtno;
        private string _gdecd;
        private string _gdsnm;
        private string _gdsspcfmodeldesc;
        private string _dclunitcd;
        private string _lawfunitcd;
        private int? _putrecseqno;
        private string _merge_num;
        private string _natcd;
        private string _yongtu;
        private string _lvyrlfmodecd;
        private string _createpep;
        private DateTime? _createdate;
        private string _secdLawfUnitcd;
        private string _agentName;
        private decimal _ratio;

        public decimal ratio
        {
            set { _ratio = value; }
            get { return _ratio; }
        }

        public string AgentName
        {
            set { _agentName = value; }
            get { return _agentName; }
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
        /// WMS中货物型号
        /// </summary>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }

        /// <summary>
        /// 品名
        /// </summary>
        public string GoodsName{get; set; }

        /// <summary>
        /// 规格型号
        /// </summary>
        public string gdsSpcfModelDesc { get; set; }

        /// <summary>
        /// 立方
        /// </summary>
        public decimal Volume { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
        public decimal netWt { get; set; }

        /// <summary>
        /// 原产国ddl
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime InDate { get; set; }

        /// <summary>
        /// 录入人
        /// </summary>
        public string InPep { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string department { get; set; }

        /// <summary>
        /// HSCODE
        /// </summary>
        public string HSCode { get; set; }

        /// <summary>
        /// 金2系统中商品料号
        /// </summary>
        public string gdsMtno
        {
            set { _gdsmtno = value; }
            get { return _gdsmtno; }
        }
        /// <summary>
        /// 商品编码
        /// </summary>
        public string gdecd
        {
            set { _gdecd = value; }
            get { return _gdecd; }
        }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string gdsNm
        {
            set { _gdsnm = value; }
            get { return _gdsnm; }
        }
        /// <summary>
        /// 商品规格型号描述
        /// </summary>
        //public string gdsSpcfModelDesc
        //{
        //    set { _gdsspcfmodeldesc = value; }
        //    get { return _gdsspcfmodeldesc; }
        //}
        /// <summary>
        /// 申报计量单位
        /// </summary>
        public string dclUnitcd
        {
            set { _dclunitcd = value; }
            get { return _dclunitcd; }
        }
        /// <summary>
        /// 法定计量单位
        /// </summary>
        public string lawfUnitcd
        {
            set { _lawfunitcd = value; }
            get { return _lawfunitcd; }
        }
        /// <summary>
        /// 备案序号（后续反更新）
        /// </summary>
        public int? putrecSeqno
        {
            set { _putrecseqno = value; }
            get { return _putrecseqno; }
        }
        /// <summary>
        /// 原5+2归并序号
        /// </summary>
        public string MERGE_NUM
        {
            set { _merge_num = value; }
            get { return _merge_num; }
        }
        /// <summary>
        /// 原产国(地区）
        /// </summary>
        public string natcd
        {
            set { _natcd = value; }
            get { return _natcd; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string yongTu
        {
            set { _yongtu = value; }
            get { return _yongtu; }
        }
        /// <summary>
        /// 征减免方式代码
        /// </summary>
        public string lvyrlfModecd
        {
            set { _lvyrlfmodecd = value; }
            get { return _lvyrlfmodecd; }
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

        public string secdLawfUnitcd
        {
            set { _secdLawfUnitcd = value; }
            get { return _secdLawfUnitcd; }
        }
        #endregion Model

    }
}

